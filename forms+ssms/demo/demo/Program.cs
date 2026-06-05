using System;
using System.Windows.Forms;

namespace demo
{
    static class Program
    {
        public static int UserId = 0;
        public static string UserLogin = "";
        public static string UserRole = "";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCatalog());
        }
    }
}