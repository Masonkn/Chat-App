using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Text_Saver
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
            allMessages.Add(txt_box.Text);//This is more for saving than displaying
            msgDisp += "\n" + txt_box.Text;//add onto the display String
            
            txt_block.Text = msgDisp;//Replace the display String

            File.WriteAllText(Directory.GetCurrentDirectory() + 
                "\\Saved Texts\\savedText.txt", allMessages[allMessages.Count-1]);//Write the contents of the file

            txt_box.Text = "";//So the user can't spam their one message
        }
    }
}
