using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    public partial class WindowAdmin : Window
    {
        DataTable dt = new DataTable();
        SqlDataAdapter da;

        public WindowAdmin() { InitializeComponent(); }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
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
            dgv.ItemsSource = dt.DefaultView;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (da == null) { MessageBox.Show("Сначала загрузите таблицу!"); return; }
            try { da.Update(dt); MessageBox.Show("Сохранено!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (da == null) { MessageBox.Show("Сначала загрузите таблицу!"); return; }
            dt.Rows.Add(dt.NewRow());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgv.SelectedItem == null || da == null) return;
            ((System.Data.DataRowView)dgv.SelectedItem).Row.Delete();
            try { da.Update(dt); dt.AcceptChanges(); MessageBox.Show("Удалено!"); }
            catch (Exception ex) { MessageBox.Show("Ошибка: " + ex.Message); }
        }
    }
}