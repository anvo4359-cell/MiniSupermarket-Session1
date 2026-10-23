
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                @"Server=(localdb)\MSSQLLocalDB;Database=MiniSupermarketDb;Integrated Security=True;TrustServerCertificate=True;";

            Console.WriteLine(
                "=== BAI 3: DOC DANH SACH BANG SQLDATAREADER ===");

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = @"
                    SELECT ProductId, Barcode, ProductName,
                           Price, StockQuantity
                    FROM Products
                    ORDER BY ProductId ASC";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine(
                    "{0,-5} {1,-15} {2,-35} {3,15} {4,10}",
                    "ID", "Barcode", "Ten san pham", "Gia", "Ton kho");

                Console.WriteLine(new string('-', 90));

                int count = 0;

                while (reader.Read())
                {
                    count++;

                    int id = Convert.ToInt32(reader["ProductId"]);
                    string barcode = reader["Barcode"].ToString() ?? "";
                    string name = reader["ProductName"].ToString() ?? "";
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int stock = Convert.ToInt32(reader["StockQuantity"]);

                    Console.WriteLine(
                        "{0,-5} {1,-15} {2,-35} {3,15:N0} {4,10}",
                        id, barcode, name, price, stock);
                }

                Console.WriteLine(new string('-', 90));

                Console.WriteLine(
                    $"Tong so mat hang da doc: {count}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("[LOI SQL]: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LOI HE THONG]: " + ex.Message);
            }

            Console.WriteLine("Nhan phim bat ky de ket thuc...");
            Console.ReadKey();
        }
    }
}
