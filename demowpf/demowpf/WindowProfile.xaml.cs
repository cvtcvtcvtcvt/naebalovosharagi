using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace demowpf
{
    public partial class WindowProfile : Window
    {
        WindowCatalog parent;

        public WindowProfile(WindowCatalog p)
        {
            InitializeComponent();
            parent = p;
            lblLogin.Text = "Логин: " + App.UserLogin;
            lblRole.Text = "Роль: " + App.UserRole;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass.Password.Trim() == "") { MessageBox.Show("Введите новый пароль!"); return; }
            if (txtPass.Password.Length > 3) { MessageBox.Show("Пароль макс 3 символа!"); return; }
            var con = DB.Get(); con.Open();
            var cmd = new SqlCommand("UPDATE Users SET Password=@p WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@p", txtPass.Password);
            cmd.Parameters.AddWithValue("@id", App.UserId);
            cmd.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Пароль изменён!");
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            App.UserId = 0;
            App.UserLogin = "";
            App.UserRole = "";
            parent.UpdateButtons();
            Close();
        }
    }
}