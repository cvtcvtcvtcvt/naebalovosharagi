using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace demowpf
{
    public partial class WindowCatalog : Window
    {
        public static DataTable Cart = new DataTable();
        bool isLoaded = false;

        public WindowCatalog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!Cart.Columns.Contains("Название"))
            {
                Cart.Columns.Add("Название");
                Cart.Columns.Add("Цена");
            }
            isLoaded = true;
            UpdateButtons();
            LoadProducts();
        }

        void LoadProducts(string search = "", string sort = "ASC")
        {
            if (!isLoaded) return;
            wrapPanel.Children.Clear();
            var con = DB.Get(); con.Open();
            string q = $@"
                SELECT p.Id, p.Name, p.Price, p.Stock, p.Discount,
                       p.Description, p.Unit,
                       c.Name as Category, m.Name as Manuf, s.Name as Supplier
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId=c.Id
                LEFT JOIN Manufacturers m ON p.ManufacturerId=m.Id
                LEFT JOIN Suppliers s ON p.SupplierId=s.Id
                WHERE p.Name LIKE '%{search}%'
                ORDER BY p.Price {sort}";
            var da = new SqlDataAdapter(q, con);
            var dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow r in dt.Rows)
                wrapPanel.Children.Add(MakeCard(r));
        }

        // ИМЕНА ФОТО
        string GetImage(int id)
        {
            if (id == 1) return "nike.png";
            if (id == 2) return "nike.png";
            if (id == 3) return "nike.png";
            return "";
        }

        Border MakeCard(DataRow r)
        {
            decimal discount = r["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Discount"]);
            int stock = r["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(r["Stock"]);
            decimal price = Convert.ToDecimal(r["Price"]);
            decimal finalPrice = price * (1 - discount / 100);

            Brush bg = Brushes.White;
            if (stock == 0) bg = Brushes.LightBlue;
            else if (discount > 15) bg = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E8B57"));

            var stack = new StackPanel { Width = 210, Margin = new Thickness(2) };

            // --- ФОТО ---
            string imgPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "images",
                GetImage(Convert.ToInt32(r["Id"])));

            var img = new Image { Height = 90, Width = 205, Stretch = Stretch.Uniform, Margin = new Thickness(0, 0, 0, 4) };
            if (System.IO.File.Exists(imgPath))
                img.Source = new BitmapImage(new Uri(imgPath));
            stack.Children.Add(img);
            // --- КОНЕЦ ФОТО ---

            stack.Children.Add(new TextBlock
            {
                Text = $"{r["Category"]} | {r["Name"]}",
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap
            });
            stack.Children.Add(new TextBlock { Text = "Описание: " + r["Description"], TextWrapping = TextWrapping.Wrap });
            stack.Children.Add(new TextBlock { Text = "Произв: " + r["Manuf"] });
            stack.Children.Add(new TextBlock { Text = "Поставщик: " + r["Supplier"] });

            if (discount > 0)
            {
                var pricePanel = new StackPanel { Orientation = Orientation.Horizontal };
                pricePanel.Children.Add(new TextBlock
                {
                    Text = price + " руб.  ",
                    TextDecorations = TextDecorations.Strikethrough,
                    Foreground = Brushes.Red
                });
                pricePanel.Children.Add(new TextBlock { Text = finalPrice.ToString("F0") + " руб." });
                stack.Children.Add(pricePanel);
            }
            else
            {
                stack.Children.Add(new TextBlock { Text = price + " руб." });
            }

            stack.Children.Add(new TextBlock { Text = $"Ед: {r["Unit"]}   Склад: {stock} шт." });
            stack.Children.Add(new TextBlock { Text = "Скидка: " + discount + "%" });

            var btn = new Button { Content = "В корзину", Tag = r, Margin = new Thickness(0, 4, 0, 0) };
            btn.Click += AddToCart;
            stack.Children.Add(btn);

            return new Border
            {
                Width = 220,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Background = bg,
                Margin = new Thickness(4),
                Padding = new Thickness(6),
                Child = stack
            };
        }

        void AddToCart(object sender, RoutedEventArgs e)
        {
            if (App.UserId == 0) { MessageBox.Show("Сначала войдите!"); return; }
            var r = (DataRow)((Button)sender).Tag;
            Cart.Rows.Add(r["Name"], r["Price"]);
            MessageBox.Show("Добавлено в корзину!");
        }

        public void UpdateButtons()
        {
            if (!isLoaded) return;
            btnLogin.Content = App.UserId == 0 ? "Войти" : "Профиль";
            btnAdmin.Visibility = App.UserRole == "admin" ? Visibility.Visible : Visibility.Collapsed;
            lblUser.Text = App.UserLogin;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (App.UserId == 0) new WindowAuth().ShowDialog();
            else new WindowProfile(this).ShowDialog();
            UpdateButtons();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isLoaded) return;
            LoadProducts(txtSearch.Text, cmbSort.SelectedIndex == 0 ? "ASC" : "DESC");
        }

        private void cmbSort_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (!isLoaded) return;
            LoadProducts(txtSearch.Text, cmbSort.SelectedIndex == 0 ? "ASC" : "DESC");
        }

        private void btnCart_Click(object sender, RoutedEventArgs e) => new WindowCart().ShowDialog();
        private void btnAdmin_Click(object sender, RoutedEventArgs e) => new WindowAdmin().ShowDialog();
        private void btnRefresh_Click(object sender, RoutedEventArgs e) => LoadProducts();
    }
}

