using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinic_Software
{
    public partial class Nhansu : Form
    {
        SQL sql = new SQL();
        SqlConnection conn = new SqlConnection();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public Nhansu()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
        }

        private void Nhansu_FormClosing(object sender, FormClosingEventArgs e)
        {

            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        public bool KiemTraThongTinBS()
        {
            if (txtmabs.Text == "" || txthotenbs.Text == "" || dateTimePicker1.ToString() == DateTime.Now.Year.ToString()
                || txtdiachibs.Text == "" || txtsdtbs.Text == "" || txtcccdbs.Text == "" || txtemailbs.Text == ""
                || txthochambs.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        public void Load_DataGridBS()
        {
            DataTable dt = new DataTable();
            string sql_bs = "select * from BACSI";
            dt = sql.Get_DataTable(sql_bs);
            dataGridView1.DataSource = dt;
        }

        private void Nhansu_Load(object sender, EventArgs e)
        {
            txtmabs.Focus();
            btnluubs.Enabled = false;
            btnluunv.Enabled = false;

            rdoBacsi.CheckedChanged += new EventHandler(rdoBacsi_CheckedChanged);
            rdoNhanvien.CheckedChanged += new EventHandler(rdoBacsi_CheckedChanged);
        }

        private void btnThembs_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTinBS())
            {
                string sqlbs = "select * from BACSI";
                DataRow newrow = dt.NewRow();
                newrow["Mabs"] = txtmabs.Text;
                newrow["hoten"] = txthotenbs.Text;
                newrow["ngaysinh"] = dateTimePicker1.Text;
                newrow["diachi"] = txtdiachibs.Text;
                newrow["sdt"] = txtsdtbs.Text;
                newrow["cccd"] = txtcccdbs.Text;
                newrow["email"] = txtemailbs.Text;
                newrow["hocham"] = txthochambs.Text;
                string gioiTinh = rdonam.Checked ? "Nam" : rdonu.Checked ? "Nữ" : DBNull.Value.ToString();
                newrow["gioitinh"] = gioiTinh;

                dt.Rows.Add(newrow);
                int kq = sql.Update_DataTable(sqlbs, dt);
                if (kq == 0)
                {
                    MessageBox.Show("Them khong thanh cong", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Them thanh cong", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmabs.Clear();
                    txthotenbs.Clear();
                    dateTimePicker1.Value = DateTime.Now;
                    txtdiachibs.Clear();
                    txtsdtbs.Clear();
                    txtcccdbs.Clear();
                    txtemailbs.Clear();
                    txthochambs.Clear();
                    rdonu.Checked = false;
                    rdonam.Checked = false;
                    dataGridView1.DataSource = dt;
                }
                Load_DataGridBS();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (rdoBacsi.Checked)
                {
                    txtmabs.Text = row.Cells["Mabs"].Value?.ToString();
                    txthotenbs.Text = row.Cells["hoten"].Value?.ToString();
                    dateTimePicker1.Text = row.Cells["ngaysinh"].Value?.ToString();
                    txtdiachibs.Text = row.Cells["diachi"].Value?.ToString();
                    txtsdtbs.Text = row.Cells["sdt"].Value?.ToString();
                    txtcccdbs.Text = row.Cells["cccd"].Value?.ToString();
                    txtemailbs.Text = row.Cells["email"].Value?.ToString();
                    txthochambs.Text = row.Cells["hocham"].Value?.ToString();
                }
                else if (rdoNhanvien.Checked)
                {
                    txtmanv.Text = row.Cells["manv"].Value?.ToString();
                    txthotennv.Text = row.Cells["hoten"].Value?.ToString();
                    dateTimePicker2.Text = row.Cells["ngaysinh"].Value?.ToString();
                    txtdiachinv.Text = row.Cells["diachi"].Value?.ToString();
                    txtsdtnv.Text = row.Cells["SDT"].Value?.ToString();
                    txtcccdnv.Text = row.Cells["cccd"].Value?.ToString();
                    txtemailnv.Text = row.Cells["email"].Value?.ToString();
                }
            }
        }
        private bool isEditing = false;

        private void txtsdtbs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isEditing)
            {
                if (txtsdtbs.Text.Length >= 10)
                {
                    MessageBox.Show("Yêu cầu chỉ nhập đủ 10 số !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtsdtbs.Focus();
                    e.Handled = true;
                }
            }

            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void txtcccdbs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtcccdbs.Text.Length >= 12)
            {
                MessageBox.Show("Yêu cầu chỉ nhập đủ 12 số !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcccdbs.Focus();
                e.Handled = true;
            }
        }

        private void txthotenbs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        public bool KiemTraThongTinNV()
        {
            if (txtmanv.Text == "" || txthotennv.Text == "" || dateTimePicker2.ToString() == DateTime.Now.Year.ToString() ||
                txtdiachinv.Text == "" || txtsdtnv.Text == "" || txtcccdnv.Text == "" || txtemailnv.Text == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void Load_DataGridNV()
        {
            DataTable dt = new DataTable();
            string sql_nv = "select * from NHANVIEN";
            dt = sql.Get_DataTable(sql_nv);
            dataGridView1.DataSource = dt;
        }

        private void rdoBacsi_CheckedChanged(object sender, EventArgs e)
        {
            string list = string.Empty;
            if (rdoBacsi.Checked)
            {
                list = "select * from BACSI";
            }
            else if (rdoNhanvien.Checked)
            {
                list = "select * from NHANVIEN";
            }
            if (!string.IsNullOrEmpty(list))
            {
                dt = sql.Get_DataTable(list);
            }
        }

        private void btnsuabs_Click(object sender, EventArgs e)
        {
            isEditing = true;

            btnluubs.Enabled = true;

            txtmabs.Enabled = false;
            txthotenbs.Enabled = false;
            rdonam.Enabled = false;
            rdonu.Enabled = false;
            dateTimePicker1.Enabled = false;
            txtcccdbs.Enabled = false;

            Load_DataGridBS();
        }

        private void btnluubs_Click(object sender, EventArgs e)
        {
            string kn = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";
            string sql = "update BACSI set DIACHI = @DIACHI, SDT =@SDT, EMAIL = @EMAIL, HOCHAM = @HOCHAM where MABS = @MABS";

            using (SqlConnection con = new SqlConnection(kn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@MABS", txtmabs.Text);
                    cmd.Parameters.AddWithValue("@DIACHI", txtdiachibs.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtsdtbs.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtemailbs.Text);
                    cmd.Parameters.AddWithValue("@HOCHAM", txthochambs.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    Load_DataGridBS();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnTrovebs_Click(object sender, EventArgs e)
        {
            txtmabs.Enabled = true;
            txthotenbs.Enabled = true;
            rdonam.Enabled = true;
            rdonu.Enabled = true;
            dateTimePicker1.Enabled = true;
            txtcccdbs.Enabled = true;
            btnluubs.Enabled = false;

            txtmabs.Clear();
            txthotenbs.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtdiachibs.Clear();
            txtsdtbs.Clear();
            txtcccdbs.Clear();
            txtemailbs.Clear();
            txthochambs.Clear();
            rdonu.Checked = false;
            rdonam.Checked = false;
        }

        private Boolean Exe(string cmd)
        {
            sql.openConnect();
            Boolean check;
            try
            {
                SqlCommand sc = new SqlCommand(cmd, conn);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }
            sql.closeConnect();
            return check;
        }

        private void btnthemnv_Click(object sender, EventArgs e)
        {
            if (KiemTraThongTinNV())
            {
                string sqlnv = "select * from NHANVIEN";
                DataRow newrow = dt.NewRow();
                newrow["manv"] = txtmanv.Text;
                newrow["hoten"] = txthotennv.Text;
                newrow["ngaysinh"] = dateTimePicker2.Text;
                newrow["diachi"] = txtdiachinv.Text;
                newrow["SDT"] = txtsdtnv.Text;
                newrow["cccd"] = txtcccdnv.Text;
                newrow["email"] = txtemailnv.Text;
                string gioiTinh = rdonamnv.Checked ? "Nam" : rdonunv.Checked ? "Nữ" : DBNull.Value.ToString();
                newrow["gioitinh"] = gioiTinh;

                dt.Rows.Add(newrow);
                int kq = sql.Update_DataTable(sqlnv, dt);
                if (kq == 0)
                {
                    MessageBox.Show("Them khong thanh cong", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Them thanh cong", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtmanv.Clear();
                    txthotennv.Clear();
                    dateTimePicker2.Value = DateTime.Now;
                    txtdiachinv.Clear();
                    txtsdtnv.Clear();
                    txtcccdnv.Clear();
                    txtemailnv.Clear();
                    rdonamnv.Checked = false;
                    rdonunv.Checked = false;
                    dataGridView1.DataSource = dt;
                }
                Load_DataGridNV();
            }
        }

        private void txthotennv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void txtsdtnv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (!isEditing)
            {
                if (txtsdtnv.Text.Length >= 10)
                {
                    MessageBox.Show("Yêu cầu chỉ nhập đủ 10 số !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtsdtnv.Focus();
                    e.Handled = true;
                }
            }
        }

        private void txtcccdnv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (txtcccdnv.Text.Length >= 12)
            {
                MessageBox.Show("Yêu cầu chỉ nhập đủ 12 số !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcccdnv.Focus();
                e.Handled = true;
            }
        }

        private void btnsuanv_Click(object sender, EventArgs e)
        {

            isEditing = true;

            btnluunv.Enabled = true;
            txtmanv.Enabled = false;
            txthotennv.Enabled = false;
            dateTimePicker2.Enabled = false;
            txtcccdnv.Enabled = false;

            Load_DataGridNV();
        }

        private void btnluunv_Click(object sender, EventArgs e)
        {
            string kn = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";
            string sql = "update NHANVIEN set DIACHI = @DIACHI, SDT =@SDT, EMAIL = @EMAIL where MANV = @MANV";

            using (SqlConnection con = new SqlConnection(kn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@MANV", txtmanv.Text);
                    cmd.Parameters.AddWithValue("@DIACHI", txtdiachinv.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtsdtnv.Text);
                    cmd.Parameters.AddWithValue("@EMAIL", txtemailnv.Text);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;
                    Load_DataGridNV();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnTrovenv_Click(object sender, EventArgs e)
        {
            txtmanv.Enabled = true;
            txthotennv.Enabled = true;
            dateTimePicker2.Enabled = true;
            txtcccdnv.Enabled = true;
            btnluunv.Enabled = false;

            txtmanv.Clear();
            txthotennv.Clear();
            dateTimePicker2.Value = DateTime.Now;
            txtdiachinv.Clear();
            txtsdtnv.Clear();
            txtcccdnv.Clear();
            txtemailnv.Clear();
            rdonamnv.Checked = false;
            rdonunv.Checked = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string gt = dataGridView1.SelectedRows[0].Cells["gioitinh"].Value.ToString();

                {
                    if (gt == "Nam")
                    {
                        rdonam.Checked = true;
                        rdonamnv.Checked = true;
                    }
                    else if (gt == "Nữ")
                    {
                        rdonu.Checked = true;
                        rdonunv.Checked = true;
                    }
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string kn = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";

            if (rdoBacsi.Checked)
            {

                string sql = "select * from BACSI where MABS = @MABS";

                using (SqlConnection con = new SqlConnection(kn))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@MABS", txttimkiem.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            if (rdoNhanvien.Checked)
            {
                string sql = "select * from NHANVIEN where MANV = @MANV";

                using (SqlConnection con = new SqlConnection(kn))
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@MANV", txttimkiem.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            if (rdoBacsi.Checked)
            {
                Load_DataGridBS();
            }
            else if (rdoNhanvien.Checked)
            {
                Load_DataGridNV();
            }
        }
    }
}

