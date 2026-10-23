using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        // ============================================================
        // CÁC BẢNG TRONG DATABASE - HAPPY MART
        // ============================================================

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        // ============================================================
        // DỮ LIỆU MẪU
        // ============================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(product => product.Barcode)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasIndex(order => order.OrderCode)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Customer)
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(detail => detail.Order)
                .WithMany(order => order.Details)
                .HasForeignKey(detail => detail.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(detail => detail.Product)
                .WithMany()
                .HasForeignKey(detail => detail.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // --------------------------------------------------------
            // DỮ LIỆU MẪU CUSTOMER - CỬA HÀNG TIỆN LỢI HAPPY MART
            // --------------------------------------------------------

            modelBuilder.Entity<Customer>().HasData(
     new Customer
     {
         CustomerId = 1,
         CustomerName = "Nguyễn Minh Anh",
         Phone = "0901234567",
         Address = "25 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP. Hồ Chí Minh",
         Email = "minhanh@gmail.com"
     },

     new Customer
     {
         CustomerId = 2,
         CustomerName = "Trần Thị Ngọc",
         Phone = "0912345678",
         Address = "128 Võ Văn Tần, Phường 6, Quận 3, TP. Hồ Chí Minh",
         Email = "ngoc.tran@gmail.com"
     },

     new Customer
     {
         CustomerId = 3,
         CustomerName = "Lê Hoàng Nam",
         Phone = "0923456789",
         Address = "56 Nguyễn Trãi, Phường 3, Quận 5, TP. Hồ Chí Minh",
         Email = "hoangnam@gmail.com"
     },

     new Customer
     {
         CustomerId = 4,
         CustomerName = "Phạm Thùy Linh",
         Phone = "0934567890",
         Address = "214 Thành Thái, Phường 12, Quận 10, TP. Hồ Chí Minh",
         Email = "thuylinh@gmail.com"
     },

     new Customer
     {
         CustomerId = 5,
         CustomerName = "Võ Thanh Tùng",
         Phone = "0945678901",
         Address = "89 Nguyễn Thị Thập, Phường Tân Phú, Quận 7, TP. Hồ Chí Minh",
         Email = "thanhtung@gmail.com"
     },

     new Customer
     {
         CustomerId = 6,
         CustomerName = "Đặng Kim Chi",
         Phone = "0956789012",
         Address = "102 Xô Viết Nghệ Tĩnh, Phường 21, Quận Bình Thạnh, TP. Hồ Chí Minh",
         Email = "kimchi@gmail.com"
     },

     new Customer
     {
         CustomerId = 7,
         CustomerName = "Nguyễn Quốc Huy",
         Phone = "0967890123",
         Address = "45 Quang Trung, Phường 10, Quận Gò Vấp, TP. Hồ Chí Minh",
         Email = "quochuy@gmail.com"
     },

     new Customer
     {
         CustomerId = 8,
         CustomerName = "Huỳnh Mỹ Duyên",
         Phone = "0978901234",
         Address = "76 Cộng Hòa, Phường 4, Quận Tân Bình, TP. Hồ Chí Minh",
         Email = "myduyen@gmail.com"
     },

     new Customer
     {
         CustomerId = 9,
         CustomerName = "Phan Gia Bảo",
         Phone = "0989012345",
         Address = "135 Lũy Bán Bích, Phường Hòa Thạnh, Quận Tân Phú, TP. Hồ Chí Minh",
         Email = "giabao@gmail.com"
     },

     new Customer
     {
         CustomerId = 10,
         CustomerName = "Bùi Thanh Hà",
         Phone = "0990123456",
         Address = "88 Võ Văn Ngân, Phường Linh Chiểu, TP. Thủ Đức, TP. Hồ Chí Minh",
         Email = "thanhha@gmail.com"
     },

     new Customer
     {
         CustomerId = 11,
         CustomerName = "Nguyễn Thị Hồng",
         Phone = "0901122334",
         Address = "42 Trần Hưng Đạo, Phường Cầu Ông Lãnh, Quận 1, TP. Hồ Chí Minh",
         Email = "hong.nguyen@gmail.com"
     },

     new Customer
     {
         CustomerId = 12,
         CustomerName = "Trần Minh Khang",
         Phone = "0902233445",
         Address = "156 Nguyễn Duy Trinh, Phường Bình Trưng Tây, TP. Thủ Đức, TP. Hồ Chí Minh",
         Email = "minhkhang@gmail.com"
     },

     new Customer
     {
         CustomerId = 13,
         CustomerName = "Lê Thị Thanh",
         Phone = "0903344556",
         Address = "73 Nam Kỳ Khởi Nghĩa, Phường Võ Thị Sáu, Quận 3, TP. Hồ Chí Minh",
         Email = "thanh.le@gmail.com"
     },

     new Customer
     {
         CustomerId = 14,
         CustomerName = "Phạm Quốc Việt",
         Phone = "0904455667",
         Address = "91 Hoàng Diệu, Phường 12, Quận 4, TP. Hồ Chí Minh",
         Email = "quocviet@gmail.com"
     },

     new Customer
     {
         CustomerId = 15,
         CustomerName = "Võ Ngọc Mai",
         Phone = "0905566778",
         Address = "64 Hậu Giang, Phường 4, Quận 6, TP. Hồ Chí Minh",
         Email = "ngocmai@gmail.com"
     },

     new Customer
     {
         CustomerId = 16,
         CustomerName = "Đỗ Hoàng Long",
         Phone = "0906677889",
         Address = "235 Phạm Thế Hiển, Phường 6, Quận 8, TP. Hồ Chí Minh",
         Email = "hoanglong@gmail.com"
     },

     new Customer
     {
         CustomerId = 17,
         CustomerName = "Nguyễn Thảo Vy",
         Phone = "0907788990",
         Address = "118 Lê Văn Việt, Phường Hiệp Phú, TP. Thủ Đức, TP. Hồ Chí Minh",
         Email = "thaovy@gmail.com"
     },

     new Customer
     {
         CustomerId = 18,
         CustomerName = "Trương Minh Đức",
         Phone = "0908899001",
         Address = "52 Lãnh Binh Thăng, Phường 13, Quận 11, TP. Hồ Chí Minh",
         Email = "minhduc@gmail.com"
     },

     new Customer
     {
         CustomerId = 19,
         CustomerName = "Huỳnh Thanh Trúc",
         Phone = "0909900112",
         Address = "183 Nguyễn Văn Quá, Phường Đông Hưng Thuận, Quận 12, TP. Hồ Chí Minh",
         Email = "thanhtruc@gmail.com"
     },

     new Customer
     {
         CustomerId = 20,
         CustomerName = "Phan Anh Tuấn",
         Phone = "0910011223",
         Address = "72 Tên Lửa, Phường Bình Trị Đông B, Quận Bình Tân, TP. Hồ Chí Minh",
         Email = "anhtuan@gmail.com"
     },

     new Customer
     {
         CustomerId = 21,
         CustomerName = "Nguyễn Khánh Linh",
         Phone = "0911122334",
         Address = "36 Nguyễn Hữu Trí, Thị trấn Tân Túc, Huyện Bình Chánh, TP. Hồ Chí Minh",
         Email = "khanhlinh@gmail.com"
     },

     new Customer
     {
         CustomerId = 22,
         CustomerName = "Đặng Minh Quân",
         Phone = "0912233445",
         Address = "27 Nguyễn Văn Tạo, Xã Long Thới, Huyện Nhà Bè, TP. Hồ Chí Minh",
         Email = "minhquan@gmail.com"
     },

     new Customer
     {
         CustomerId = 23,
         CustomerName = "Bùi Ngọc Anh",
         Phone = "0913344556",
         Address = "94 Phan Văn Hớn, Xã Bà Điểm, Huyện Hóc Môn, TP. Hồ Chí Minh",
         Email = "ngocanh@gmail.com"
     },

     new Customer
     {
         CustomerId = 24,
         CustomerName = "Lý Gia Hân",
         Phone = "0914455667",
         Address = "215 Tỉnh lộ 8, Thị trấn Củ Chi, Huyện Củ Chi, TP. Hồ Chí Minh",
         Email = "giahan@gmail.com"
     },

     new Customer
     {
         CustomerId = 25,
         CustomerName = "Phạm Minh Nhật",
         Phone = "0915566778",
         Address = "145 Võ Văn Ngân, Phường Bình Thọ, TP. Thủ Đức, TP. Hồ Chí Minh",
         Email = "minhnhat@gmail.com"
     }
 );

            // --------------------------------------------------------
            // DỮ LIỆU MẪU PRODUCT - KHO HÀNG HAPPY MART
            // --------------------------------------------------------

            modelBuilder.Entity<Product>().HasData(

                // 1. Bánh kẹo & đồ ăn vặt
                new Product
                {
                    ProductId = 1,
                    Barcode = "8934680010011",
                    ProductName = "Bánh Oreo vị socola",
                    Price = 18000,
                    StockQuantity = 50,
                    CategoryId = 1
                },

                new Product
                {
                    ProductId = 2,
                    Barcode = "8934680010028",
                    ProductName = "Snack khoai tây Lay's",
                    Price = 12000,
                    StockQuantity = 60,
                    CategoryId = 1
                },

                // 2. Nước giải khát
                new Product
                {
                    ProductId = 3,
                    Barcode = "8934680010035",
                    ProductName = "Coca Cola lon 330ml",
                    Price = 10000,
                    StockQuantity = 100,
                    CategoryId = 2
                },

                new Product
                {
                    ProductId = 4,
                    Barcode = "8934680010042",
                    ProductName = "Nước suối Aquafina 500ml",
                    Price = 7000,
                    StockQuantity = 120,
                    CategoryId = 2
                },

                // 3. Sữa & sản phẩm từ sữa
                new Product
                {
                    ProductId = 5,
                    Barcode = "8934680010059",
                    ProductName = "Sữa tươi Vinamilk 180ml",
                    Price = 8000,
                    StockQuantity = 80,
                    CategoryId = 3
                },

                new Product
                {
                    ProductId = 6,
                    Barcode = "8934680010066",
                    ProductName = "Sữa chua Vinamilk",
                    Price = 7000,
                    StockQuantity = 70,
                    CategoryId = 3
                },

                // 4. Mì & thực phẩm ăn liền
                new Product
                {
                    ProductId = 7,
                    Barcode = "8934680010073",
                    ProductName = "Mì Hảo Hảo tôm chua cay",
                    Price = 4500,
                    StockQuantity = 150,
                    CategoryId = 4
                },

                new Product
                {
                    ProductId = 8,
                    Barcode = "8934680010080",
                    ProductName = "Mì Omachi sườn hầm ngũ quả",
                    Price = 12000,
                    StockQuantity = 80,
                    CategoryId = 4
                },

                // 5. Gia vị & thực phẩm khô
                new Product
                {
                    ProductId = 9,
                    Barcode = "8934680010097",
                    ProductName = "Nước mắm Nam Ngư 500ml",
                    Price = 32000,
                    StockQuantity = 40,
                    CategoryId = 5
                },

                // 6. Đồ dùng gia đình
                new Product
                {
                    ProductId = 10,
                    Barcode = "8934680010103",
                    ProductName = "Khăn giấy Pulppy",
                    Price = 28000,
                    StockQuantity = 45,
                    CategoryId = 6
                },

                // 7. Chăm sóc cá nhân
                new Product
                {
                    ProductId = 11,
                    Barcode = "8934680010110",
                    ProductName = "Kem đánh răng P/S 180g",
                    Price = 35000,
                    StockQuantity = 35,
                    CategoryId = 7
                },

                // 8. Chăm sóc nhà cửa
                new Product
                {
                    ProductId = 12,
                    Barcode = "8934680010127",
                    ProductName = "Nước rửa chén Sunlight 750ml",
                    Price = 32000,
                    StockQuantity = 40,
                    CategoryId = 8
                },

                // 9. Thực phẩm tươi sống
                new Product
                {
                    ProductId = 13,
                    Barcode = "8934680010134",
                    ProductName = "Trứng gà hộp 10 quả",
                    Price = 32000,
                    StockQuantity = 30,
                    CategoryId = 9
                },

                // 10. Rau củ & trái cây
                new Product
                {
                    ProductId = 14,
                    Barcode = "8934680010141",
                    ProductName = "Táo Fuji 1kg",
                    Price = 45000,
                    StockQuantity = 25,
                    CategoryId = 10
                },

                // 11. Thực phẩm đông lạnh
                new Product
                {
                    ProductId = 15,
                    Barcode = "8934680010158",
                    ProductName = "Xúc xích CP 500g",
                    Price = 65000,
                    StockQuantity = 30,
                    CategoryId = 11
                },

                // 12. Đồ hộp & thực phẩm đóng gói
                new Product
                {
                    ProductId = 16,
                    Barcode = "8934680010165",
                    ProductName = "Cá hộp 3 Cô Gái",
                    Price = 28000,
                    StockQuantity = 35,
                    CategoryId = 12
                },

                // 13. Đồ dùng học tập & văn phòng phẩm
                new Product
                {
                    ProductId = 17,
                    Barcode = "8934680010172",
                    ProductName = "Bút bi Thiên Long",
                    Price = 5000,
                    StockQuantity = 100,
                    CategoryId = 13
                },

                // 15. Đồ dùng tiện ích
                new Product
                {
                    ProductId = 18,
                    Barcode = "8934680010189",
                    ProductName = "Pin AA Panasonic",
                    Price = 25000,
                    StockQuantity = 50,
                    CategoryId = 15
                }
            );

            // ========================================================
            // DỮ LIỆU MẪU USER - 15 TÀI KHOẢN HAPPY MART
            // ========================================================

            modelBuilder.Entity<User>().HasData(

                // ================= ADMIN =================

                new User
                {
                    UserId = 1,
                    Username = "admin01",
                    Password = "123456",
                    Role = "Admin",
                    FullName = "Nguyễn Quản Trị"
                },

                new User
                {
                    UserId = 2,
                    Username = "admin02",
                    Password = "123456",
                    Role = "Admin",
                    FullName = "Trần Giám Đốc"
                },

                new User
                {
                    UserId = 3,
                    Username = "admin03",
                    Password = "123456",
                    Role = "Admin",
                    FullName = "Lê Quản Lý"
                },

                new User
                {
                    UserId = 4,
                    Username = "admin04",
                    Password = "123456",
                    Role = "Admin",
                    FullName = "Phạm Quản Trị"
                },

                // ================= CASHIER =================

                new User
                {
                    UserId = 5,
                    Username = "cashier01",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Lê Thu Ngân"
                },

                new User
                {
                    UserId = 6,
                    Username = "cashier02",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Phạm Bán Hàng"
                },

                new User
                {
                    UserId = 7,
                    Username = "cashier03",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Hoàng Thu Ngân"
                },

                new User
                {
                    UserId = 8,
                    Username = "cashier04",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Vũ Thị Quầy"
                },

                new User
                {
                    UserId = 9,
                    Username = "cashier05",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Đỗ Bán Lẻ"
                },

                new User
                {
                    UserId = 10,
                    Username = "cashier06",
                    Password = "123456",
                    Role = "Cashier",
                    FullName = "Nguyễn Thu Ngân"
                },

                // ================= WAREHOUSE =================

                new User
                {
                    UserId = 11,
                    Username = "ware01",
                    Password = "123456",
                    Role = "Warehouse",
                    FullName = "Ngô Quản Kho"
                },

                new User
                {
                    UserId = 12,
                    Username = "ware02",
                    Password = "123456",
                    Role = "Warehouse",
                    FullName = "Bùi Kiểm Kê"
                },

                new User
                {
                    UserId = 13,
                    Username = "ware03",
                    Password = "123456",
                    Role = "Warehouse",
                    FullName = "Dương Thủ Kho"
                },

                new User
                {
                    UserId = 14,
                    Username = "ware04",
                    Password = "123456",
                    Role = "Warehouse",
                    FullName = "Trần Nhập Kho"
                },

                new User
                {
                    UserId = 15,
                    Username = "ware05",
                    Password = "123456",
                    Role = "Warehouse",
                    FullName = "Lê Xuất Kho"
                }
            );

            // --------------------------------------------------------
            // DỮ LIỆU MẪU CATEGORY - CỬA HÀNG TIỆN LỢI HAPPY MART
            // --------------------------------------------------------

            modelBuilder.Entity<Category>().HasData(
                 new Category
                 {
                     CategoryId = 1,
                     CategoryName = "Bánh kẹo & Đồ ăn vặt",
                     Description = "Bánh quy, snack, kẹo và các sản phẩm ăn vặt"
                 },

                 new Category
                 {
                     CategoryId = 2,
                     CategoryName = "Nước giải khát",
                     Description = "Nước ngọt, nước suối, trà và các loại đồ uống"
                 },

                 new Category
                 {
                     CategoryId = 3,
                     CategoryName = "Sữa & Sản phẩm từ sữa",
                     Description = "Sữa tươi, sữa chua và các sản phẩm từ sữa"
                 },

                 new Category
                 {
                     CategoryId = 4,
                     CategoryName = "Mì & Thực phẩm ăn liền",
                     Description = "Mì gói, mì ly, cháo và thực phẩm ăn liền"
                 },

                 new Category
                 {
                     CategoryId = 5,
                     CategoryName = "Gia vị & Thực phẩm khô",
                     Description = "Nước mắm, dầu ăn, gia vị và thực phẩm khô"
                 },

                 new Category
                 {
                     CategoryId = 6,
                     CategoryName = "Đồ dùng gia đình",
                     Description = "Khăn giấy, túi rác và các vật dụng gia đình"
                 },

                 new Category
                 {
                     CategoryId = 7,
                     CategoryName = "Chăm sóc cá nhân",
                     Description = "Dầu gội, sữa tắm, kem đánh răng và sản phẩm chăm sóc cá nhân"
                 },

                 new Category
                 {
                     CategoryId = 8,
                     CategoryName = "Chăm sóc nhà cửa",
                     Description = "Nước giặt, nước rửa chén, chất tẩy rửa và vệ sinh nhà cửa"
                 },

                 new Category
                 {
                     CategoryId = 9,
                     CategoryName = "Thực phẩm tươi sống",
                     Description = "Thịt, cá, trứng và các thực phẩm tươi sống"
                 },

                 new Category
                 {
                     CategoryId = 10,
                     CategoryName = "Rau củ & Trái cây",
                     Description = "Rau xanh, củ quả và trái cây tươi"
                 },

                 new Category
                 {
                     CategoryId = 11,
                     CategoryName = "Thực phẩm đông lạnh",
                     Description = "Thịt đông lạnh, hải sản đông lạnh và thực phẩm chế biến đông lạnh"
                 },

                 new Category
                 {
                     CategoryId = 12,
                     CategoryName = "Đồ hộp & Thực phẩm đóng gói",
                     Description = "Cá hộp, thịt hộp, thực phẩm đóng gói và đóng hộp"
                 },

                 new Category
                 {
                     CategoryId = 13,
                     CategoryName = "Đồ dùng học tập & Văn phòng phẩm",
                     Description = "Bút, vở, giấy và các dụng cụ học tập, văn phòng"
                 },

                 new Category
                 {
                     CategoryId = 14,
                     CategoryName = "Đồ dùng cho mẹ & Bé",
                     Description = "Tã, khăn giấy, sữa và các sản phẩm dành cho mẹ và bé"
                 },

                 new Category
                 {
                     CategoryId = 15,
                     CategoryName = "Đồ dùng tiện ích",
                     Description = "Pin, bật lửa, móc khóa và các sản phẩm tiện ích hàng ngày"
                 }
             );
        }
    }
}