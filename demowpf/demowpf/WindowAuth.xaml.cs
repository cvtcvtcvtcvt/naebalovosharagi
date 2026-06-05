using System;
using System.Data.SqlClient;
using System.Windows;

namespace demowpf
{
    public partial class WindowAuth : Window
    {
        public WindowAuth()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPass.Password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }

            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader r = null;

            try
            {
                con = DB.Get();
                con.Open();
                cmd = new SqlCommand("SELECT Id, Login, Role FROM Users WHERE Login=@l AND Password=@p", con);
                cmd.Parameters.AddWithValue("@l", txtLogin.Text);
                cmd.Parameters.AddWithValue("@p", txtPass.Password);

                r = cmd.ExecuteReader();
                if (r.Read())
                {
                    App.UserId = (int)r["Id"];
                    App.UserLogin = r["Login"].ToString();
                    App.UserRole = r["Role"].ToString();

                    var catalog = new WindowCatalog();
                    catalog.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                if (r != null) r.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var regWindow = new WindowRegister();
            regWindow.ShowDialog();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            new WindowCatalog().Show();
            Close();
        }
    }
}