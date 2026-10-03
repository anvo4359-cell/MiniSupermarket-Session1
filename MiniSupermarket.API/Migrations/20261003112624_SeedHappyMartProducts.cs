using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedHappyMartProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8934680010011", 1, 18000m, "Bánh Oreo vị socola", 50 },
                    { 2, "8934680010028", 1, 12000m, "Snack khoai tây Lay's", 60 },
                    { 3, "8934680010035", 2, 10000m, "Coca Cola lon 330ml", 100 },
                    { 4, "8934680010042", 2, 7000m, "Nước suối Aquafina 500ml", 120 },
                    { 5, "8934680010059", 3, 8000m, "Sữa tươi Vinamilk 180ml", 80 },
                    { 6, "8934680010066", 3, 7000m, "Sữa chua Vinamilk", 70 },
                    { 7, "8934680010073", 4, 4500m, "Mì Hảo Hảo tôm chua cay", 150 },
                    { 8, "8934680010080", 4, 12000m, "Mì Omachi sườn hầm ngũ quả", 80 },
                    { 9, "8934680010097", 5, 32000m, "Nước mắm Nam Ngư 500ml", 40 },
                    { 10, "8934680010103", 6, 28000m, "Khăn giấy Pulppy", 45 },
                    { 11, "8934680010110", 7, 35000m, "Kem đánh răng P/S 180g", 35 },
                    { 12, "8934680010127", 8, 32000m, "Nước rửa chén Sunlight 750ml", 40 },
                    { 13, "8934680010134", 9, 32000m, "Trứng gà hộp 10 quả", 30 },
                    { 14, "8934680010141", 10, 45000m, "Táo Fuji 1kg", 25 },
                    { 15, "8934680010158", 11, 65000m, "Xúc xích CP 500g", 30 },
                    { 16, "8934680010165", 12, 28000m, "Cá hộp 3 Cô Gái", 35 },
                    { 17, "8934680010172", 13, 5000m, "Bút bi Thiên Long", 100 },
                    { 18, "8934680010189", 15, 25000m, "Pin AA Panasonic", 50 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18);
        }
    }
}
