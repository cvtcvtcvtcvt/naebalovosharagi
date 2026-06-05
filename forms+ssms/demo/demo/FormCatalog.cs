using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace demo
{
    public partial class FormCatalog : Form
    {
        public static DataTable Cart = new DataTable();

        public FormCatalog()
        {
            InitializeComponent();
            Cart.Columns.Add("Название");
            Cart.Columns.Add("Цена");
            cmbSort.SelectedIndex = 0;
            LoadProducts();
        }

        void LoadProducts(string search = "", string sort = "ASC")
        {
            flowPanel.Controls.Clear();
            var con = DB.Get(); con.Open();
            string q = $@"
                SELECT p.Id, p.Name, p.Price, p.Stock, p.Discount,
                       p.Description, p.Unit,
                       c.Name as Category,
                       m.Name as Manuf,
                       s.Name as Supplier
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId = c.Id
                LEFT JOIN Manufacturers m ON p.ManufacturerId = m.Id
                LEFT JOIN Suppliers s ON p.SupplierId = s.Id
                WHERE p.Name LIKE '%{search}%'
                ORDER BY p.Price {sort}";
            var da = new SqlDataAdapter(q, con);
            var dt = new DataTable();
            da.Fill(dt);
            con.Close();
            foreach (DataRow r in dt.Rows)
                flowPanel.Controls.Add(MakeCard(r));
        }

        // МЕНЯЙ ТОЛЬКО ИМЕНА ФАЙЛОВ ЗДЕСЬ:
        string GetImage(int id)
        {
            if (id == 1) return "nike.png";
            if (id == 2) return "nike.png";
            if (id == 3) return "nike.png";
            return "";
        }

        Panel MakeCard(DataRow r)
        {
            decimal discount = r["Discount"] == DBNull.Value ? 0 : Convert.ToDecimal(r["Discount"]);
            int stock = r["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(r["Stock"]);
            decimal price = Convert.ToDecimal(r["Price"]);
            decimal finalPrice = price * (1 - discount / 100);

            var p = new Panel
            {
                Width = 220,
                Height = 350,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(8)
            };

            if (stock == 0) p.BackColor = Color.LightBlue;
            else if (discount > 15) p.BackColor = ColorTranslator.FromHtml("#2E8B57");

            int top = 5;

            // фото
            string imgPath = Application.StartupPath + @"\images\" + GetImage(Convert.ToInt32(r["Id"]));
            var pic = new PictureBox { Width = 205, Height = 90, Top = top, Left = 5, SizeMode = PictureBoxSizeMode.Zoom };
            if (System.IO.File.Exists(imgPath))
                pic.Image = Image.FromFile(imgPath);
            p.Controls.Add(pic);
            top += 95;

            p.Controls.Add(new Label { Text = $"{r["Category"]} | {r["Name"]}", Top = top, Left = 5, Width = 205, Height = 30, Font = new Font("Arial", 8, FontStyle.Bold), AutoSize = false });
            top += 32;

            p.Controls.Add(new Label { Text = "Описание: " + r["Description"], Top = top, Left = 5, Width = 205 });
            top += 20;

            p.Controls.Add(new Label { Text = "Произв: " + r["Manuf"], Top = top, Left = 5, Width = 205 });
            top += 20;

            p.Controls.Add(new Label { Text = "Поставщик: " + r["Supplier"], Top = top, Left = 5, Width = 205 });
            top += 20;

            if (discount > 0)
            {
                p.Controls.Add(new Label { Text = price + " руб.", Top = top, Left = 5, Width = 100, ForeColor = Color.Red, Font = new Font("Arial", 8, FontStyle.Strikeout) });
                p.Controls.Add(new Label { Text = finalPrice.ToString("F0") + " руб.", Top = top, Left = 108, Width = 100 });
            }
            else
            {
                p.Controls.Add(new Label { Text = price + " руб.", Top = top, Left = 5, Width = 205 });
            }
            top += 20;

            p.Controls.Add(new Label { Text = $"Ед: {r["Unit"]}   Склад: {stock} шт.", Top = top, Left = 5, Width = 205 });
            top += 20;

            p.Controls.Add(new Label { Text = "Скидка: " + discount + "%", Top = top, Left = 5, Width = 205 });
            top += 25;

            var btn = new Button { Text = "В корзину", Top = top, Left = 5, Width = 205, Tag = r };
            btn.Click += AddToCart;
            p.Controls.Add(btn);

            return p;
        }

        void AddToCart(object sender, EventArgs e)
        {
            if (Program.UserId == 0) { MessageBox.Show("Сначала войдите!"); return; }
            var r = (DataRow)((Button)sender).Tag;
            Cart.Rows.Add(r["Name"], r["Price"]);
            MessageBox.Show("Добавлено в корзину!");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Program.UserId == 0) new FormAuth(this).ShowDialog();
            else new FormProfile(this).ShowDialog();
        }

        public void UpdateButtons()
        {
            btnLogin.Text = Program.UserId == 0 ? "Войти" : "Профиль";
            btnAdmin.Visible = Program.UserRole == "admin";
            lblUser.Text = Program.UserId == 0 ? "" : Program.UserLogin;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
            => LoadProducts(txtSearch.Text, cmbSort.SelectedIndex == 0 ? "ASC" : "DESC");

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
            => LoadProducts(txtSearch.Text, cmbSort.SelectedIndex == 0 ? "ASC" : "DESC");

        private void btnCart_Click(object sender, EventArgs e) => new FormCart().ShowDialog();
        private void btnAdmin_Click(object sender, EventArgs e) => new FormAdmin().ShowDialog();
        private void btnRefresh_Click(object sender, EventArgs e) => LoadProducts();
    }
}