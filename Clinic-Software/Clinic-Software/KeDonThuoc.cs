using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        string sumvalue;
        public string MABN {  get; set; }
        public KeDonThuoc()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
            this.WindowState = FormWindowState.Maximized;
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
                MessageBox.Show("Không thể bỏ trống các ô thông tin kê đơn !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int sl = Convert.ToInt32(soluongtxt.Text);
                int tonkho = (int)sql.getExecuteScalar("SELECT SOLUONGTONKHO FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'");
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
                            if (slmoi <= tonkho)
                            {
                                row.Cells[4].Value = slmoi.ToString();
                                addded = true;
                                MessageBox.Show("Đã tăng thêm số lượng cho thuốc " + tenth, "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                addded = true;
                                MessageBox.Show("Số lượng tồn kho của thuốc này không đủ. Hiện tại chỉ còn " + (tonkho - slht).ToString(), "Tồn kho không đủ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                if (!addded)
                {
                    if (sl > tonkho)
                    {
                        MessageBox.Show("Số lượng tồn kho của thuốc này không đủ. Hiện tại chỉ còn " + tonkho.ToString(), "Tồn kho không đủ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (sl < 1)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int stt = dataGridView1.RowCount + 1;
                        string[] newrow = { stt.ToString(), mat, tenth, lieuluongtxt.Text, soluongtxt.Text };
                        dataGridView1.Rows.Add(newrow);
                    }
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                tenthuoccbo.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
                lieuluongtxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                soluongtxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
        }

        private void suabtn_Click(object sender, EventArgs e)
        {
            string tenth = sql.getExecuteScalar("SELECT TENTHUOC FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'").ToString();
            string mat = tenthuoccbo.SelectedValue.ToString();
            DataGridViewRow currentrow = dataGridView1.CurrentRow;
            if (dataGridView1.RowCount > 0)
            {
                if (currentrow.Cells[1].Value != mat)
                {
                    MessageBox.Show("Sai tên thuốc! Bạn chỉ có thể sửa số lượng hoặc liều lượng thuốc\nTip: Hãy sửa tên thuốc thành " + currentrow.Cells[2].Value + " để hoàn thành tác vụ", "Sai thông tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int sl = Convert.ToInt32(soluongtxt.Text);
                    if (sl > 0)
                    {
                        int tonkho = (int)sql.getExecuteScalar("SELECT SOLUONGTONKHO FROM THUOC WHERE MAT = '" + tenthuoccbo.SelectedValue.ToString() + "'");
                        if (sl <= tonkho)
                        {
                            currentrow.Cells[3].Value = lieuluongtxt.Text;
                            currentrow.Cells[4].Value = soluongtxt.Text;
                            MessageBox.Show("Đã sửa thành công!", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Số lượng tồn kho của thuốc này không đủ. Hiện tại chỉ còn " + tonkho.ToString(), "Tồn kho không đủ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0", "Sai thông tin", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Chưa có dòng nào trong đơn thuốc", "Đơn thuốc trống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoabtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
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
            else
            {
                MessageBox.Show("Chưa có hàng nào trong đơn thuốc!","Đơn thuốc trống",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Sum()
        {
            int sum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int dongia = (int)sql.getExecuteScalar("SELECT DONGIA FROM THUOC WHERE MAT = '" + row.Cells[1].Value + "'");
                int soluong = int.Parse(row.Cells[4].Value.ToString());
                sum += (dongia * soluong);
            }
            sumvalue = sum.ToString();
            tongtientxt.Text = sum.ToString("C0", new CultureInfo("vi-VN"));
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Sum();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Sum();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Sum();
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Sum();
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn lưu đơn thuốc không?\nLưu ý: Đơn thuốc đã lưu không thể sửa.", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r == DialogResult.Yes) {
                //insert vao donthuoc
                string sttdt = sql.getExecuteScalar("SELECT FORMAT(COUNT(*)+1, '000') FROM DONTHUOC").ToString();
                string madt = "DT" + sttdt;
                DateTime ngaykedon = DateTime.Now.Date;
                string maba = sql.getExecuteScalar("SELECT MABA FROM LSKHAM WHERE MABN = '"+MABN+"' AND NGAYKHAM = '"+ngaykedon.ToString("yyy-MM-dd")+"'").ToString();
                sql.getExecuteNonQuery("INSERT INTO DONTHUOC VALUES ('" + madt + "','" + maba + "','" + ngaykedon.ToString("yyyy-MM-dd") + "','" + sumvalue + "')");
                //insert vao chitietdonthuoc
                foreach (DataGridViewRow row in dataGridView1.Rows) 
                {
                    sql.getExecuteNonQuery("INSERT INTO CHITIETDONTHUOC VALUES ('" + madt + "','" + row.Cells[1].Value.ToString() + "',N'" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() + "')") ;
                }
                MessageBox.Show("Đã lưu thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

    }
}
