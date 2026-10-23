# HAPPY MART — Buổi 4: Thiết kế giao diện WinForms Shell và điều hướng phân quyền

## 1. Giới thiệu

Buổi 4 tập trung xây dựng giao diện điều khiển trung tâm cho hệ thống **HAPPY MART – POS thu ngân tích hợp quản lý kho hàng** bằng Windows Forms (WinForms) và C#.

Thay vì mở nhiều cửa sổ độc lập, hệ thống sử dụng kiến trúc **Single-Form Architecture (kiến trúc một cửa sổ chính)**. Các màn hình nghiệp vụ được nhúng vào vùng nội dung trung tâm, giúp giao diện thống nhất, dễ sử dụng và thuận tiện cho việc quản lý các chức năng.

Bên cạnh đó, ứng dụng xây dựng cơ chế **RBAC (Role-Based Access Control – kiểm soát truy cập dựa trên vai trò)** để điều chỉnh các chức năng được phép sử dụng theo ba vai trò: Admin, Cashier và Warehouse.

## 2. Mục tiêu thực hiện

- Thiết kế giao diện chính `FormMainShell` gồm thanh điều hướng bên trái, thanh tiêu đề và vùng nội dung trung tâm.
- Nhúng các Form con vào `panelMainContent` mà không mở thêm cửa sổ độc lập.
- Hiển thị tên đăng nhập và vai trò của nhân viên sau khi đăng nhập.
- Ẩn hoặc hiện các nút chức năng theo quyền của từng vai trò.
- Làm nổi bật nút menu đang được chọn và cập nhật tiêu đề màn hình.
- Xây dựng chức năng đăng xuất, xóa thông tin phiên đăng nhập và quay lại màn hình đăng nhập.
- Định hướng mở rộng các màn hình bán hàng, quản lý sản phẩm, khách hàng, báo cáo và quản trị tài khoản.

## 3. Công nghệ sử dụng

- **C#**: Ngôn ngữ lập trình chính.
- **Windows Forms (.NET)**: Xây dựng giao diện ứng dụng máy tính.
- **Panel, Button, Label, DataGridView**: Các thành phần giao diện.
- **HttpClient**: Giao tiếp với Web API khi các Form nghiệp vụ cần truy xuất dữ liệu.
- **JWT (JSON Web Token – mã thông báo xác thực)**: Duy trì thông tin xác thực khi gọi API, kế thừa từ nội dung Buổi 2.
- **SessionManager**: Quản lý thông tin phiên đăng nhập như tên người dùng, vai trò và mã thông báo xác thực.

## 4. Thiết kế giao diện FormMainShell

Giao diện chính được chia thành ba khu vực:

### 4.1. Thanh điều hướng Sidebar

Thanh Sidebar nằm bên trái, có chiều rộng dự kiến 230 pixel, sử dụng nền xanh đen và chứa các nút chức năng:

- `btnPOS`: Bán hàng tại quầy.
- `btnCategory`: Quản lý danh mục.
- `btnProduct`: Quản lý sản phẩm và kho hàng.
- `btnCustomer`: Quản lý khách hàng.
- `btnReports`: Báo cáo doanh thu.
- `btnUserManage`: Quản trị tài khoản.
- `btnLogout`: Đăng xuất.

Các nút menu được thiết kế đồng nhất. Khi chọn một chức năng, nút tương ứng được đổi màu để người dùng nhận biết màn hình đang hoạt động.

### 4.2. Thanh tiêu đề Top Header

Khu vực `panelTopHeader` hiển thị:

- `lblTitle`: Tên màn hình nghiệp vụ hiện tại.
- `lblUserInfo`: Tên nhân viên và vai trò đang đăng nhập.

Thông tin trên thanh tiêu đề được cập nhật khi người dùng đăng nhập và chuyển đổi giữa các chức năng.

### 4.3. Vùng nội dung Main Workspace

`panelMainContent` là vùng hiển thị các màn hình nghiệp vụ. Khi người dùng chọn một chức năng, Form con được cấu hình `TopLevel = false`, `FormBorderStyle = None` và `Dock = Fill` trước khi thêm vào Panel.

Cách tổ chức này giúp các màn hình nghiệp vụ hiển thị trong cùng một cửa sổ chính.

## 5. Cơ chế nhúng Form con

Phương thức `OpenChildForm()` được sử dụng để quản lý Form đang hiển thị trong `panelMainContent`.

Quy trình điều hướng gồm:

