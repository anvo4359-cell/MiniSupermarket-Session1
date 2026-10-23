//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAY THUCHIEN: 03/10/2026
//********** DE TAI: HAPPY MART - POS THU NGAN TICH HOP QUAN LY KHO

using System;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cấu hình ứng dụng WinForms
            ApplicationConfiguration.Initialize();

            // Mở form đăng nhập trước
            using (FormLogin loginForm = new FormLogin())
            {
                // Nếu đăng nhập thành công (DialogResult.OK)
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Khởi chạy FormMain chính thức của hệ thống POS & Quản lý kho
                    Application.Run(new FormMain());
                }
            }
        }
    }
}