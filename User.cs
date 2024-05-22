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
    public partial class User : Form
    {
        private string loggedInUsername;
        private FormManager manager;
        public User(string username = null, FormManager manager = null)
        {
            InitializeComponent();
            this.loggedInUsername = username;
            this.manager = manager;
            LoadUserData(loggedInUsername); // Tải dữ liệu người dùng cho loggedInUsername
        }

        private void LoadUserData(string username)
        {
            try
            {
                DataProvider provider = new DataProvider();
                DataTable userData = provider.GetUserDataByUsername(username);

                if (userData.Rows.Count > 0)
                {
                    DataRow row = userData.Rows[0];
                    txtmsv.Text = row["Mã sinh viên"].ToString();
                    txttsv.Text = row["Tên sinh viên"].ToString();
                    txtns.Text = ((DateTime)row["Ngày sinh"]).ToString("dd/MM/yyyy");
                    txtgt.Text = row["Giới tính"].ToString();
                    txtqH.Text = row["Quê quán huyện"].ToString();
                    txtqT.Text = row["Tỉnh/TP"].ToString();
                    txtL.Text = row["Tên lớp"].ToString();
                    txtTN.Text = row["Tên ngành"].ToString();
                    txtTK.Text = row["Tên khoa"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void btchangePS_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(loggedInUsername);
            changePasswordForm.ShowDialog();
        }

    }
}