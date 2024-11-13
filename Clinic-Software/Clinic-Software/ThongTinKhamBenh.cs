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
    public partial class ThongTinKhamBenh : Form
    {
        public ThongTinKhamBenh()
        {
            InitializeComponent();
        }

        private void ThongTinKhamBenh_Load(object sender, EventArgs e)
        {
            malh.Text = KhamBenh.malh.ToString();
            dateTimePicker1.Text= string.Empty;
        }
    }
}
