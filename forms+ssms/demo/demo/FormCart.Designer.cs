namespace demo
{
    partial class FormCart
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // таблица товаров в корзине
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Size = new System.Drawing.Size(480, 340);
            this.dgv.AllowUserToAddRows = false;
            this.dgv.ReadOnly = true;

            // кнопка очистить
            this.btnClear.Text = "Очистить корзину";
            this.btnClear.Location = new System.Drawing.Point(170, 350);
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.ClientSize = new System.Drawing.Size(480, 390);
            this.Text = "Корзина";
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnClear);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClear;
    }
}
