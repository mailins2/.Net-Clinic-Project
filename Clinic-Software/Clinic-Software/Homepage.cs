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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void khámChữaBệnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhamBenh kb = new KhamBenh();
            kb.Show();
        }
    }
}
