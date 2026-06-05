namespace demo
{
    partial class FormAdmin
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.SuspendLayout();

            // выбор таблицы
            this.cmbTable.Location = new System.Drawing.Point(10, 10);
            this.cmbTable.Width = 130;
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.Items.AddRange(new object[] { "Товары", "Пользователи" });
            this.cmbTable.SelectedIndex = 0;

            // загрузить таблицу
            this.btnLoad.Text = "Загрузить";
            this.btnLoad.Location = new System.Drawing.Point(150, 8);
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // добавить строку
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Location = new System.Drawing.Point(240, 8);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // сохранить изменения
            this.btnSave.Text = "Сохранить";
            this.btnSave.Location = new System.Drawing.Point(330, 8);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // удалить строку
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Location = new System.Drawing.Point(420, 8);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // таблица данных
            this.dgv.Location = new System.Drawing.Point(0, 45);
            this.dgv.Size = new System.Drawing.Size(900, 450);
            this.dgv.AllowUserToAddRows = false;

            this.ClientSize = new System.Drawing.Size(900, 510);
            this.Text = "Админ панель";
            this.Controls.Add(this.cmbTable);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgv);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dgv;
    }
}
