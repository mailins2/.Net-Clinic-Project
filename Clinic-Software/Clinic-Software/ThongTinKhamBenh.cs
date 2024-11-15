using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;
namespace Clinic_Software
{
    public partial class ThongTinKhamBenh : Form
    {
        List<TextBox> textBoxes = new List<TextBox>();
        ThongTinKham ttkb = new ThongTinKham();
        SQL sql = new SQL();
        public ThongTinKhamBenh()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
        }
        public void UpdateData(int i,string malh, string mabn, string tenbn, string ngaysinh, string gioitinh, string sdt, string diachi,string ngadk) 
        { 
            this.malh.Text = malh; 
            this.mabn.Text = mabn; 
            this.tenbn.Text = tenbn; 
            this.ngsinh.Text = ngaysinh; 
            this.gioitinh.Text = gioitinh; 
            this.sdt.Text = sdt; 
            this.diachi.Text = diachi; 
            this.ngaydk.Text = ngadk;

            if (i==0)
            {
                this.datxt.Text = ttkb.Da;
                this.mongtxt.Text = ttkb.Mong;
                this.toctxt.Text = ttkb.Toc;
                this.niemmactxt.Text = ttkb.NiemMac;
                this.trieuchungtxt.Text = ttkb.TrieuChung;
                this.chandoantxt.Text = ttkb.ChanDoan;
                this.dieutritxt.Text = ttkb.DieuTri;
                this.dantxt.Text = ttkb.LoiDan;
                this.checkBox2.Checked = ttkb.xacnhan;
                if (ttkb.NgayHen != string.Empty)
                {
                    checkBox1.Checked = true;
                    dateTimePicker1.Enabled = true;
                    this.dateTimePicker1.Text = ttkb.NgayHen;
                }
            }
        }
        public void Kham(bool flag)
        {
            if (flag)
            {
                foreach (var tb in textBoxes)
                {
                    tb.Enabled = true;
                }
                checkBox1.Enabled = true;
                LuuBtn.Enabled = true;
                InBtn.Enabled = true;
            }
            else
            {
                foreach (var tb in textBoxes)
                {
                    tb.Enabled = false;
                }
                checkBox1 .Enabled = false;
                LuuBtn.Enabled = false;
                InBtn .Enabled = false;
            }
        }
        public void saveTTKB()
        {
            ttkb.Da = datxt.Text;
            ttkb.Mong = mongtxt.Text;
            ttkb.Toc = toctxt.Text;
            ttkb.NiemMac = niemmactxt.Text;
            ttkb.TrieuChung = trieuchungtxt.Text;
            ttkb.ChanDoan = chandoantxt.Text;
            ttkb.DieuTri = dieutritxt.Text;
            if (checkBox1.Checked)
            {
                ttkb.NgayHen = dateTimePicker1.Text;
            }
            ttkb.LoiDan = dantxt.Text;
            ttkb.xacnhan = checkBox2.Checked;
        }
        public void clearAllTextBox()
        {
            checkBox1.Checked = false;
            datxt.Text = string.Empty;
            mongtxt.Text = string.Empty;
            toctxt.Text = string.Empty;
            niemmactxt.Text = string.Empty;
            trieuchungtxt.Text = string.Empty;
            chandoantxt.Text = string.Empty;
            dieutritxt.Text = string.Empty;
            dantxt.Text = string.Empty;
            dateTimePicker1.Enabled = false;
            checkBox2.Checked = false;
        }
        private void ThongTinKhamBenh_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            checkBox2.Enabled = false;
            textBoxes.Add(datxt);
            textBoxes.Add(toctxt);
            textBoxes.Add(mongtxt);
            textBoxes.Add(dantxt);
            textBoxes.Add(dieutritxt);
            textBoxes.Add(niemmactxt);
            textBoxes.Add(chandoantxt);
            textBoxes.Add(trieuchungtxt);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
                checkBox2 .Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                checkBox2 .Enabled = false;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);
            DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 30, 0);
            if (checkBox2.Checked)
            {
                if (dateTimePicker1.Value.Date < DateTime.Today)
                {
                    checkBox2.Checked = false;
                    MessageBox.Show("Không thể hẹn tái khám vào ngày đã qua", "KHÔNG THỂ XÁC NHẬN",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else if(dateTimePicker1.Value.Date == DateTime.Today)
                {
                    checkBox2.Checked = false;
                    MessageBox.Show("Không thể hẹn tái khám trong cùng một ngày", "KHÔNG THỂ XÁC NHẬN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dateTimePicker1.Value.TimeOfDay < startTime.TimeOfDay || dateTimePicker1.Value.TimeOfDay > endTime.TimeOfDay)
                {
                    checkBox2.Checked = false;
                    MessageBox.Show("Không thể tạo lịch hẹn ngoài giờ hành chính", "KHÔNG THỂ XÁC NHẬN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int count = (int)sql.getExecuteScalar("SELECT COUNT(*) FROM LICHHEN WHERE NGAYHEN BETWEEN DATEADD(minute, -15, '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "') AND DATEADD(minute, 15, '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "') AND MABS = '" + Program.LoginID + "'");
                    if (count > 0)
                    {
                        checkBox2.Checked = false;
                        MessageBox.Show("Đã có lịch hẹn trong khoảng thời gian này !");
                    }
                    else
                    {
                        MessageBox.Show("Lịch hẹn hợp lệ", "XÁC NHẬN LỊCH HẸN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            checkBox2.Checked=false;
        }

        private void LuuBtn_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("Ngày hẹn tái khám chưa được xác nhận. Vui lòng kiểm tra lại!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (chandoantxt.Text == string.Empty || dieutritxt.Text == string.Empty)
            {
                MessageBox.Show("Không thể để trống mục chẩn đoán và điều trị. Vui lòng kiểm tra lại", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult r = MessageBox.Show("Bạn có chắc muốn lưu bệnh án này không?\n Lưu ý: Bệnh án đã lưu không thể sửa!", "Xác nhận lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    string stt = sql.getExecuteScalar("SELECT FORMAT(COUNT(*)+1, '0000') FROM LSKHAM").ToString();
                    string maba = "BA" + stt;
                    DateTime ngaykham =(DateTime) sql.getExecuteScalar("SELECT NGAYHEN FROM LICHHEN WHERE MALH = '" + malh.Text + "'");
                    
                    sql.getExecuteNonQuery("INSERT INTO LSKHAM VALUES('"+maba+"','"+mabn.Text+"','"+Program.LoginID+ "','"+ ngaykham.ToString("yyyy-MM-dd")+ "','" + chandoantxt.Text+"','"+dieutritxt.Text+"')");
                    if (checkBox1.Checked)
                    {
                        stt = sql.getExecuteScalar("SELECT FORMAT(COUNT(*)+1, '0000') FROM LICHHEN").ToString();

                        string malh = "LH" + stt;
                        sql.getExecuteNonQuery("INSERT INTO LICHHEN VALUES('" + malh + "','" + mabn.Text + "','" + Program.LoginID + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm") + "',N'Chưa xác nhận')");
                    }
                    MessageBox.Show("Đã thêm bệnh án thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
