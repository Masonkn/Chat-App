using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Text_Saver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> allMessages = new List<string>(); //The list of all mesages
        String msgDisp; //The string that will represent all the messages sent.
        string connStr = "Server=tcp:coding-messanger-server.database.windows.net,1433;" +
                "Initial Catalog=Coding Messanger;Persist Security Info=False;User ID=Hayden;" +
                "Password=Arthur123;MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;";

        public MainWindow()
        {
            //InitializeComponent();
            PullTimer();
        }

        private void PullTimer()
        {
            DispatcherTimer dt = new DispatcherTimer();

            dt.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dt.Tick += dt_Update;  
       
            dt.Start();
        }
        private void dt_Update(object sender, EventArgs e)
        {
            SQLpull();
        }

        private void PushButton(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE dbo.Code
                    SET codeText = @codeText
                    WHERE CodeID = 4643635";

                    cmd.Parameters.AddWithValue("@codeText", txt_box.Text);

                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        void DisplayMessage()
        {
            msgDisp += "\n" + txt_box.Text;//add onto the display String

            //txt_block.Text = msgDisp;//Replace the display String

            txt_box.Text = "";//So the user can't spam their one message
        }
        void SQLsend(String connStr)
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO dbo.Code (CodeID, CodeText)
                    VALUES (@CodeID, @CodeText)";

                    cmd.Parameters.AddWithValue("@CodeID", new Guid());
                    cmd.Parameters.AddWithValue("@CodeText", txt_box.Text);

                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }

        }
        void SQLedit(String connStr)
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE dbo.Code
                    SET codeText = @codeText
                    WHERE CodeID = 1";

                    cmd.Parameters.AddWithValue("@codeText", txt_box.Text);

                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }
        }
        void PullButton(object sender, RoutedEventArgs e)
        {
            SQLpull();
        }

        void SQLpull()
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var command = conn.CreateCommand())//Reading?
                {
                    command.CommandText = @"
                    SELECT
                    codedb.CodeText
                    FROM dbo.Code as codedb
                    ";
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txt_box.Text = reader.GetString(0);
                        }
                    }
                }
            }
        }

        void LocalSave()
        {
            allMessages.Add(txt_box.Text);//This is more for saving than displaying
            File.WriteAllText(Directory.GetCurrentDirectory() +
                "\\Saved Texts\\savedText.txt", allMessages[allMessages.Count - 1]);//Write the contents of the file
        }
    }
}
