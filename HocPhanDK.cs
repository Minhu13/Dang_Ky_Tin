﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Đăng_Ký_Tín
{
    public partial class HocPhanDK : Form
    {
        public HocPhanDK()
        {
            InitializeComponent();
        }
        public void LoadDataTable(DataTable dataTable)
        {
            if (dataTable != null)
            {
                guna2DataGridView1.DataSource = dataTable;
                guna2DataGridView1.Visible = true;
            }
        }
    }
}
