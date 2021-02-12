using HWViewer.Model;
using HWViewer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace HWViewer.ViewModel
{
    public class MainVM : BaseVM
    {
        public const string UnknownData = "???";
        private const string FileDialogFilter = "Home Work (*.txthw)|*.txthw|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
        private string openedPath = string.Empty;
        private HomeWork openedHW = new HomeWork()
        {
            FIO = "Strelkov Roman",
            Task = @"Назовите основные этапы процесса разработки программного обеспечения.
Назовите основные модели процесса разработки программного обеспечения.Дайте им краткую характеристику.
",
            Decision = "public class A { public int Q{get; set;}}",
            Comment = "?"
        };


        internal MainWindow view;
        bool styleIsDark = true;
        bool rawView = false;

        public string OpenedPath
        {
            get { return openedPath; }
            set
            {
                openedPath = value;
                OnPropertyChanged(nameof(OpenedPath));
            }
        }

        public HomeWork OpenedHW
        {
            get { return openedHW; }
            set
            {
                openedHW = value;
                OnPropertyChanged(nameof(OpenedHW));
            }
        }

        public bool RawView
        {
            get { return rawView; }
            set
            {
                rawView = value;
                OnPropertyChanged(nameof(RawView));

                if (rawView)
                    OpenRaw(OpenedPath);
                else
                    Open(OpenedPath);
            }
        }


        readonly DoubleAnimation fastHidingMessageAnimation = new DoubleAnimation() { Duration = TimeSpan.FromMilliseconds(1500) };
        private string message;
        private const int OpacityMaxValue = 1;
        private const int OpacityMinValue = 0;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));

                fastHidingMessageAnimation.From = OpacityMaxValue;
                fastHidingMessageAnimation.To = OpacityMinValue;

                view.tbFastMessage.BeginAnimation(UIElement.OpacityProperty, fastHidingMessageAnimation);
            }
        }

        public MainVM()
        {

        }

        #region Команды

        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(OpenAs));
            }
        }
        private RelayCommand openCommand = null;

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(Save));
            }
        }
        private RelayCommand saveCommand = null;

        public RelayCommand SaveAsCommand
        {
            get
            {
                return saveAsCommand ??
                  (saveAsCommand = new RelayCommand(SaveAs));
            }
        }
        private RelayCommand saveAsCommand = null;


        public RelayCommand ChangeStyleCommand
        {
            get
            {
                return changeStyle ??
                  (changeStyle = new RelayCommand(ChangeStyle));
            }
        }
        private RelayCommand changeStyle = null;
        #endregion

        #region Методы команд

        private void Save(object o)
        {
            try
            {
                openedHW.Decision = new TextRange(view.tbDecision.Document.ContentStart, view.tbDecision.Document.ContentEnd).Text;
                FileWorker.Save(openedPath, openedHW);
                Message = "Saved";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void SaveAs(object o)
        {
            var sfd = new Microsoft.Win32.SaveFileDialog
            {
                Filter = FileDialogFilter,
                FileName = OpenedPath
            };
            if (sfd.ShowDialog() == true)
            {
                OpenedPath = sfd.FileName;
                Save(o);
            }
        }


        /// <summary>
        /// Открытие файла
        /// </summary>
        private void OpenAs(object o)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog
            {
                Filter = FileDialogFilter,
                FileName = OpenedPath
            };
            if (ofd.ShowDialog() == true)
            {
                string path = ofd.FileName;
                Open(path);
            }
        }

        public void Open(string path)
        {
            OpenedPath = path;
            try
            {
                if (RawView)
                {
                    OpenRaw(path);
                }
                else
                {
                    OpenedHW = FileWorker.Open(path);
                    UpdateRTBColors();
                }
            }
            catch (FileFormatException ffe)
            {
                Message = ffe.Message;
                OpenRaw(path);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateRTBColors()
        {
            CodeFormatter.Fill(view.tbDecision.Document, OpenedHW.Decision);
        }

        /// <summary>
        /// Открыть сырую версию документа
        /// </summary>
        /// <param name="path"></param>
        private void OpenRaw(string path)
        {
            try
            {
                OpenedHW = new HomeWork(UnknownData, UnknownData, FileWorker.OpenRaw(path), UnknownData);
                UpdateRTBColors();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ChangeStyle(object o)
        {
            styleIsDark ^= true;
            if (styleIsDark)
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Styles/Dark.xaml", UriKind.Relative);
            else
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri("Styles/Light.xaml", UriKind.Relative);

            UpdateRTBColors();
        }

        #endregion


    }
}
