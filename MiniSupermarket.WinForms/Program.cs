
//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG

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

            // Form chính của chương trình
            Application.Run(new FormMain());
        }
    }
}

