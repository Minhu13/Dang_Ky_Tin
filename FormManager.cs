using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Đăng_Ký_Tín
{
    public partial class FormManager : Form
    {
        bool DK;
        private string userRole;
        private string loggedInUsername;

        public FormManager(string role = null, string username = null)
        {
            InitializeComponent();
            //Xóa thanh tiêu đề của form 
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            //Ẩn nút đi khi tài khoản đăng nhập có vai trò tương ứng
            this.userRole = role;

            if (role == "User")
            {
                AddContainer(new Home());
                guna2GradientTileButton4.Visible = false;
                label1.Visible = true;
            }
            if (role == "Admin")
            {
                AddContainer(new Home());
                guna2GradientTileButton2.Visible = false;
                DKcontrol.Visible = false;
                label2.Visible = true;
            }
            this.loggedInUsername = username; // Lưu lại username của người dùng đã đăng nhập
        }


        private void ButtonEx_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        public void AddContainer(Form form)
        {
            if (guna2Panel_control.Controls.Count > 0) guna2Panel_control.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            guna2Panel_control.Controls.Add(form);
            guna2Panel_control.Tag = form;
            form.BringToFront();
            form.Show();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            AddContainer(new Home());
        }

        private void DKtimer_Tick(object sender, EventArgs e)
        {
            if (DK)
            {
                DKcontrol.Height += 10;
                if (DKcontrol.Height == DKcontrol.MaximumSize.Height)
                {
                    DK = false;
                    DKtimer.Stop();
                }
            }
            else
            {
                DKcontrol.Height -= 10;
                if (DKcontrol.Height == DKcontrol.MinimumSize.Height)
                {
                    DK = true;
                    DKtimer.Stop();
                }
            }
        }
        private void guna2GradientTileButton2_Click(object sender, EventArgs e)
        {
            DKtimer.Start();
        }
        private void guna2GradientTileButton3_Click(object sender, EventArgs e)
        {
            if (userRole == "User")
            {
                // Sử dụng giá trị username đã đăng nhập thành công để truyền cho User form
                // Nếu vai trò của người dùng là "User", hiển thị form User và truyền loggedInUsername
                AddContainer(new User(loggedInUsername));
            }
            else if(userRole == "Admin")
            {
                // Nếu vai trò của người dùng là "Admin", hiển thị form User1 (hoặc tên form tương ứng cho Admin)
                AddContainer(new User1(loggedInUsername));
            }          
            //container(new User(loggedInUsername));
        }
        private void guna2GradientTileButton5_Click(object sender, EventArgs e)
        {
            AddContainer(new DangKy());
        }

        private void guna2GradientTileButton6_Click(object sender, EventArgs e)
        {
            //AddContainer(new HocPhanDK());
        }   
        private void guna2GradientTileButton4_Click(object sender, EventArgs e)
        {
            AddContainer(new Admin());
        }
        private void TTCN_Click(object sender, EventArgs e)
        {

        }

        private void ĐMK_Click(object sender, EventArgs e)
        {

        }
        private void Exit_Click(object sender, EventArgs e)
        {
           this.Close();
        }

    }
}
 