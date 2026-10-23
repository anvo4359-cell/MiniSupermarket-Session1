
using System;
using Microsoft.Data.SqlClient;

namespace ThucHanhAdoNet_Tiet2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                @"Server=(localdb)\MSSQLLocalDB;Database=MiniSupermarketDb;Integrated Security=True;TrustServerCertificate=True;";

            Console.WriteLine(
                "=== BAI 2: CAP NHAT TON KHO BANG EXECUTENONQUERY ===");

            int productId = 1;
            int newStockQuantity = 250;

            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                try
                {
                    string updateSql = @"
                        UPDATE Products
                        SET StockQuantity = @Stock
                        WHERE ProductId = @Id";

                    using (SqlCommand command =
                           new SqlCommand(updateSql, connection))
                    {
                        command.Parameters.AddWithValue(
                            "@Stock", newStockQuantity);

                        command.Parameters.AddWithValue(
                            "@Id", productId);

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine(
                                $"Cap nhat thanh cong san pham ID = {productId}.");

                            Console.WriteLine(
                                $"Ton kho moi: {newStockQuantity}");

                            Console.WriteLine(
                                $"So dong bi anh huong: {rowsAffected}");
                        }
                        else
                        {
                            Console.WriteLine(
                                $"Khong tim thay san pham ID = {productId}.");
                        }
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
