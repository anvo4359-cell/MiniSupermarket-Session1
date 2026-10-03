using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreHappyMartCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "Email", "Phone" },
                values: new object[,]
                {
                    { 11, "Quận 1, TP. Hồ Chí Minh", "Nguyễn Thị Hồng", "hong.nguyen@gmail.com", "0901122334" },
                    { 12, "Quận 2, TP. Hồ Chí Minh", "Trần Minh Khang", "minhkhang@gmail.com", "0902233445" },
                    { 13, "Quận 3, TP. Hồ Chí Minh", "Lê Thị Thanh", "thanh.le@gmail.com", "0903344556" },
                    { 14, "Quận 4, TP. Hồ Chí Minh", "Phạm Quốc Việt", "quocviet@gmail.com", "0904455667" },
                    { 15, "Quận 6, TP. Hồ Chí Minh", "Võ Ngọc Mai", "ngocmai@gmail.com", "0905566778" },
                    { 16, "Quận 8, TP. Hồ Chí Minh", "Đỗ Hoàng Long", "hoanglong@gmail.com", "0906677889" },
                    { 17, "Quận 9, TP. Hồ Chí Minh", "Nguyễn Thảo Vy", "thaovy@gmail.com", "0907788990" },
                    { 18, "Quận 11, TP. Hồ Chí Minh", "Trương Minh Đức", "minhduc@gmail.com", "0908899001" },
                    { 19, "Quận 12, TP. Hồ Chí Minh", "Huỳnh Thanh Trúc", "thanhtruc@gmail.com", "0909900112" },
                    { 20, "Bình Tân, TP. Hồ Chí Minh", "Phan Anh Tuấn", "anhtuan@gmail.com", "0910011223" },
                    { 21, "Bình Chánh, TP. Hồ Chí Minh", "Nguyễn Khánh Linh", "khanhlinh@gmail.com", "0911122334" },
                    { 22, "Nhà Bè, TP. Hồ Chí Minh", "Đặng Minh Quân", "minhquan@gmail.com", "0912233445" },
                    { 23, "Hóc Môn, TP. Hồ Chí Minh", "Bùi Ngọc Anh", "ngocanh@gmail.com", "0913344556" },
                    { 24, "Củ Chi, TP. Hồ Chí Minh", "Lý Gia Hân", "giahan@gmail.com", "0914455667" },
                    { 25, "Thủ Đức, TP. Hồ Chí Minh", "Phạm Minh Nhật", "minhnhat@gmail.com", "0915566778" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 25);
        }
    }
}
