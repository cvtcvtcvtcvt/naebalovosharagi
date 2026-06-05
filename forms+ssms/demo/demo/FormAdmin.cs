using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace demo
{
    public partial class FormAdmin : Form
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        public FormAdmin() { InitializeComponent(); }

        // загрузить выбранную таблицу
        private void btnLoad_Click(object sender, EventArgs e)
        {
            var con = DB.Get(); con.Open();
            string q = cmbTable.SelectedIndex == 0
                ? "SELECT * FROM Products"
                : "SELECT * FROM Users";
            da = new SqlDataAdapter(q, con);
            new SqlCommandBuilder(da);
            dt = new DataTable();
            da.Fill(dt);
            con.Close();
            dgv.DataSource = dt;
            dgv.DataError += (s, ex) => { }; // подавляем ошибку форматирования
        }

        // сохранить изменения — редактирование прямо в таблице
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (da == null) { MessageBox.Show("Сначала загрузите таблицу!"); return; }
            try
            {
                dgv.EndEdit();
                da.Update(dt);
                MessageBox.Show("Сохранено!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }

        // добавить пустую строку для заполнения
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (da == null) { MessageBox.Show("Сначала загрузите таблицу!"); return; }
            var row = dt.NewRow();
            if (cmbTable.SelectedIndex == 0) // Products
            {
                row["Name"] = "Новый товар";
                row["Price"] = 0;
                row["Stock"] = 0;
                row["Discount"] = 0;
                row["Unit"] = "шт.";
                row["Description"] = "";
                row["CategoryId"] = 1;
                row["ManufacturerId"] = 1;
                row["SupplierId"] = 1;
            }
            dt.Rows.Add(row);
        }

        // удалить выбранную строку
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || da == null) return;
            dt.Rows[dgv.CurrentRow.Index].Delete();
            try
            {
                da.Update(dt);
                dt.AcceptChanges();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}
