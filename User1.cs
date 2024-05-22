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
    public partial class User1 : Form
    {
        private string loggedInUsername;
        public User1(string username = null)
        {
            InitializeComponent();
            this.loggedInUsername = username;
            LoadUserData(loggedInUsername); // Tải dữ liệu người dùng cho loggedInUsername
        }

        private void LoadUserData(string username)
        {
            try
            {
                DataProvider provider = new DataProvider();
                DataTable userData = provider.GetUserDataByUsername1(username);

                if (userData.Rows.Count > 0)
                {
                    DataRow row = userData.Rows[0];
                    // Hiển thị thông tin người dùng trên giao diện
                    txtmgv.Text = row["Mã giảng viên"].ToString();
                    txttgv.Text = row["Họ và Tên"].ToString();
                    txtns.Text = ((DateTime)row["Ngày sinh"]).ToString("dd/MM/yyyy");
                    txtgt.Text = row["Giới tính"].ToString();
                    txtqH.Text = row["Quê quán huyện"].ToString();
                    txtqT.Text = row["Tỉnh/TP"].ToString();
                    txtTk.Text = row["Tên khoa"].ToString();
                    txtTrgk.Text = row["Trưởng khoa"].ToString();
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy thông tin giảng viên: {username}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void btchangePS_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(loggedInUsername);
            changePasswordForm.ShowDialog();
        }
    }
}
