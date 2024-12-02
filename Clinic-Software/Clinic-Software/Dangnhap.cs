using System;
using ClassLibrary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinic_Software
{
    public partial class Dangnhap : Form
    {
        SQL sql = new SQL();
        public Dangnhap()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string kn = @"Data Source = DESKTOP-97KSLKF\SQLEXPRESS;Initial Catalog = QLPK; Integrated Security = True";

            string tentk = txtusername.Text.Trim();
            string mk = txtmatkhaudn.Text.Trim();
            if (string.IsNullOrEmpty(tentk) || string.IsNullOrEmpty(mk))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string vaitro = "select VAITRO from TAIKHOAN where TENDANGNHAP =@TENDANGNHAP";

            Nhansu ns = new Nhansu();

            SqlConnection conn = new SqlConnection(kn);
            conn.Open();
            SqlCommand cdm = new SqlCommand();
            cdm.CommandType = CommandType.Text;
            cdm.CommandText = "select * from TAIKHOAN where TENDANGNHAP ='" + tentk + "' AND MATKHAU ='" + mk + "'";

            cdm.Connection = conn;
            SqlDataReader reader = cdm.ExecuteReader();

            if (reader.Read() == true)
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK);
                Homepage homepage = new Homepage();
                homepage.Show();
                if (vaitro == "Bacsi")
                {
                    ns.Enabled = false;

                }
                else if (vaitro == "Nhanvien")
                {
                    ns.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
          
        }

        private void Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dg == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ckbHienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbHienmatkhau.Checked)
            {
                txtmatkhaudn.PasswordChar = (char)0;
            }
            else
                txtmatkhaudn.PasswordChar = '*';
        }
    }
}

