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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var tb = new TextBox();
            var rtb = new RichTextBox();
            rtb.Height = 50;
            rtb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            spBlocks.Children.Add(tb);
            spBlocks.Children.Add(rtb);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (spBlocks.Children.Count >= 4)
            {
                spBlocks.Children.RemoveAt(spBlocks.Children.Count - 1);
                spBlocks.Children.RemoveAt(spBlocks.Children.Count - 1);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(tbBlockCode.Document.ContentStart, tbBlockCode.Document.ContentEnd);

            string t = textRange.Text;

            MessageBox.Show(t);
        }
		
	   //close
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
1111
            Close();
        }
    }
}
