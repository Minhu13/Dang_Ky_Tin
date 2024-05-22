using Đăng_Ký_Tín.DAO;
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
    public partial class ChangePasswordForm : Form
    {
        private string username;

        public ChangePasswordForm(string username = null)
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.username = username;
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;
            string newPasswordAgain = txtPasswordAgain.Text; // Đã sửa tên của txtPassowrdAgain thành txtPasswordAgain

            // Kiểm tra mật khẩu mới và mật khẩu nhập lại
            if (newPassword != newPasswordAgain)
            {
                errorProvider1.SetError(txtPasswordAgain, "Mật khẩu nhập lại không khớp với mật khẩu mới.");
                errorProvider1.SetError(txtNewPassword, "Mật khẩu nhập lại không khớp với mật khẩu mới.");
                return; // Dừng lại nếu không trùng khớp
            }
            else
            {
                errorProvider1.Clear(); // Xóa thông báo lỗi nếu trùng khớp
            }

            try
            {
                DataProvider provider = new DataProvider();
                bool passwordChanged = provider.ChangePassword(username, currentPassword, newPassword);

                if (passwordChanged)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu cũ chưa chính xác. Vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}");
            }
        }
        private void btcancelPS_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn muốn hủy thay đổi mật khẩu?","Thông báo!",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.No)
            {
                this.Close();
            }
        }
        private void txtPasswordAgain_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear(); // Xóa thông báo lỗi khi người dùng thay đổi nội dung
        }
        private void vOn_Click(object sender, EventArgs e)
        {
            if(txtCurrentPassword.PasswordChar == '●')
            {
                vOff.BringToFront();
                txtCurrentPassword.PasswordChar = '\0';
            }
        }

        private void vOff_Click(object sender, EventArgs e)
        {
            if(txtCurrentPassword.PasswordChar == '\0')
            {
                vOn.BringToFront();
                txtCurrentPassword.PasswordChar = '●';
            }
        }
        private void vOn1_Click(object sender, EventArgs e)
        {
            if(txtNewPassword.PasswordChar == '●')
            {
                vOff1.BringToFront();
                txtNewPassword.PasswordChar = '\0';
            }
        }

        private void vOff1_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.PasswordChar == '\0')
            {
                vOn1.BringToFront();
                txtNewPassword.PasswordChar = '●';
            }
        }

        private void vOn2_Click(object sender, EventArgs e)
        {
            if(txtPasswordAgain.PasswordChar == '●')
            {
                vOff2.BringToFront();
                txtPasswordAgain.PasswordChar = '\0';
            }
        }

        private void vOff2_Click(object sender, EventArgs e)
        {
            if(txtPasswordAgain.PasswordChar == '\0')
            {
                vOn2.BringToFront();
                txtPasswordAgain.PasswordChar = '●';
            }
        }

    }
}
