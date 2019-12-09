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
using System.Data.SqlClient;

namespace Chat_App_Application
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "dev" && txtPassword.Password == "123")
            {
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
            else
            {
                //This connects to Azure Database Server
                SqlConnection sqlCon = new SqlConnection(@"Data Source= tcp:coding-messanger-server.database.windows.net,1433;" + "Initial Catalog=Coding Messanger;Persist Security Info=False;User ID=Hayden;" +
                "Password=Arthur123;MultipleActiveResultSets=False;Encrypt=True;" +
                "TrustServerCertificate=False;Connection Timeout=30;");
               
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    String query = "SELECT COUNT(1) FROM USER_LOGIN WHERE USERNAME=@USERNAME AND PASSWORD=@PASSWORD";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@USERNAME", txtUsername.Text);
                    sqlCmd.Parameters.AddWithValue("@PASSWORD", txtPassword.Password);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MainWindow dashboard = new MainWindow();
                        dashboard.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is incorrect.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();

                }
            }
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            signup_page.Visibility = Visibility.Visible;
        }

        private void signup_back(object sender, RoutedEventArgs e)
        {
            signup_page.Visibility = Visibility.Hidden;
        }

        private void finish_signup(object sender, RoutedEventArgs e)
        {
            if (pass.Password != confirm_pass.Password)
            {
                confirm_pass.BorderBrush = Brushes.PaleVioletRed;
                pass_error.Visibility = Visibility.Visible;

            } else if (pass.Password == confirm_pass.Password)
            {
                confirm_pass.BorderBrush = Brushes.LightGray;
                pass_error.Visibility = Visibility.Hidden;
                signup_page.Visibility = Visibility.Hidden;
            }
        }
    }
}

