using System;
using ClassLibrary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Clinic_Software
{
    public partial class Doimatkhau : Form
    {
        SQL sql = new SQL();
        public Doimatkhau()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
        }

        private void Doimatkhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ckbHTmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHTmatkhau.Checked)
            {
                txtmkmoi.PasswordChar = (char)0;
                txtxacnhanmk.PasswordChar = (char)0;
            }
            else
            {
                txtmkmoi.Text = "*";
                txtxacnhanmk.Text = "*";
            }
        }
        public bool KiemTra()
        {
            if (txtmkmoi.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtusername.Focus();
                return false;
            }
            else if (txtxacnhanmk.Text == "")
            {
                MessageBox.Show("Vui lòng xác nhận mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtxacnhanmk.Focus();
                return false;
            }
            else if (txtxacnhanmk.Text != txtmkmoi.Text)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu xác nhận không trùng khớp! Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtxacnhanmk.Focus();
                txtxacnhanmk.SelectAll();
                return false;
            }
            return true;
        }

        private void btnxacnhan_Click(object sender, EventArgs e)
        {

            string kn = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";
            string sql = "update TAIKHOAN set MATKHAU = @MATKHAU where TENDANGNHAP = @TENDANGNHAP";

            using (SqlConnection con = new SqlConnection(kn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@TENDANGNHAP", txtusername.Text);
                    cmd.Parameters.AddWithValue("@MATKHAU", txtmkmoi.Text);


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (KiemTra())
                    {
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