1. Đóng Form con đang hoạt động nếu cần chuyển sang màn hình khác.
2. Đánh dấu nút chức năng vừa được chọn.
3. Cấu hình Form con để có thể nhúng vào Panel.
4. Xóa nội dung cũ trong vùng hiển thị.
5. Thêm và hiển thị Form con mới.
6. Cập nhật tiêu đề màn hình trên Header.

Các màn hình nghiệp vụ dự kiến được tích hợp gồm `FormPOS`, `FormCategoryManagement`, `FormProductManagement`, `FormCustomerManagement`, `FormQuickReport` và `FormUserManagement`.

Việc tích hợp thực tế phụ thuộc vào các Form đã được tạo và những chức năng đã được triển khai trong dự án.

## 6. Phân quyền giao diện theo vai trò

Hệ thống dự kiến hỗ trợ ba vai trò chính:

| Vai trò | Chức năng được phép truy cập |
|---|---|
| Admin | Bán hàng, quản lý danh mục, sản phẩm, khách hàng, báo cáo và tài khoản. |
| Cashier | Bán hàng tại quầy và quản lý khách hàng thành viên. |
| Warehouse | Quản lý danh mục, sản phẩm và nghiệp vụ kho hàng. |

Phương thức `ApplyRolePermissions()` chịu trách nhiệm điều chỉnh trạng thái hiển thị của các nút trên Sidebar theo vai trò hiện tại.

Nếu vai trò không hợp lệ, ứng dụng cần thông báo và kết thúc hoặc chuyển về màn hình đăng nhập theo cách xử lý của dự án.

### Tài khoản kiểm thử theo đề bài

Các tài khoản mẫu được cung cấp để kiểm tra cơ chế phân quyền:

| Tài khoản | Vai trò | Phạm vi kiểm thử |
|---|---|---|
| `admin01` | Admin | Hiển thị đầy đủ chức năng. |
| `cashier01` | Cashier | Chỉ hiển thị Bán hàng và Khách hàng. |
| `ware01` | Warehouse | Chỉ hiển thị Danh mục và Sản phẩm. |

Đề bài còn cung cấp các tài khoản mẫu khác thuộc ba vai trò để mở rộng kiểm thử.

**Lưu ý:** Danh sách tài khoản trên là dữ liệu mẫu của bài thực hành. Khả năng đăng nhập thành công phụ thuộc vào dữ liệu tài khoản thực tế được cấu hình trong hệ thống.

## 7. Điều hướng mặc định theo vai trò

Sau khi đăng nhập, `FormMainShell` hiển thị màn hình phù hợp với vai trò:

- **Admin:** Màn hình quản lý danh mục theo luồng điều hướng được mô tả trong phần triển khai cơ bản.
- **Cashier:** Màn hình khách hàng theo đoạn mã điều hướng ban đầu; phần hướng dẫn mở rộng yêu cầu màn hình POS làm màn hình mặc định.
- **Warehouse:** Màn hình quản lý danh mục trong đoạn mã ban đầu; bảng tổng kết cuối bài yêu cầu màn hình quản lý sản phẩm.

Các hướng dẫn trong tài liệu có một số điểm chưa thống nhất về màn hình mặc định của Cashier và Warehouse. Khi triển khai hoàn chỉnh, cần chọn một quy tắc thống nhất theo yêu cầu cuối cùng của giảng viên và mã nguồn thực tế.

## 8. Chức năng đăng xuất

Nút `btnLogout` thực hiện quy trình:

1. Hiển thị hộp thoại xác nhận đăng xuất.
2. Xóa `SessionManager.JwtToken`.
3. Xóa thông tin tên đăng nhập và vai trò.
4. Ẩn màn hình chính.
5. Mở lại `FormLogin`.
6. Đóng phiên làm việc hiện tại sau khi xử lý đăng nhập lại.

Mục tiêu là tránh tiếp tục sử dụng phiên làm việc cũ sau khi người dùng đăng xuất.

## 9. Các màn hình nghiệp vụ mở rộng

### 9.1. FormPOS – Bán hàng tại quầy

Màn hình POS (Point of Sale – điểm bán hàng) dự kiến hỗ trợ:

- Quét hoặc nhập mã vạch sản phẩm.
- Hiển thị danh sách sản phẩm trong giỏ hàng.
- Tính tổng tiền cần thanh toán.
- Nhập số tiền khách đưa và tính tiền thừa.
- Gửi yêu cầu thanh toán đến Web API.
- Hỗ trợ thông tin khách hàng thành viên.

