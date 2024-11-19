using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
namespace Clinic_Software
{
    public partial class KeDonThuoc : Form
    {
        SQL sql = new SQL();
        public KeDonThuoc()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
        }
        private void KeDonThuoc_Load(object sender, EventArgs e)
        {
            tenthuoccbo.DataSource = sql.Get_DataTable("SELECT * FROM THUOC");
            tenthuoccbo.DisplayMember = "TENTHUOC";
            tenthuoccbo.ValueMember = "MAT";
            tenthuoccbo.SelectedIndex = 0;
        }

        private void soluongtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&& e.KeyChar!='\b')
            {
                e.Handled = true;
            }
        }
        private void thembtn_Click(object sender, EventArgs e)
        {
            if(lieuluongtxt.Text == "" || soluongtxt.Text == "")
            {
                MessageBox.Show("Không thể bỏ trống các ô thông tin kê đơn !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int sl = Convert.ToInt32(soluongtxt.Text);
                int tonkho = (int)sql.getExecuteScalar("SELECT SOLUONGTONKHO FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'");
                if (sl > tonkho)
                {
                    MessageBox.Show("Số lượng tồn kho của thuốc này không đủ. Hiện tại chỉ còn " + tonkho.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string mat = tenthuoccbo.SelectedValue.ToString();
                    bool addded = false;
                    string tenth = sql.getExecuteScalar("SELECT TENTHUOC FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'").ToString();
                    if (dataGridView1.RowCount > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == mat)
                            {
                                int slht = Convert.ToInt32(row.Cells[4].Value);
                                int slmoi = sl + slht;
                                row.Cells[4].Value = slmoi.ToString();
                                addded = true;
                                MessageBox.Show("Đã tăng thêm số lượng cho thuốc " + tenth, "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    if (!addded)
                    {
                        int stt = dataGridView1.RowCount +1;
                        string[] newrow = { stt.ToString(), mat,tenth, lieuluongtxt.Text, soluongtxt.Text };
                        dataGridView1.Rows.Add(newrow);
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            tenthuoccbo.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
            lieuluongtxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            soluongtxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void suabtn_Click(object sender, EventArgs e)
        {
            string tenth = sql.getExecuteScalar("SELECT TENTHUOC FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'").ToString();
            bool added = false;
            string mat = tenthuoccbo.SelectedValue.ToString();
            if (dataGridView1.RowCount > 1)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[1].Value.ToString() == mat && row != dataGridView1.CurrentRow)
                    {
                        int slht = Convert.ToInt32(row.Cells[4].Value);
                        int sl = Convert.ToInt32(soluongtxt.Text);
                        int slmoi = sl + slht;
                        row.Cells[4].Value = slmoi.ToString();
                        row.Cells[3].Value = lieuluongtxt.Text;
                        added = true;
                        MessageBox.Show("Đã tăng thêm số lượng cho thuốc " + tenth, "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                int stt = 1;
                foreach(DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = stt;
                    stt++;
                }
            }
            if (!added)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                row.Cells[1].Value = mat;
                row.Cells[2].Value = tenth;
                row.Cells[3].Value = lieuluongtxt.Text;
                row.Cells[4].Value = soluongtxt.Text;
            }
        }

        private void xoabtn_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Cảnh báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                int stt = 1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[0].Value = stt;
                    stt++;
                }
            }
        }
    }
}
