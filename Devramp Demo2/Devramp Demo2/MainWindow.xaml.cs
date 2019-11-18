using System;
using System.Collections.Generic;
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

namespace Devramp_Demo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string connStr = "Server=tcp:coding-messanger-server.database.windows.net,1433;Initial Catalog=Coding Messanger;Persist Security Info=False;User ID=Hayden;Password=Arthur123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (var conn = new SqlConnection(connStr))
            {
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT
                            c.CustomerID,
                            c.CompanyName,
                            COUNT(soh.SalesOrderID) AS OrderCount
                        FROM SalesLT.Customer AS c
                        LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID
                        GROUP BY c.CustomerID, c.CompanyName
                        ORDER BY OrderCount DESC;";
                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("ID: {0} Name: {1} Order Count: {2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                }
            }

            using (var conn = new SqlConnection(connStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate)
                    OUTPUT INSTERT.ProductID
                    VALUES (@Name, @number, @Cost, @Price, CURRENT_TIMESTAMP)";

                    cmd.Parameters.AddWithValue("@Name", "SQL Server Express");
                    cmd.Parameters.AddWithValue("@Number", "SQLEXPRESS1");
                    cmd.Parameters.AddWithValue("@Cost", 0);
                    cmd.Parameters.AddWithValue("@Price", 0);

                    conn.Open();

                    int insertedProductID = (int)cmd.ExecuteScalar();

                    Console.WriteLine("Product ID {0} inserted.", insertedProductID);
                }
            }

            Console.ReadLine();
        }
    }
}