### 9.2. FormProductManagement – Quản lý sản phẩm

Màn hình dự kiến cung cấp các chức năng:

- Hiển thị danh sách sản phẩm.
- Tìm kiếm theo mã vạch.
- Lọc sản phẩm theo danh mục.
- Thêm, sửa và xóa sản phẩm.
- Quản lý giá bán, số lượng tồn kho và danh mục liên quan.

### 9.3. FormUserManagement – Quản trị tài khoản

Màn hình dành cho Admin, dự kiến hỗ trợ:

- Hiển thị danh sách tài khoản nhân viên.
- Tạo tài khoản mới.
- Chọn vai trò Admin, Cashier hoặc Warehouse.
- Mở rộng chức năng đặt lại mật khẩu và khóa hoặc mở khóa tài khoản nếu API hỗ trợ.

### 9.4. FormQuickReport – Báo cáo doanh thu

Màn hình báo cáo mở rộng dự kiến hiển thị:

- Tổng số hóa đơn.
- Tổng doanh thu.
- Mặt hàng bán chạy nhất.
- Ngày cần xem báo cáo.

Chức năng xem dữ liệu tài chính chỉ dành cho Admin theo yêu cầu bài thực hành.

**Lưu ý:** Các màn hình và chức năng trong phần mở rộng chỉ được xem là đã hoàn thành khi đã được triển khai, kết nối API và kiểm thử thành công trong mã nguồn dự án.

## 10. Kiểm soát quyền truy cập

Bài thực hành yêu cầu kiểm tra quyền ở hai lớp:

- **Lớp giao diện:** Ẩn các nút không thuộc quyền sử dụng của vai trò hiện tại.
- **Lớp xử lý chức năng:** Kiểm tra quyền trước khi thực hiện các thao tác nhạy cảm, chẳng hạn xem báo cáo doanh thu hoặc quản trị tài khoản.

Ẩn nút trên giao diện không thay thế được việc bảo vệ dữ liệu ở máy chủ. Những API nhạy cảm cần kiểm tra quyền truy cập ở phía Web API để tránh người dùng gọi trực tiếp endpoint khi không có quyền.

## 11. Kiểm thử và đánh giá

Các nội dung cần kiểm thử:

- Đăng nhập bằng tài khoản Admin, Cashier và Warehouse.
- Kiểm tra các nút được hiển thị theo đúng vai trò.
- Chuyển đổi giữa các màn hình và kiểm tra tiêu đề.
- Xác nhận Form con hiển thị trong `panelMainContent`, không mở thành cửa sổ riêng.
- Kiểm tra nút menu được chọn có màu nổi bật.
- Đăng xuất và xác nhận thông tin phiên đăng nhập được xóa.
- Thử truy cập chức năng báo cáo bằng tài khoản không phải Admin.
- Kiểm tra các API có dữ liệu nhạy cảm đã được bảo vệ quyền truy cập ở máy chủ.

### Checklist kết quả

- [ ] Hoàn thành giao diện `FormMainShell`.
- [ ] Nhúng được Form con vào `panelMainContent`.
- [ ] Hiển thị thông tin nhân viên và vai trò.
- [ ] Phân quyền Sidebar theo ba vai trò.
- [ ] Cập nhật tiêu đề và màu nút menu khi điều hướng.
- [ ] Đăng xuất và quay lại màn hình đăng nhập.
- [ ] Kiểm tra quyền truy cập chức năng báo cáo.
- [ ] Xác nhận các màn hình mở rộng hoạt động với API nếu đã triển khai.

## 12. Kết luận

Buổi 4 xây dựng nền tảng giao diện và điều hướng cho hệ thống HAPPY MART. Kiến trúc một cửa sổ chính giúp các chức năng nghiệp vụ được tổ chức tập trung, trong khi cơ chế phân quyền theo vai trò hỗ trợ giới hạn các chức năng hiển thị cho từng nhóm người dùng.

Nội dung này là bước chuẩn bị để tích hợp các màn hình bán hàng, quản lý sản phẩm, quản lý khách hàng, báo cáo và quản trị tài khoản trong những buổi thực hành tiếp theo.

**Ghi chú:** README mô tả mục tiêu và hướng dẫn của Buổi 4. Trước khi xác nhận chức năng đã hoàn thành, cần đối chiếu với mã nguồn thực tế của dự án HAPPY MART.
