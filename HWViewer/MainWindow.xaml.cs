using HWViewer.Model;
using HWViewer.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HWViewer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            (DataContext as ViewModel.MainVM).view = this;

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                (DataContext as ViewModel.MainVM).Open(args[1]);
            }
        }

        private void MIClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
