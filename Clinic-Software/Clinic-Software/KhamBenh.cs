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
    public partial class KhamBenh : Form
    {
        public KhamBenh()
        {
            InitializeComponent();
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
    }    
}
