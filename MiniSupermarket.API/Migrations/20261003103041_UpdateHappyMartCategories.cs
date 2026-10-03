using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHappyMartCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Bánh quy, snack, kẹo và các sản phẩm ăn vặt");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Nước giải khát",
                    "Nước ngọt, nước suối, trà và các loại đồ uống"
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Description",
                value: "Sữa tươi, sữa chua và các sản phẩm từ sữa");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Mì & Thực phẩm ăn liền",
                    "Mì gói, mì ly, cháo và thực phẩm ăn liền"
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Gia vị & Thực phẩm khô",
                    "Nước mắm, dầu ăn, gia vị và thực phẩm khô"
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Description",
                value: "Snack, bánh quy, kẹo dẻo");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Nước giải khát & Trà",
                    "Nước ngọt, nước khoáng, trà"
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Description",
                value: "Sữa tươi, sữa chua, phô mai");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Mì gói & Thực phẩm ăn liền",
                    "Mì ăn liền, phở khô, cháo gói"
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[]
                {
                    "Gia vị & Dầu ăn",
                    "Nước mắm, hạt nêm, dầu thực vật"
                });
        }
    }
}