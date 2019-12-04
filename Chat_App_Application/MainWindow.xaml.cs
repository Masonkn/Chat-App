using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace Chat_App_Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> allMessages = new List<string>(); //The list of all mesages
        String msgDisp; //The string that will represent all the messages sent.
        Random random = new Random();
        string connStr = "Server=tcp:coding-messanger-server.database.windows.net,1433;" +
               "Initial Catalog=Coding Messanger;Persist Security Info=False;User ID=Hayden;" +
               "Password=Arthur123;MultipleActiveResultSets=False;Encrypt=True;" +
               "TrustServerCertificate=False;Connection Timeout=30;";

        public MainWindow()
        {
            InitializeComponent();
            ResetTable();
            PullTimer();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LocalSave();
            SQLsend();
            SQLread();
            //DisplayMessage();
        }

        void DisplayMessage()
        {
            msgDisp += "\n" + msg_txtbox.Text;//add onto the display String

            msg_txtblock.Text = msgDisp;//Replace the display String

            msg_txtbox.Text = "";//So the user can't spam their one message
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
            SQLread();
        }

        void SQLsend()
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO dbo.Code (CodeID, CodeText)
                    VALUES (@CodeID, @CodeText)";

                    //string id = new Guid().ToString();

                    //cmd.Parameters.AddWithValue("@CodeID", new Guid());
                    cmd.Parameters.AddWithValue("@CodeID", random.Next());
                    cmd.Parameters.AddWithValue("@CodeText", msg_txtbox.Text);

                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }

        }

        void SQLread()
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
                            msg_txtblock.Text = "";
                            msgDisp += "\n" + reader.GetString(0);
                            msg_txtblock.Text = msgDisp;
                            msg_txtbox.Text = "";//So the user can't spam their one message
                        }
                    }
                }
            }
        }

        void ResetTable()
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    DELETE FROM dbo.Code
                    ";

                    conn.Open();
                    cmd.ExecuteScalar();
                }
            }
        }

        void LocalSave()
        {
            allMessages.Add(msg_txtbox.Text);//This is more for saving than displaying
            File.WriteAllText(Directory.GetCurrentDirectory() +
                "\\Saved Texts\\savedText.txt", allMessages[allMessages.Count - 1]);//Write the contents of the file
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

        private void TopDock_MouseDown(object sender, MouseButtonEventArgs e)
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


        private void Create_NewTicket(object sender, RoutedEventArgs e)
        {
            NewTicket_popup.Visibility = Visibility.Visible;
        }

        private void New_Ticket_done(object sender, RoutedEventArgs e)
        {
            NewTicket_popup.Visibility = Visibility.Hidden;

            ListBoxItem new_ticket = new ListBoxItem();
            new_ticket.Height = 60;

            if (Tickets.Items.Count % 2 == 0 )
            {
                new_ticket.Background = Brushes.White;

            }

            new_ticket.Content = New_Ticket_Name.Text;
            Tickets.Items.Add(new_ticket);
        }

        private void PopUpClose_Click(object sender, RoutedEventArgs e)
        {
            NewTicket_popup.Visibility = Visibility.Hidden;
        }
    }
}
