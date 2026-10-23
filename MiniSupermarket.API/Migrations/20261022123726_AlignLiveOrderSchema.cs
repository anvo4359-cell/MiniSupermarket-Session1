using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AlignLiveOrderSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH(N'dbo.Customers', N'RewardPoints') IS NULL
    ALTER TABLE dbo.Customers ADD RewardPoints int NOT NULL CONSTRAINT DF_Customers_RewardPoints DEFAULT 0;");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.Orders', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Orders
    (
        OrderId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_Orders PRIMARY KEY,
        OrderCode nvarchar(100) NOT NULL,
        CustomerId int NULL,
        CashierUsername nvarchar(200) NOT NULL,
        CreatedDate datetime2 NOT NULL,
        TotalAmount decimal(18,2) NOT NULL,
        FinalAmount decimal(18,2) NOT NULL,
        RewardPoints int NOT NULL,
        CONSTRAINT UQ_Orders_OrderCode UNIQUE (OrderCode),
        CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(CustomerId)
    );
END;");

            migrationBuilder.Sql(@"
IF OBJECT_ID(N'dbo.OrderDetails', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderDetails
    (
        OrderDetailId int IDENTITY(1,1) NOT NULL CONSTRAINT PK_OrderDetails PRIMARY KEY,
        OrderId int NOT NULL,
        ProductId int NOT NULL,
        Quantity int NOT NULL,
        UnitPrice decimal(18,2) NOT NULL,
        Subtotal decimal(18,2) NULL,
        CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId),
        CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
    );
END;");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'UX_Products_Barcode' AND object_id = OBJECT_ID(N'dbo.Products'))
    CREATE UNIQUE INDEX UX_Products_Barcode ON dbo.Products(Barcode);");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Products_Categories_CategoryId')
    ALTER TABLE dbo.Products DROP CONSTRAINT FK_Products_Categories_CategoryId;
ALTER TABLE dbo.Products ADD CONSTRAINT FK_Products_Categories_CategoryId
    FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(CategoryId) ON DELETE NO ACTION;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally non-destructive: the live database may contain transaction history.
        }
    }
}
