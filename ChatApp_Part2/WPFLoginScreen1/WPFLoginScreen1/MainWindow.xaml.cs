using System;
using System.IO;
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

namespace WPFLoginScreen1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> allMessages = new List<string>(); //The list of all mesages
        String msgDisp; //The string that will represent all the messages sent.

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            allMessages.Add(txtbox_msg.Text);//This is more for saving than displaying
            msgDisp += "\n" + txtbox_msg.Text;//add onto the display String

            msg_txtblock.Text = msgDisp;//Replace the display String

            File.WriteAllText(Directory.GetCurrentDirectory() +
                "\\Saved Texts\\savedText.txt", allMessages[allMessages.Count - 1]);//Write the contents of the file

            txtbox_msg.Text = "";//So the user can't spam their one message
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnMinim_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMaxim_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();

        }

        private void topDock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                if (e.ClickCount == 2)
                {
                    AdjustWindowSize();
                }
                else
                {
                    Application.Current.MainWindow.DragMove();
                }
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
    }
}
