using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class WindowRegister : Window
    {
        public WindowRegister()
        {
            InitializeComponent();
        }

        private bool ValidateInput()
        {
            string login = txtLogin.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPass.Password;
            string confirm = txtPassConfirm.Password;

            if (string.IsNullOrWhiteSpace(login))
            {
                lblError.Text = "Введите логин!";
                return false;
            }

            if (login.Length < 3)
            {
                lblError.Text = "Логин должен быть не менее 3 символов!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                lblError.Text = "Введите телефон!";
                return false;
            }

            // Проверка формата телефона (+7XXXXXXXXXX)
            if (!Regex.IsMatch(phone, @"^\+7\d{10}$"))
            {
                lblError.Text = "Телефон должен быть в формате +7XXXXXXXXXX (10 цифр после +7)!";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Введите пароль!";
                return false;
            }

            if (password.Length < 8)
            {
                lblError.Text = "Пароль должен содержать минимум 8 символов!";
                return false;
            }

            if (password != confirm)
            {
                lblError.Text = "Пароли не совпадают!";
                return false;
            }

            return true;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            SqlConnection con = null;
            SqlCommand checkLogin = null;
            SqlCommand checkPhone = null;
            SqlCommand cmd = null;

            try
            {
                con = DB.Get();
                con.Open();

                // Проверка существования логина
                checkLogin = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login=@l", con);
                checkLogin.Parameters.AddWithValue("@l", txtLogin.Text.Trim());
                if ((int)checkLogin.ExecuteScalar() > 0)
                {
                    lblError.Text = "Этот логин уже занят!";
                    return;
                }

                // Проверка существования телефона
                checkPhone = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Phone=@p", con);
                checkPhone.Parameters.AddWithValue("@p", txtPhone.Text.Trim());
                if ((int)checkPhone.ExecuteScalar() > 0)
                {
                    lblError.Text = "Этот телефон уже зарегистрирован!";
                    return;
                }

                // Добавление пользователя
                cmd = new SqlCommand("INSERT INTO Users(Login, Password, Phone, Role) VALUES(@l, @p, @ph, 'user')", con);
                cmd.Parameters.AddWithValue("@l", txtLogin.Text.Trim());
                cmd.Parameters.AddWithValue("@p", txtPass.Password);
                cmd.Parameters.AddWithValue("@ph", txtPhone.Text.Trim());
                cmd.ExecuteNonQuery();

                MessageBox.Show("Регистрация успешно завершена! Теперь вы можете войти.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (checkLogin != null) checkLogin.Dispose();
                if (checkPhone != null) checkPhone.Dispose();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }
        }
    }
}