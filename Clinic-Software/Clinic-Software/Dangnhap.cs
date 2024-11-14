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
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void ckbHienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienmatkhau.Checked)
            {
                txtmatkhau.PasswordChar = (char)0;
            }
            else
                txtmatkhau.PasswordChar = '*';
        }

        private void Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
