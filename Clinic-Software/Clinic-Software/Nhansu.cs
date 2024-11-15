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

namespace Clinic_Software
{
    public partial class Nhansu : Form
    {
        SQL sql = new SQL();
        DataTable dt = new DataTable();
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
            if (txtmabs.Text == "")
            {
                lblmabs.ForeColor = System.Drawing.Color.Red;
                lblmabs.Text = "Chưa nhập mã bác sĩ";
                txtmabs.Focus();
                return false;
            }
            else if (txthotenbs.Text == "")
            {
                lblhotenbs.ForeColor = System.Drawing.Color.Red;
                lblhotenbs.Text = "Chưa nhập họ tên";
                txthotenbs.Focus();
                return false;
            }
            else if (dateTimePicker1.ToString() == DateTime.Now.Year.ToString())
            {
                lblngaysinhbs.ForeColor = System.Drawing.Color.Red;
                lblngaysinhbs.Text = "Chưa chọn ngày sinh";
                dateTimePicker1.Focus();
                return false;
            }
            else if (txtdiachibs.Text == "")
            {
                lbldiachibs.ForeColor = System.Drawing.Color.Red;
                lbldiachibs.Text = "Chưa nhập địa chỉ";
                txtdiachibs.Focus();
                return false;
            }
            else if (txtsdtbs.Text == "")
            {
                lblsdtbs.ForeColor = System.Drawing.Color.Red;
                lblsdtbs.Text = "Chưa nhập số điện thoại";
                txtsdtbs.Focus();
                return false;
            }
            else if(txtcccdbs.Text == "")
            {
                lblcccdbs.ForeColor = System.Drawing.Color.Red;
                lblcccdbs.Text = "Chưa nhập CCCD";
                txtcccdbs.Focus();
                return false;
            }    
            else if (txtemailbs.Text == "")
            {
                lblemailbs.ForeColor = System.Drawing.Color.Red;
                lblemailbs.Text = "Chưa nhập email";
                txtemailbs.Focus(); 
                return false;
            }
            else if(txthochambs.Text == "")
            {
                lblhocham.ForeColor= System.Drawing.Color.Red;
                lblhocham.Text = "Chưa nhập học hàm";
                txthochambs.Focus();
                return false;
            }    
            return true;
        }
        public bool KiemTraThongTinNV()
        {
            if (txtmanv.Text == "")
            {
                lblmanv.ForeColor = System.Drawing.Color.Red;
                lblmanv.Text = "Chưa nhập mã nhân viên";
                txtmanv.Focus();
                return false;
            }
            else if (txthotennv.Text == "")
            {
                lblhotennv.ForeColor = System.Drawing.Color.Red;
                lblhotennv.Text = "Chưa nhập họ tên";
                txthotennv.Focus();
                return false;
            }
            else if (dateTimePicker2.ToString() == DateTime.Now.Year.ToString())
            {
                lblngaysinhnv.ForeColor = System.Drawing.Color.Red;
                lblngaysinhnv.Text = "Chưa chọn ngày sinh";
                dateTimePicker2.Focus();
                return false;
            }
            else if (txtdiachinv.Text == "")
            {
                lbldiachinv.ForeColor = System.Drawing.Color.Red;
                lbldiachinv.Text = "Chưa nhập địa chỉ";
                txtdiachinv.Focus();
                return false;
            }
            else if (txtsdtnv.Text == "")
            {
                lblsdtnv.ForeColor = System.Drawing.Color.Red;
                lblsdtnv.Text = "Chưa nhập số điện thoại";
                txtsdtnv.Focus();
                return false;
            }
            else if (txtcccdnv.Text == "")
            {
                lblcccdnv.ForeColor = System.Drawing.Color.Red;
                lblcccdnv.Text = "Chưa nhập CCCD";
                txtcccdnv.Focus();
                return false;
            }
            else if (txtemailbs.Text == "")
            {
                lblemailnv.ForeColor = System.Drawing.Color.Red;
                lblemailnv.Text = "Chưa nhập email";
                txtemailnv.Focus();
                return false;
            }
            return true;
        }

        private void Nhansu_Load(object sender, EventArgs e)
        {
            txtmabs.Focus();
            txtmanv.Focus();

            string sql_bs = "select * from BACSI";
            dt = sql.Get_DataTable(sql_bs);
            dataGridView1.DataSource = dt;
        }
    }
}
