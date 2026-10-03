using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedHappyMartCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "Email", "Phone" },
                values: new object[,]
                {
                    { 1, "Quận 1, TP. Hồ Chí Minh", "Nguyễn Minh Anh", "minhanh@gmail.com", "0901234567" },
                    { 2, "Quận 3, TP. Hồ Chí Minh", "Trần Thị Ngọc", "ngoc.tran@gmail.com", "0912345678" },
                    { 3, "Quận 5, TP. Hồ Chí Minh", "Lê Hoàng Nam", "hoangnam@gmail.com", "0923456789" },
                    { 4, "Quận 10, TP. Hồ Chí Minh", "Phạm Thùy Linh", "thuylinh@gmail.com", "0934567890" },
                    { 5, "Quận 7, TP. Hồ Chí Minh", "Võ Thanh Tùng", "thanhtung@gmail.com", "0945678901" },
                    { 6, "Bình Thạnh, TP. Hồ Chí Minh", "Đặng Kim Chi", "kimchi@gmail.com", "0956789012" },
                    { 7, "Gò Vấp, TP. Hồ Chí Minh", "Nguyễn Quốc Huy", "quochuy@gmail.com", "0967890123" },
                    { 8, "Tân Bình, TP. Hồ Chí Minh", "Huỳnh Mỹ Duyên", "myduyen@gmail.com", "0978901234" },
                    { 9, "Tân Phú, TP. Hồ Chí Minh", "Phan Gia Bảo", "giabao@gmail.com", "0989012345" },
                    { 10, "Thủ Đức, TP. Hồ Chí Minh", "Bùi Thanh Hà", "thanhha@gmail.com", "0990123456" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);
        }
    }
}
