using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic_Software
{
    public partial class Doimatkhau : Form
    {
        public Doimatkhau()
        {
            InitializeComponent();
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

        private void Doimatkhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        public bool KiemTra()
        {
            if (txtmkmoi.Text == "")
            {
                lblmkmoi.ForeColor = System.Drawing.Color.Red;
                lblmkmoi.Text = "Vui lòng nhập mật khẩu mới";
                txtmkmoi.Focus();
                return false;
            }
            else if (txtxacnhanmk.Text == "")
            {
                lblxacnhanmk.ForeColor = System.Drawing.Color.Red;
                lblxacnhanmk.Text = "Vui lòng xác nhận mật khẩu";
                txtxacnhanmk.Focus();
                return false;
            }
            else if (txtxacnhanmk.Text != txtmkmoi.Text)
            {
                lblxacnhanmk.ForeColor = System.Drawing.Color.Red;
                lblxacnhanmk.Text = "Mật khẩu mới và mật khẩu xác nhận không trùng khớp";
                txtxacnhanmk.Focus();
                txtxacnhanmk.SelectAll();
                return false;
            }
            return true;
        }
    }
}
