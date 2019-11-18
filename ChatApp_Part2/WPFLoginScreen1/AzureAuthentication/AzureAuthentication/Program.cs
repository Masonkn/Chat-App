using System;
using System.Data.SqlClient;

namespace AzureAuthentication
{
    class Program
    {

        static void Main(string[] args)

        {
            try
            {
                SqlConnection thisConnection = new SqlConnection(@"Server=DESKTOP-Q69OPGB\SQLEXPRESS;Database=LoginDB;Trusted_Connection=Yes;");
                thisConnection.Open();

                string Get_Data = "SELECT * FROM USER_TABLE;";

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable("USER_TABLE");
                sda.Fill(dt);

                MessageBox.Show("Connected");
                //dataGrid1.ItemsSource = dt.DefaultView;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
           
    }
}
