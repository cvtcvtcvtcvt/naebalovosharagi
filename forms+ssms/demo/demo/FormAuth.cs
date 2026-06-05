using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class FormAuth : Form
    {
        FormCatalog parent;

        // parent = null когда открывается при старте
        public FormAuth(FormCatalog p)
        {
            InitializeComponent();
            parent = p;
        }

        // проверка полей перед входом или регистрацией
        bool Check()
        {
            if (txtLogin.Text.Trim() == "") { MessageBox.Show("Введите логин!"); return false; }
            if (txtPass.Text.Trim() == "") { MessageBox.Show("Введите пароль!"); return false; }
            if (txtPass.Text.Length > 3) { MessageBox.Show("Пароль макс 3 символа!"); return false; }
            return true;
        }

        // кнопка Войти
        private void btnVhod_Click(object sender, EventArgs e)
        {
            if (!Check()) return;
            var con = DB.Get(); con.Open();
            var cmd = new SqlCommand(
                "SELECT * FROM Users WHERE Login=@l AND Password=@p", con);
            cmd.Parameters.AddWithValue("@l", txtLogin.Text);
            cmd.Parameters.AddWithValue("@p", txtPass.Text);
            var r = cmd.ExecuteReader();
            if (r.Read())
            {
                Program.UserId = (int)r["Id"];
                Program.UserLogin = r["Login"].ToString();
                Program.UserRole = r["Role"].ToString();
                con.Close();
                // обновляем кнопки в каталоге если он уже открыт
                if (parent != null) parent.UpdateButtons();
                Close();
            }
            else { con.Close(); MessageBox.Show("Неверный логин или пароль!"); }
        }

        // кнопка Зарегистрироваться
        private void btnReg_Click(object sender, EventArgs e)
        {
            if (!Check()) return;
            var con = DB.Get(); con.Open();

            // проверяем не занят ли логин
            var check = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE Login=@l", con);
            check.Parameters.AddWithValue("@l", txtLogin.Text);
            int count = (int)check.ExecuteScalar();
            if (count > 0) { con.Close(); MessageBox.Show("Логин уже занят!"); return; }

            var cmd = new SqlCommand(
                "INSERT INTO Users(Login,Password,Role) VALUES(@l,@p,'user')", con);
            cmd.Parameters.AddWithValue("@l", txtLogin.Text);
            cmd.Parameters.AddWithValue("@p", txtPass.Text);
            cmd.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Готово! Теперь войдите.");
        }

        // кнопка Войти как гость — просто закрываем окно
        private void btnGuest_Click(object sender, EventArgs e) => Close();
    }
}