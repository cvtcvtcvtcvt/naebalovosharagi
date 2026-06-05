using System;
using System.Windows.Forms;

namespace demo
{
    public partial class FormCart : Form
    {
        public FormCart()
        {
            InitializeComponent();
            // показываем содержимое корзины
            dgv.DataSource = FormCatalog.Cart;
        }

        // кнопка очистить всю корзину
        private void btnClear_Click(object sender, EventArgs e)
        {
            FormCatalog.Cart.Rows.Clear();
        }
    }
}
