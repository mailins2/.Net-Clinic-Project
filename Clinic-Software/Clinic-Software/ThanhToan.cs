﻿using System;
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
    public partial class ThanhToan : Form
    {
        private DataTable dttable;
        public ThanhToan(DataTable dt)
        {
            InitializeComponent();
            dttable = dt;
           
        }
        private void Display()
        {
            dataGridView1.DataSource = dttable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Display();
        }
    }
}
