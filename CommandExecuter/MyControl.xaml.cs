using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Company.CommandExecuter
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl
    {
        public MyControl()
        {
            InitializeComponent();
        }

        private void MyToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        CommandWindow win;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win = new CommandWindow();
            win.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string document = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(document, "CommandExecuter"));
            System.Diagnostics.Process.Start(path);
        }
    }
}