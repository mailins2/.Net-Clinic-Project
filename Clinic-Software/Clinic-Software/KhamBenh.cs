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
        public static string malh { get; set; }
        public KhamBenh()
        {
            InitializeComponent();
            sql.Connect();
            sql.openConnect();
            malh = string.Empty;
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren) { frm.Close(); }
            ThongTinKhamBenh fr1 = new ThongTinKhamBenh
            {
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false
            };
            panel1.Controls.Add(fr1);
            panel1.Tag = fr1;
            fr1.Show();
        }

        private void kêDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren) { frm.Close(); }
            KeDichVu fr2 = new KeDichVu
            {
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false
            };
            panel1.Controls.Add(fr2);
            panel1.Tag = fr2;
            fr2.Show();
        }

        private void KhamBenh_Load(object sender, EventArgs e)
        {
            Program.LoginID = "BS0005";
            SqlDataAdapter adt = new SqlDataAdapter("SELECT MALH,HOTEN,FORMAT(NGAYHEN,'HH:mm dd/MM/yyyy') FROM LICHHEN,BENHNHAN WHERE BENHNHAN.MABN = LICHHEN.MABN AND MABS = '" + Program.LoginID+"' ORDER BY NGAYHEN",sql.Con);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            dataGridViewDsBN.DataSource = dt;
            DataGridViewButtonColumn btns = new DataGridViewButtonColumn();
            btns.HeaderText = "#";
            btns.Text = "Khám"; 
            btns.UseColumnTextForButtonValue = true;
            dataGridViewDsBN.Columns.Add(btns);
            
            foreach (Form frm in this.MdiChildren) 
            { 
                frm.Close(); 
            }
            ThongTinKhamBenh fr2 = new ThongTinKhamBenh
            {
                Dock = DockStyle.Fill,
                FormBorderStyle = FormBorderStyle.None,
                TopLevel = false
            };
            panel1.Controls.Add(fr2);
            panel1.Tag = fr2;
            fr2.Show();
        }

        private void dataGridViewDsBN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("a");
        }

        private void dataGridViewDsBN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            malh = dataGridViewDsBN.SelectedCells[0].Value.ToString();
        }
    }    
}
