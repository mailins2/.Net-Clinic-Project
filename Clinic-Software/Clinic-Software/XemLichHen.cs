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
using System.Data.SqlClient;
namespace Clinic_Software
{
    public partial class XemLichHen : Form
    {
        SQL db = new SQL();
        int flag =0;
       
        BindingSource bindingsource = new BindingSource();
        public XemLichHen()
        {
            InitializeComponent();

            txtHoTen2.TextChanged += FilterChanged;
            txtMaLichHen.TextChanged += FilterChanged;
            txtMaBenhNhan2.TextChanged += FilterChanged;
            cbxTrangThai.SelectedIndexChanged += FilterChanged;

            dtPkerTuNgay.ValueChanged += FilterChanged;
            dtPikerDenNgay.ValueChanged += FilterChanged;
            btnTim.Click +=BtnTim_Click;

           
        }
        private void FilterChanged(object sender, EventArgs e)
        {
            LoadAppointments();
          // ApplyFilter();
        }
        private void BtnTim_Click(object sender, EventArgs e)
        {
            Load_Data();
        }




        public void Load_cbxTrangThai()
        {
            DataTable dt_LichHen = new DataTable();
            string sql = "select distinct trangthai from LICHHEN";
            dt_LichHen = db.Get_DataTable(sql);
            cbxTrangThai.DataSource = dt_LichHen;

            cbxTrangThai.DisplayMember = "trangthai";
            cbxTrangThai.ValueMember = "trangthai";
            cbxTrangThai.SelectedIndex = 0;
        }
        //public void AddNewItemCbxTrangThai()
        //{
        //    Load_cbxTrangThai();

        //    DataTable dataTable = (DataTable)cbxTrangThai.DataSource;

        //    // Kiểm tra xem dataTable có null hay không
        //    if (dataTable != null)
        //    {
        //        DataRow newRow = dataTable.NewRow();
        //        newRow["trangthai"] = " ";
        //        dataTable.Rows.Add(newRow);

        //        // Hiển thị nội dung của DataTable
        //        foreach (DataRow row in dataTable.Rows)
        //        {
        //            Console.WriteLine(row["trangthai"]);
        //        }

