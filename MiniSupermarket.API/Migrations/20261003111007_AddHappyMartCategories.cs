using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddHappyMartCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 7, "Chăm sóc cá nhân", "Dầu gội, sữa tắm, kem đánh răng và sản phẩm chăm sóc cá nhân" },
                    { 8, "Chăm sóc nhà cửa", "Nước giặt, nước rửa chén, chất tẩy rửa và vệ sinh nhà cửa" },
                    { 9, "Thực phẩm tươi sống", "Thịt, cá, trứng và các thực phẩm tươi sống" },
                    { 10, "Rau củ & Trái cây", "Rau xanh, củ quả và trái cây tươi" },
                    { 11, "Thực phẩm đông lạnh", "Thịt đông lạnh, hải sản đông lạnh và thực phẩm chế biến đông lạnh" },
                    { 12, "Đồ hộp & Thực phẩm đóng gói", "Cá hộp, thịt hộp, thực phẩm đóng gói và đóng hộp" },
                    { 13, "Đồ dùng học tập & Văn phòng phẩm", "Bút, vở, giấy và các dụng cụ học tập, văn phòng" },
                    { 14, "Đồ dùng cho mẹ & Bé", "Tã, khăn giấy, sữa và các sản phẩm dành cho mẹ và bé" },
                    { 15, "Đồ dùng tiện ích", "Pin, bật lửa, móc khóa và các sản phẩm tiện ích hàng ngày" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);
        }
    }
}
