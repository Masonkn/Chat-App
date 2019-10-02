using Microsoft.Win32; // add this library because the tutorial told us to
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Text file(*.txt)|*.txt|c# file (*.cs)|*.cs";//allows file types to be set
            dlg.InitialDirectory = @"c:\";//Change to use a different directory.
            //Can also equal = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if(dlg.ShowDialog() == true)
            {
                File.WriteAllText(dlg.FileName, tx1.Text);//Curtis thinks this is where the work actually happens
            }
        }
        
    }
}
