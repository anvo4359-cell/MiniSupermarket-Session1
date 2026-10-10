
using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=MiniSupermarketDb;Integrated Security=True;TrustServerCertificate=True;";

        try
        {
            using SqlConnection conn =
                new SqlConnection(connectionString);

            conn.Open();

            string sql = @"
                SELECT ProductId, Barcode, ProductName,
                       Price, StockQuantity
                FROM Products
                ORDER BY ProductId ASC";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine(
                "{0,-5} {1,-15} {2,-25} {3,12} {4,10}",
                "ID", "Barcode", "Ten san pham", "Gia", "Ton kho");

            Console.WriteLine(new string('-', 75));

            while (reader.Read())
            {
                Console.WriteLine(
                    "{0,-5} {1,-15} {2,-25} {3,12:N0} {4,10}",
                    reader["ProductId"],
                    reader["Barcode"],
                    reader["ProductName"],
                    reader["Price"],
                    reader["StockQuantity"]);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Loi: " + ex.Message);
        }

        Console.ReadKey();
    }
}
