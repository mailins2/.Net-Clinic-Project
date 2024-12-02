using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
namespace Clinic_Software
{
    public partial class KhamBenh : Form
    {
        SQL sql= new SQL();
        string tenbnht = string.Empty;
        int rowbnht;
        List<int> selectedrow = new List<int>();
        int click_count = 0;
        int saved_control = 0;
        string MALICHHEN;
        ThongTinKhamBenh thongtinkhambenh = new ThongTinKhamBenh
        {
            Dock = DockStyle.Fill,
            FormBorderStyle = FormBorderStyle.None,
            TopLevel = false
        };
        public KhamBenh()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
            this.WindowState = FormWindowState.Maximized;
            panel1.Controls.Add(thongtinkhambenh);
            panel1.Tag = thongtinkhambenh;
            thongtinkhambenh.Show();
        }
        private void KhamBenh_Load(object sender, EventArgs e)
        {
            Program.LoginID = "BS0004";
            SqlDataAdapter adt = new SqlDataAdapter("SELECT MALH,HOTEN,FORMAT(NGAYHEN,'HH:mm dd/MM/yyyy') AS 'NGAYHEN' FROM LICHHEN,BENHNHAN WHERE BENHNHAN.MABN = LICHHEN.MABN AND ngayhen>= GETDATE() AND TRANGTHAI = N'đã xác nhận' AND MABS = '" + Program.LoginID+ "' order by cast(ngayhen as date), cast(ngayhen as time);", sql.Con);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dataGridViewDsBN.DataSource = dt;
            DataGridViewButtonColumn btns = new DataGridViewButtonColumn();
            btns.HeaderText = "#";
            btns.Text = "Khám"; 
            btns.UseColumnTextForButtonValue = true;
            dataGridViewDsBN.Columns.Add(btns);
            thongtinkhambenh.UpdateData(1,string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            thongtinkhambenh.Kham(false);
        }
        private void addSelectedRow(int i)
        {
            foreach(var item in selectedrow)
            {
                if (item == i)
                    return;
            }
            selectedrow.Add(i);
        }
        private void dataGridViewDsBN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewDsBN.Rows[e.RowIndex];
            string malh = MALICHHEN = row.Cells["MALH"].Value.ToString();
            string mabn = sql.getExecuteScalar("SELECT MABN FROM LICHHEN WHERE MALH = '" + malh + "'").ToString();
            string tenbn = row.Cells["HOTEN"].Value.ToString();
            string ngaysinh = sql.getExecuteScalar("SELECT FORMAT(NGAYSINH,'dd/MM/yyyy')  FROM BENHNHAN WHERE MABN = '" + mabn + "'").ToString();
            string gioitinh = sql.getExecuteScalar("SELECT GIOITINH  FROM LICHHEN,BENHNHAN WHERE LICHHEN.MABN = BENHNHAN.MABN AND MALH = '" + malh + "'").ToString();
            string sdt = sql.getExecuteScalar("SELECT SDT  FROM LICHHEN,BENHNHAN WHERE LICHHEN.MABN = BENHNHAN.MABN AND MALH = '" + malh + "'").ToString();
            string diachi = sql.getExecuteScalar("SELECT DIACHI FROM LICHHEN,BENHNHAN WHERE LICHHEN.MABN = BENHNHAN.MABN AND MALH = '" + malh + "'").ToString();
            string ngdk = row.Cells["NGAYHEN"].Value.ToString();
            // Cập nhật dữ liệu cho form
            if (selectedrow.Count > 0 && e.RowIndex == selectedrow[0])
            {
                // bệnh nhân đang được khám 
                thongtinkhambenh.UpdateData(0, malh, mabn, tenbn, ngaysinh, gioitinh, sdt, diachi, ngdk);
                saved_control = 0;
            }
            else
            {
                saved_control++;
                if (saved_control == 1)
                {
                    thongtinkhambenh.saveTTKB();
                }
                thongtinkhambenh.clearAllTextBox();
                thongtinkhambenh.UpdateData(1, malh, mabn, tenbn, ngaysinh, gioitinh, sdt, diachi, ngdk);

            }
            
            // Sự kiện cho nút Khám 
            if (e.ColumnIndex == 3)
            {
                addSelectedRow(e.RowIndex);
                if (e.RowIndex == selectedrow[0])
                {
                    thongtinkhambenh.Kham(true);
                    tenbnht = dataGridViewDsBN.SelectedCells[1].Value.ToString();
                    saved_control = 0;
                    kethucbtn.Enabled = true;
                    sql.getExecuteNonQuery("UPDATE LICHHEN SET TRANGTHAI = N'đang khám' WHERE MALH = '" + MALICHHEN + "'");
                }
                else
                {
                    MessageBox.Show("Hãy kết thúc khám bệnh nhân " + tenbnht + " trước !");
                    saved_control++;
                }
            }
            else
            {
                if (selectedrow.Count>0 && e.RowIndex == selectedrow[0])
                {
                    thongtinkhambenh.Kham(true);
                    thongtinkhambenh.UpdateData(0, malh, mabn, tenbn, ngaysinh, gioitinh, sdt, diachi, ngdk);

                }
                else
                { 
                    thongtinkhambenh.Kham(false);
                }
            }
        }

        private void kethucbtn_Click(object sender, EventArgs e)
        {
            sql.getExecuteNonQuery("UPDATE LICHHEN SET TRANGTHAI = N'đã khám' WHERE MALH = '" + MALICHHEN + "'");
            selectedrow.Clear();
            this.Hide(); 
            var newForm = new KhamBenh(); 
            newForm.Show(); 
        }
    }    
}
