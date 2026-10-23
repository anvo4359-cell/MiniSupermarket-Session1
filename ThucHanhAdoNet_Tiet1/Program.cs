
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                @"Server=(localdb)\MSSQLLocalDB;Database=MiniSupermarketDb;Integrated Security=True;TrustServerCertificate=True;";

            Console.WriteLine("=== BAI 1: DEM TONG SO MAT HANG ===");

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM Products";

                    using (SqlCommand command =
                           new SqlCommand(query, connection))
                    {
                        connection.Open();

                        Console.WriteLine(
                            "Da ket noi SQL Server thanh cong!");

                        object? result = command.ExecuteScalar();

                        int totalCount = Convert.ToInt32(result);

                        Console.WriteLine(
                            $"Tong so mat hang: {totalCount} san pham.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("[LOI SQL]: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[LOI HE THONG]: " + ex.Message);
                }
            }

            Console.WriteLine("Nhan phim bat ky de ket thuc...");
            Console.ReadKey();
        }
    }
}
