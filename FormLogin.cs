using Đăng_Ký_Tín.DAO;
using Guna.UI2.WinForms;
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

namespace Đăng_Ký_Tín
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {          
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;           
        }
        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?","Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
        private void ButtonEx_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            DataProvider provider = new DataProvider();
            string username = txtUser.Text;
            string password = txtPass.Text;
            string role; // Biến role để nhận vai trò của người dùng

            if (provider.CheckLogin(username, password, out role))
            {
                // Đăng nhập thành công
                FormManager formManager = new FormManager(role, username); // Truyền username vào constructor của FormManager                
                MessageBox.Show($"Đăng nhập thành công! Vai trò của bạn là: {role}");
                this.Hide();
                formManager.ShowDialog();  
                this.Show(); 
                formManager.FormClosed += (s, args) =>this.Close();
                
            }
            else
            {
                // Đăng nhập thất bại
                txtPass.Clear();
                labelError.Visible = true;
                MessageBox.Show("Tài khoản hoặc mật khẩu sai!","Vui lòng nhập lại.",MessageBoxButtons.OK,MessageBoxIcon.Question);
            }
        }

        //Ẩn hiện mật khẩu
        private void vOff_Click(object sender, EventArgs e)
        {
              if (txtPass.PasswordChar == '\0')
              {
                  vOn.BringToFront();
                  txtPass.PasswordChar = '●';
              }
        }  
        private void vOn_Click(object sender, EventArgs e)
        {
              if(txtPass.PasswordChar == '●')
              {
                  vOff.BringToFront();
                  txtPass.PasswordChar = '\0';
              }
        }
    }
}