        //        // Cập nhật lại ComboBox
        //        cbxTrangThai.DataSource = null;
        //        cbxTrangThai.DataSource = dataTable;
        //        cbxTrangThai.DisplayMember = "trangthai";
        //        cbxTrangThai.ValueMember = "trangthai";
        //    }
        //    else
        //    {
        //        MessageBox.Show("DataTable is null.");
        //    }
        //}
        public void LoadAppointments()
        {

            string connectstr = "Data Source = CHOLE;Initial Catalog = QLPK;User ID = sa;Password = 123";
            string sql = "SELECT malh,lh.mabn,bn.HOTEN,lh.mabs,FORMAT(NGAYHEN,'HH:mm dd-MM-yyyy') AS 'NGAYHEN',trangthai FROM LichHen lh join BENHNHAN bn on bn.MABN = lh.mabn WHERE(@TrangThai IS NULL OR trangthai = @TrangThai) AND(@MaLichHen IS NULL OR malh = @MaLichHen) AND(@MaBenhNhan IS NULL OR lh.mabn = @MaBenhNhan) AND(@HoTen is null or HOTEN = @HoTen) AND (ngayhen BETWEEN @TuNgay  AND @DenNgay) order by cast(ngayhen as date), cast(ngayhen as time) ";
            DateTime selectedDateTuNgay = dtPkerTuNgay.Value;
            DateTime selectedDateDenNgay = dtPikerDenNgay.Value;
            string formatDateTuNgay = selectedDateTuNgay.ToString("yyyy-MM-dd HH:mm:ss");
            string formatDateDenNgay = selectedDateDenNgay.ToString("yyyy-MM-dd HH:mm:ss");

            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                SqlCommand command = new SqlCommand(sql, connection);

                // Thêm các tham số cho bộ lọc
                command.Parameters.AddWithValue("@TrangThai", string.IsNullOrEmpty(cbxTrangThai.SelectedValue.ToString()) ? (object)DBNull.Value : cbxTrangThai.SelectedValue.ToString());
                command.Parameters.AddWithValue("@MaLichHen", string.IsNullOrEmpty(txtMaLichHen.Text) ? (object)DBNull.Value : txtMaLichHen.Text);
                command.Parameters.AddWithValue("@MaBenhNhan", string.IsNullOrEmpty(txtMaBenhNhan2.Text) ? (object)DBNull.Value : txtMaBenhNhan2.Text);
                command.Parameters.AddWithValue("@HoTen", string.IsNullOrEmpty(txtHoTen2.Text) ? (object)DBNull.Value : txtHoTen2.Text);

                command.Parameters.AddWithValue("@TuNgay", formatDateTuNgay);
                command.Parameters.AddWithValue("@DenNgay", formatDateDenNgay);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dtGV_LichHen.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        public void Load_cbxBacSi()
        {
            string sql = "select mabs,hoten from BACSI";
            DataTable dt_BaSi = new DataTable();
            dt_BaSi = db.Get_DataTable(sql);
            cbxBacSi.DataSource = dt_BaSi;
            cbxBacSi.DisplayMember = "hoten";
            cbxBacSi.ValueMember = "mabs";
        }
        public void Load_cbxGioiTinh()
        {
            string sql = "select distinct GIOITINH from BENHNHAN";
            DataTable dt_BenhNhan = new DataTable();
            dt_BenhNhan = db.Get_DataTable(sql);
            cbxGioiTinh.DataSource = dt_BenhNhan;
            cbxGioiTinh.DisplayMember = "gioitinh";
            cbxGioiTinh.ValueMember = "gioitinh";
        }


        private void XemLichHen_Load(object sender, EventArgs e)
        {
            dtPker_NgayHen.Format = DateTimePickerFormat.Custom;
            dtPker_NgayHen.CustomFormat = "HH:mm dd-MM-yyyy";
            dtPkerTuNgay.Format = DateTimePickerFormat.Custom;
            dtPkerTuNgay.CustomFormat = "dd-MM-yyyy";
            dtPikerDenNgay.Format = DateTimePickerFormat.Custom;
            dtPikerDenNgay.CustomFormat = "dd-MM-yyyy";
            dtPkerNgaySinh.Format = DateTimePickerFormat.Custom;
            dtPkerNgaySinh.CustomFormat = "dd-MM-yyyy";
            Load_cbxTrangThai();
           
            Load_cbxBacSi();
            Load_cbxGioiTinh();
            LoadAppointments();

            
            txtHoTen.Enabled = false;
            dtPkerNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            cbxGioiTinh.Enabled = false;
            txtDiaChi.Enabled = false;

            rdHuy.Enabled = false;
            rdXacNhan.Enabled = false;
            rdChuaXN.Enabled = false;
            rdDangKham.Enabled = false;
            rdKhamXong.Enabled = false;
            txtMaLH.Enabled = false;
            cbxBacSi.Enabled = false;
            dtPker_NgayHen.Enabled = false;
            btnDangKy.Enabled = true;
            gbxXn_TrangThai.Visible=false;
            
            flag = 1;


        }

        private void txtMaBN_Enter(object sender, EventArgs e)
        {
            if (txtMaBN.Text == "Nhập mã bệnh nhân ")
            {
                txtMaBN.Text = "";
                txtMaBN.ForeColor = System.Drawing.Color.Black;
                txtMaBN.Font = new Font(txtMaBN.Font, FontStyle.Regular);//đổi kiểu chữ sang bình thường 
            }
        }

        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBN.Text))
            {
                txtMaBN.Text = "Nhập mã bệnh nhân ";
                txtMaBN.ForeColor = System.Drawing.Color.Gray;
              
            }
        }

        private void txtHoTen_Enter(object sender, EventArgs e)
        {
            if (txtHoTen.Text == "Nhập họ tên  ")
            {
                txtHoTen.Text = "";
                txtHoTen.ForeColor = System.Drawing.Color.Black;
               // txtHoTen.Font = new Font(txtHoTen.Font, FontStyle.Regular);//đổi kiểu chữ sang bình thường 
            }
        }

        private void txtHoTen_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                txtHoTen.Text = "Nhập họ tên  ";
                txtHoTen.ForeColor = System.Drawing.Color.Gray;
               // txtHoTen.Font = new Font(txtHoTen.Font, FontStyle.Italic);//đổi kiểu chữ sang in nghiêng 
            }
        }

        private void txtSDT_Enter(object sender, EventArgs e)
        {
            if (txtSDT.Text == "Nhập số điện thoại  ")
            {
                txtSDT.Text = "";
                txtSDT.ForeColor = System.Drawing.Color.Black;
               // txtSDT.Font = new Font(txtSDT.Font, FontStyle.Regular);//đổi kiểu chữ sang bình thường 
            }
        }

        private void txtSDT_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                txtSDT.Text = "Nhập số điện thoại  ";
                txtSDT.ForeColor = System.Drawing.Color.Gray;
               // txtSDT.Font = new Font(txtSDT.Font, FontStyle.Italic);//đổi kiểu chữ sang in nghiêng 
            }
        }

        private void txtDiaChi_Enter(object sender, EventArgs e)
        {
            if (txtDiaChi.Text == "Nhập địa chỉ ")
            {
                txtDiaChi.Text = "";
                txtDiaChi.ForeColor = System.Drawing.Color.Black;
               // txtDiaChi.Font = new Font(txtDiaChi.Font, FontStyle.Regular);//đổi kiểu chữ sang bình thường 
            }
        }

        private void txtDiaChi_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                txtDiaChi.Text = "Nhập địa chỉ ";
                txtDiaChi.ForeColor = System.Drawing.Color.Gray;
                //txtDiaChi.Font = new Font(txtDiaChi.Font, FontStyle.Italic);//đổi kiểu chữ sang in nghiêng 
            }
        }

        private void txtMaLH_Enter(object sender, EventArgs e)
        {
            if (txtMaLH.Text == "Nhập mã lịch hẹn  ")
            {
                txtMaLH.Text = "";
                txtMaLH.ForeColor = System.Drawing.Color.Black;
                //txtMaLH.Font = new Font(txtMaLH.Font, FontStyle.Regular);//đổi kiểu chữ sang bình thường 
            }
        }

        private void txtMaLH_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLH.Text))
            {
                txtMaLH.Text = "Nhập mã lịch hẹn  ";
                txtMaLH.ForeColor = System.Drawing.Color.Gray;
               // txtMaLH.Font = new Font(txtMaLH.Font, FontStyle.Italic);//đổi kiểu chữ sang in nghiêng 
            }
        }

      

        private void dtGV_LichHen_SelectionChanged(object sender, EventArgs e)
        {
            rdBtnXN_Huy.Checked = false;
            rdBtnXn_XacNhan.Checked = false;
            gbxXn_TrangThai.Visible = false;
            if (dtGV_LichHen.SelectedRows.Count > 0)
            {
                var selectedRow = dtGV_LichHen.SelectedRows[0];
                txtMaBN.Text = selectedRow.Cells["mabn"].Value.ToString();
                txtHoTen.Text = selectedRow.Cells["hoten"].Value.ToString();
                txtMaLH.Text = selectedRow.Cells["malh"].Value.ToString();
                cbxBacSi.SelectedValue = selectedRow.Cells["mabs"].Value.ToString();
                string trangthai = selectedRow.Cells["trangthai"].Value.ToString().ToUpper().Trim();
                string NgayHen = selectedRow.Cells["ngayhen"].Value.ToString();
                DateTime dateValue;

                if (DateTime.TryParseExact(NgayHen, "HH:mm dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateValue))
                {
                    dtPker_NgayHen.Value = dateValue;
                }
                else
                {
                    MessageBox.Show("Giá trị ngày không hợp lệ: " + NgayHen + " ngày chuyển đổi: " + dateValue);


                }
                string sql = "select FORMAT(ngaysinh,'dd-MM-yyyy') as Ngaysinh,DIACHI,SDT,GIOITINH from BENHNHAN bn where mabn = '" + selectedRow.Cells["mabn"].Value.ToString() + "'";
                DataTable dt_tmp =new DataTable();
                dt_tmp = db.Get_DataTable(sql);
                DataRow dataRow = dt_tmp.Rows[0];
                txtDiaChi.Text = dataRow["diachi"].ToString();
                txtSDT.Text = dataRow["sdt"].ToString();
                cbxGioiTinh.SelectedValue = dataRow["gioitinh"].ToString();
                string ngaysinh = dataRow["ngaysinh"].ToString();
                DateTime dateNgaysinh;
                if(DateTime.TryParseExact(ngaysinh,"dd-MM-yyyy",System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out dateNgaysinh))
                {
                    dtPkerNgaySinh.Value = dateNgaysinh;

                }
                else
                {
                    MessageBox.Show("Giá trị không hợp lệ : " + ngaysinh + dateNgaysinh);
                }
                foreach(Control c in tbLayoutRadio.Controls)
                {

                    RadioButton rb = c as RadioButton;
           
                    if ( rb!=null && rb.Text.ToUpper().Trim()==trangthai.ToUpper().Trim())
                    {
                        rb.Checked = true;
                    }
                }

            }
            
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            gbxXn_TrangThai.Visible=true;

            txtHoTen.Enabled = true;
            dtPkerNgaySinh.Enabled = true;
            txtSDT.Enabled = true;
            cbxGioiTinh.Enabled = true;
            txtDiaChi.Enabled = true;
            txtMaLH.Enabled = false;
            cbxBacSi.Enabled = true;
            dtPker_NgayHen.Enabled = true;
           if(rdChuaXN.Checked==true)
            {
                rdBtnXN_Huy.Enabled = true;
                rdBtnXn_XacNhan.Enabled = true;
            }
           else
            {
                rdBtnXN_Huy.Enabled = false;
                rdBtnXn_XacNhan.Enabled = false;
            }





        }
        public bool CheckTextBox(string a)
        {
            if(string.IsNullOrEmpty(a))
            {
                return false;
            }
            return true;
        }

        private void Load_Data()
        {
            if(CheckTextBox(txtMaBN.Text))
            {
                string sql = "SELECT malh,lh.mabn,bn.HOTEN,lh.mabs,FORMAT(NGAYHEN,'HH:mm dd-MM-yyyy') AS 'NGAYHEN',trangthai FROM LichHen lh join BENHNHAN bn on bn.MABN = lh.mabn where bn.MABN='" + txtMaBN.Text + "' order by cast(ngayhen as date), cast(ngayhen as time)";
                DataTable dt_ttbn = new DataTable();
                dt_ttbn = db.Get_DataTable(sql);
                
                dtGV_LichHen.DataSource = dt_ttbn;
            }
            else
            {
                MessageBox.Show("Lỗi : Vui lòng nhập mã bệnh nhân ");
            }
            

        }
        //public void ApplyFilter()//chưa chạy được 
        //{
        //    string filter = "";
        //    if(!string.IsNullOrEmpty(txtMaLichHen.Text))
        //    {
        //        filter += $"malh='{txtMaLichHen.Text}' AND";

        //    }
        //    if (!string.IsNullOrEmpty(txtMaBenhNhan2.Text)) 
        //    { 
        //        filter += $"mabn = '{txtMaBenhNhan2.Text}' AND "; 
        //    }
        //    if (!string.IsNullOrEmpty(txtHoTen2.Text)) 
        //    { 
        //        filter += $"HOTEN LIKE '%{txtHoTen2.Text}%' AND ";
        //    }
        //    if (cbxTrangThai.SelectedIndex >= 0 && !string.IsNullOrEmpty(cbxTrangThai.SelectedValue.ToString())) 
        //    {
        //        filter += $"trangthai = '{cbxTrangThai.SelectedValue}' AND "; 
        //    }
        //    DateTime tuNgay = dtPkerTuNgay.Value.Date; 
        //    DateTime denNgay = dtPikerDenNgay.Value.Date; 
        //    filter += $"ngayhen >= '{tuNgay:yyyy-MM-dd HH:mm:ss}' AND ngayhen <= '{denNgay:yyyy-MM-dd HH:mm:ss}'"; 
        //    // Áp dụng filter
        //    bindingsource.Filter = filter;
        //}

       
        public string Trangthai()
        {
          
            if(rdChuaXN.Checked==true)
            {
               
                if (rdBtnXN_Huy.Checked)
                {
                    return "Đã hủy";
                }
                else if(rdBtnXn_XacNhan.Checked)
                {
                    return "Đã xác nhận";
                }
                else
                {
                    return "Chưa xác nhận";
                }
            }
            else if(rdHuy.Checked==true)
            {
                return "Đã hủy";
            }
            else if (rdXacNhan.Checked==true)
            {
                return "Đã xác nhận";
            }
            else if(rdDangKham.Checked==true)
            {
                return "Đang khám";
            }
            else
            {
                return "Đã khám"; 
            }
            
            

           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            btnKetThuc.Enabled = true;
            txtMaLichHen.Enabled = false;
            string connectstr = "Data Source = CHOLE;Initial Catalog = QLPK;User ID = sa;Password = 123";

            using (SqlConnection connection = new SqlConnection(connectstr))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();//để khi có lỗi thì có thể quay về database lúc đầu 
                try
                {
                    string sqlUdBN = "UPDATE BENHNHAN SET HOTEN = @hoten, NGAYSINH = @ngaysinh, SDT = @sdt, GIOITINH = @gioitinh, DIACHI = @diachi WHERE MABN = @mabn";
                    string sqlUdLH = "update LICHHEN set mabn = @mabn, mabs = @mabs, ngayhen = @ngayhen, trangthai = @trangthai where malh = @malh";

                    DateTimePicker ngaysinh = dtPkerNgaySinh;
                    ngaysinh.Format = DateTimePickerFormat.Custom;
                    ngaysinh.CustomFormat = "dd-MM-yyyy";
                    DateTime ns = ngaysinh.Value;
                    DateTimePicker ngayhen = dtPker_NgayHen;
                    ngayhen.Format = DateTimePickerFormat.Custom;
                    ngayhen.CustomFormat = "dd-MM-yyyy";
                    DateTime nh = ngayhen.Value;
                    
                    SqlCommand cmdUpdateLH = new SqlCommand(sqlUdLH, connection, transaction);
                    cmdUpdateLH.Parameters.AddWithValue("@mabs", cbxBacSi.SelectedValue.ToString());
                    cmdUpdateLH.Parameters.AddWithValue("@ngayhen", nh);
                    cmdUpdateLH.Parameters.AddWithValue("@mabn", txtMaBN.Text);
                    cmdUpdateLH.Parameters.AddWithValue("@malh", txtMaLH.Text);
                    if(Trangthai()=="0")
                    {
                        DialogResult r;
                        r=MessageBox.Show("Bạn chưa xác nhận trạng thái cho lịch hẹn ", "Thông báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if(r==DialogResult.Yes)
                        {
                            Load_TTBN();
                        }

                    }
                    else
                    {
                        cmdUpdateLH.Parameters.AddWithValue("@trangthai", Trangthai().ToLower().Trim());
                    }
                    cmdUpdateLH.ExecuteNonQuery();
                    SqlCommand cmdUpdateBN = new SqlCommand(sqlUdBN, connection, transaction);
                    cmdUpdateBN.Parameters.AddWithValue("@hoten", txtHoTen.Text);
                    cmdUpdateBN.Parameters.AddWithValue("@ngaysinh", ns);
                    cmdUpdateBN.Parameters.AddWithValue("@sdt", txtSDT.Text);
                    cmdUpdateBN.Parameters.AddWithValue("@gioitinh", cbxGioiTinh.SelectedValue.ToString());
                    cmdUpdateBN.Parameters.AddWithValue("@diachi", txtDiaChi.Text);

                    cmdUpdateBN.Parameters.AddWithValue("@mabn", txtMaBN.Text);
                    cmdUpdateBN.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Dữ liệu đã được cập nhật thành công .");
                    Load_cbxTrangThai();
                    LoadAppointments();


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi: " + ex.Message);
                }

            }
            Load_TTBN();
            
            
            
            

        }
        public void Load_TTBN()
        {
            gbxXn_TrangThai.Visible = false;
            txtHoTen.Enabled = false;
            dtPkerNgaySinh.Enabled = false;
            txtSDT.Enabled = false;
            cbxGioiTinh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtMaLH.Enabled = false;
            cbxBacSi.Enabled = false;
            dtPker_NgayHen.Enabled = false;

            string sql = "SELECT trangthai FROM LICHHEN WHERE malh = @malh";
            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@malh", txtMaLH.Text)
            };
            string trangthaimoi = (string)db.getExecuteScalar(sql, parameters);

            foreach (Control c in tbLayoutRadio.Controls)
            {
                RadioButton rb = c as RadioButton;
                if (rb != null && rb.Text.ToUpper().Trim() == trangthaimoi.ToUpper().Trim())
                {
                    rb.Checked = true;
                }
            }
        }


        public void DeleteAppoinments(object sender, EventArgs e)
        {

            try
            {
                string sql = "delete from LICHHEN where malh = '" + txtMaLH.Text + "'";

                db.getExecuteNonQuery(sql);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra !  " + ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(flag==1)
            {
                DialogResult r;
                r = MessageBox.Show("Bạn có chắc chắn muốn xóa lịch hẹn này không ?","Thông báo" , MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (r == DialogResult.Yes)
                {
                    DeleteAppoinments(sender, e);
                    
                    MessageBox.Show("Lịch hẹn đã được xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
           
        }

    

      

       

        private void btnKham_Click(object sender, EventArgs e)
        {
            if (rdXacNhan.Checked == true)
            {
                DialogResult r;
                r = MessageBox.Show("Lịch hẹn bắt đầu ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (r == DialogResult.OK)
                {
                    DateTime CurrentTime = DateTime.Now;
                    DateTime TimeHen = dtPker_NgayHen.Value;
                    // if (CurrentTime.Date == TimeHen.Date && CurrentTime.Hour == TimeHen.Hour)
                    if (CurrentTime.Date == TimeHen.Date )//xem code có đúng không 
                    {
                        DialogResult m;

                        m = MessageBox.Show("Bệnh nhân đang được khám bệnh?", "Thông báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (m == DialogResult.Yes)
                        {
                            rdDangKham.Checked = true;
                        }
                        else
                        {
                            rdDangKham.Checked = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lịch hẹn hiện chưa tới thời gian hẹn . Vui lòng kiểm tra lại thời gian khám !");
                    }


                }

            }
            else
            {
                MessageBox.Show("Lịch hẹn bệnh nhân chưa được xác nhận", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            DangKyLichHen newDk = new DangKyLichHen();
            newDk.ShowDialog();
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bệnh nhân đã khám xong ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                rdKhamXong.Checked = true;
                DialogResult a;
                a = MessageBox.Show("Xem thông tin thanh toán ?", "Thông báo ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.Yes)
                {
                    string sql = "SELECT malh,lh.mabn,bn.HOTEN,lh.mabs,FORMAT(NGAYHEN,'HH:mm dd-MM-yyyy') AS 'NGAYHEN',trangthai  FROM LichHen lh join BENHNHAN bn on bn.MABN = lh.mabn where malh = '" + txtMaLH.Text + "'";
                    DataTable db_ttbn = new DataTable();
                    db_ttbn = db.Get_DataTable(sql);
                    ThanhToan newThanhToan = new ThanhToan(db_ttbn);
                    newThanhToan.ShowDialog();
                }

            }
        }
    }
}
