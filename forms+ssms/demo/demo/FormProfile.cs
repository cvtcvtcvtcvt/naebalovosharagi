using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class FormProfile : Form
    {
        FormCatalog parent;

        public FormProfile(FormCatalog p)
        {
            InitializeComponent();
            parent = p;
            // показываем логин текущего пользователя
            lblLogin.Text = "Логин: " + Program.UserLogin;
            lblRole.Text = "Роль: " + Program.UserRole;
        }

        // кнопка Изменить пароль
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPass.Text.Trim() == "") { MessageBox.Show("Введите новый пароль!"); return; }
            if (txtPass.Text.Length > 3) { MessageBox.Show("Пароль макс 3 символа!"); return; }
            var con = DB.Get(); con.Open();
            var cmd = new SqlCommand(
                "UPDATE Users SET Password=@p WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@p", txtPass.Text);
            cmd.Parameters.AddWithValue("@id", Program.UserId);
            cmd.ExecuteNonQuery(); con.Close();
            MessageBox.Show("Пароль изменён!");
        }

        // кнопка Выйти из аккаунта
        private void btnLogout_Click(object sender, EventArgs e)
        {
            Program.UserId = 0;
            Program.UserLogin = "";
            Program.UserRole = "";
            parent.UpdateButtons();
            Close();
        }
    }
}
