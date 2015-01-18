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
using System.Windows.Shapes;

namespace Company.CommandExecuter
{
    /// <summary>
    /// CommandWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CommandWindow : Window
    {
        Dictionary<string, string> commandId = new Dictionary<string, string>();
        Dictionary<string, string> commandDesc = new Dictionary<string, string>();

        public CommandWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }

        public void LoadSettings()
        {
            string document = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(document, "CommandExecuter", "settings.txt"));

            string[] keys = new[] { "up", "down", "left", "right" };
            foreach (var key in keys)
            {
                commandId[key] = "";
                commandDesc[key] = "";
            }
            if (System.IO.File.Exists(path))
            {
                var allLines = System.IO.File.ReadAllLines(path);
                foreach (var line in allLines)
                {
                    var tokens = line.Split(',').Select(t => t.Trim()).ToArray();
                    if (tokens.Length != 3)
                        continue;
                    var key = tokens[0].ToLower();
                    if (keys.Contains(tokens[0].ToLower()))
                    {
                        commandId[key] = tokens[1];
                        commandDesc[key] = tokens[2];
                    }
                }
            }
            UpButton.Content = "[U/I] " + commandId["up"] + "\n" + commandDesc["up"];
            DownButton.Content = "[N/M] " + commandId["down"] + "\n" + commandDesc["down"];
            LeftButton.Content = "[H] " + commandId["left"] + "\n" + commandDesc["left"];
            RightButton.Content = "[K] " + commandId["right"] + "\n" + commandDesc["right"];
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommand("up");
            this.Hide();
        }
        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommand("left");
            this.Hide();
        }
        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommand("right");
            this.Hide();
        }
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteCommand("down");
            this.Hide();
        }
        void ExecuteCommand(string key)
        {
            try
            {
                key = key.ToLower();
                if (commandId.ContainsKey(key))
                {
                    var cmd = FLib.VSInfo.DTE2.Commands.Item(commandId[key]);
                    object cin = null, cout = null;
                    FLib.VSInfo.DTE2.Commands.Raise(cmd.Guid, cmd.ID, ref cin, ref cout);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "\n" + ex.StackTrace);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.U:
                case Key.I:
                    ExecuteCommand("up");
                    this.Hide();
                    break;
                case Key.N:
                case Key.M:
                    ExecuteCommand("down");
                    this.Hide();
                    break;
                case Key.H:
                    ExecuteCommand("left");
                    this.Hide();
                    break;
                case Key.K:
                    ExecuteCommand("right");
                    this.Hide();
                    break;
                case Key.Escape:
                    this.Hide();
                    break;
            }
        }
    }
}