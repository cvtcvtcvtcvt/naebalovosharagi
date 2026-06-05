namespace demo
{
    partial class FormCatalog
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.btnCart = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdmin = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();

            // верхняя панель
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 40;
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.cmbSort);
            this.panelTop.Controls.Add(this.btnCart);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnAdmin);
            this.panelTop.Controls.Add(this.btnLogin);
            this.panelTop.Controls.Add(this.lblUser);

            // поле поиска
            this.txtSearch.Location = new System.Drawing.Point(5, 10);
            this.txtSearch.Width = 150;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // выпадающий список сортировки
            this.cmbSort.Location = new System.Drawing.Point(165, 8);
            this.cmbSort.Width = 120;
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Items.AddRange(new object[] { "По цене ↑", "По цене ↓" });
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);

            // кнопка корзина
            this.btnCart.Text = "Корзина";
            this.btnCart.Location = new System.Drawing.Point(295, 8);
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);

            // кнопка обновить
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Location = new System.Drawing.Point(385, 8);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // кнопка админ — скрыта по умолчанию
            this.btnAdmin.Text = "Админ";
            this.btnAdmin.Location = new System.Drawing.Point(475, 8);
            this.btnAdmin.Visible = false;
            this.btnAdmin.Click += new System.EventHandler(this.btnAdmin_Click);

            // кнопка войти/профиль
            this.btnLogin.Text = "Войти";
            this.btnLogin.Location = new System.Drawing.Point(565, 8);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // имя пользователя справа
            this.lblUser.Location = new System.Drawing.Point(660, 12);
            this.lblUser.Width = 120;
            this.lblUser.Text = "";

            // панель карточек
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.AutoScroll = true;
            this.flowPanel.WrapContents = true;

            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Text = "Магазин обуви";
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.panelTop);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdmin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
    }
}
