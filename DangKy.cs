using Đăng_Ký_Tín.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Đăng_Ký_Tín
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
           
        }
        private void LoadDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();
                string selectedValue = guna2ComboBox1.SelectedItem.ToString();

                string query = $@"
SELECT 
    HP.MaDangKy AS 'Mã Lớp',
    HP.TenHocPhan AS 'Tên Môn Học',
    HP.SoDonViHocTrinh AS 'TC',
    HP.TenLop AS 'Tên Lớp',
    L.CoVanHocTap AS 'Giảng viên',
    HP.SiSo AS 'Sĩ số'
FROM 
    HocPhan HP
JOIN 
    Lop L ON HP.TenLop = L.TenLop
WHERE 
    HP.HocKy = '{selectedValue}';";
                   

                DataTable data = provider.ExecuteQuery(query);
                guna2DataGridView1.DataSource = data;
                guna2DataGridView1.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một DataTable để lưu trữ thông tin về các môn học đã chọn
                DataTable selectedSubjectsTable = new DataTable();
                selectedSubjectsTable.Columns.Add("Mã Lớp");
                selectedSubjectsTable.Columns.Add("Tên Môn Học");
                selectedSubjectsTable.Columns.Add("TC");
                selectedSubjectsTable.Columns.Add("Tên Lớp");
                selectedSubjectsTable.Columns.Add("Giảng viên");
                selectedSubjectsTable.Columns.Add("Sĩ số");
                bool isAnySelected = false;

                // Duyệt qua từng dòng trong DataGridView
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    // Kiểm tra nếu dòng hiện tại không phải là dòng header và ô checkbox được chọn
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["clChoice"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                    {
                        isAnySelected = true; // Cập nhật biến isAnySelected khi có một môn học được chọn

                        // Lấy thông tin về môn học đã chọn từ DataGridView ban đầu
                        string maLop = row.Cells["Mã Lớp"].Value.ToString();

                        // Kiểm tra xem mã lớp đã tồn tại trong DataTable hay chưa
                        bool exists = false;
                        foreach (DataRow existingRow in selectedSubjectsTable.Rows)
                        {
                            if (existingRow["Mã Lớp"].ToString() == maLop)
                            {
                                exists = true;
                                break;
                            }
                        }

                        // Nếu mã lớp chưa tồn tại trong DataTable, thêm môn học vào
                        if (!exists)
                        {
                            string tenMonHoc = row.Cells["Tên Môn Học"].Value.ToString();
                            string soDonViHocTrinh = row.Cells["TC"].Value.ToString();
                            string tenLop = row.Cells["Tên Lớp"].Value.ToString();
                            string cvht = row.Cells["Giảng viên"].Value.ToString();
                            string ss = row.Cells["Sĩ số"].Value.ToString();

                            // Thêm vào bảng DangKyHocPhan
                            string query = $@"
                        INSERT INTO DangKyHocPhan(MaLop, TenLop, TenHocPhan, SoDonViHocTrinh, CoVanHocTap, SiSo)
                        VALUES ({maLop}, N'{tenLop}', N'{tenMonHoc}', {soDonViHocTrinh}, N'{cvht}', N'{ss}');";

                            // Thực thi câu lệnh SQL
                            DataProvider provider = new DataProvider();
                            provider.ExecuteQuery(query);

                            // Thêm thông tin về môn học đã chọn vào DataTable
                            selectedSubjectsTable.Rows.Add(maLop, tenMonHoc, soDonViHocTrinh, tenLop, cvht, ss);
                        }
                    }
                }

                if (!isAnySelected)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một môn học.");
                    return;
                }
                HPDDK.Visible = true;
                label2.Visible = true;
                btDangKy.Visible = true;
                btHuy.Visible = true;

                // Load lại dữ liệu cho bảng HPDDK
                LoadHPDK();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadHPDK()
        {
            try
            {
                // Tạo một DataTable để lưu trữ dữ liệu từ bảng DangKyHocPhan
                DataTable dtHPDK = new DataTable();

                // Tạo câu truy vấn SQL để lấy dữ liệu từ bảng DangKyHocPhan
                string query = @"SELECT MaDangKy AS N'Mã Đăng Ký', 
                            MaLop AS N'Mã Lớp', 
                            TenLop AS N'Tên Lớp', 
                            TenHocPhan AS N'Tên Môn Học', 
                            SoDonViHocTrinh AS N'TC',
                            CoVanHocTap AS N'Giảng viên',
                            SiSo AS N'Sĩ Số' 
                     FROM DangKyHocPhan";

                // Thực thi truy vấn SQL và lấy dữ liệu vào DataTable
                DataProvider provider = new DataProvider();
                dtHPDK = provider.ExecuteQuery(query);

                // Gán DataTable chứa dữ liệu vào bảng HPDDK
                HPDDK.DataSource = dtHPDK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void Guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2RadioButton1.Checked)
            {
                LoadDataGridView(); // Gọi hàm để load dữ liệu vào DataGridView khi RadioButton được chọn
            } 
        }
        private void btDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ngày và giờ hiện tại
                DateTime ngayDangKy = DateTime.Now;

                // Khởi tạo DataTable để lưu trữ thông tin về các môn học đã chọn
                DataTable selectedSubjectsTable = new DataTable();
                selectedSubjectsTable.Columns.Add("Mã Lớp");
                selectedSubjectsTable.Columns.Add("Tên Môn Học");
                selectedSubjectsTable.Columns.Add("TC");
                selectedSubjectsTable.Columns.Add("Tên Lớp");
                selectedSubjectsTable.Columns.Add("Giảng viên");
                selectedSubjectsTable.Columns.Add("Ngày Đăng Ký");

                bool isAnySelected = false;

                // Duyệt qua từng dòng trong DataGridView HPDDK
                foreach (DataGridViewRow row in HPDDK.Rows)
                {
                    // Kiểm tra nếu ô checkbox được chọn
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["Cl2"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                    {
                        isAnySelected = true;
                        // Lấy thông tin về môn học đã chọn từ DataGridView
                        string maLop = row.Cells["Mã Lớp"].Value.ToString();
                        string tenMonHoc = row.Cells["Tên Môn Học"].Value.ToString();
                        string soDonViHocTrinh = row.Cells["TC"].Value.ToString();
                        string tenLop = row.Cells["Tên Lớp"].Value.ToString();
                        string cvht = row.Cells["Giảng viên"].Value.ToString();
                        string ngayDangKyStr = ngayDangKy.ToString("dd/MM/yyy");

                        // Thêm thông tin về môn học đã chọn vào DataTable
                        selectedSubjectsTable.Rows.Add(maLop, tenMonHoc, soDonViHocTrinh, tenLop, cvht, ngayDangKyStr);
                    }
                }

                if (!isAnySelected)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một môn học.");
                    return;
                }

                // Gán DataTable chứa các môn học đã chọn cho DataGridView tbDK
                tbDK.DataSource = selectedSubjectsTable;
                // Hiển thị các thành phần tương ứng
                tbDK.Visible = true;
                label3.Visible = true;
                panel3.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btHuy_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> maDangKyList = new List<int>();

                foreach (DataGridViewRow row in HPDDK.Rows)
                {
                    // Kiểm tra nếu ô checkbox được chọn
                    DataGridViewCheckBoxCell checkBoxCell = row.Cells["Cl2"] as DataGridViewCheckBoxCell;
                    if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                    {
                        // Lấy mã đăng ký từ dòng hiện tại
                        int maDangKy = Convert.ToInt32(row.Cells["Mã Đăng Ký"].Value);

                        // Thêm mã đăng ký vào danh sách
                        maDangKyList.Add(maDangKy);

                        // Xóa dòng được chọn khỏi cơ sở dữ liệu
                        XoaDangKy(maDangKy);

                        // Xóa dòng khỏi DataGridView
                        HPDDK.Rows.Remove(row);
                    }
                }
                // Clear selection sau khi xóa
                HPDDK.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaDangKy(int maDangKy)
        {
            try
            {
                // Tạo câu lệnh SQL DELETE
                string query = $"DELETE FROM DangKyHocPhan WHERE MaDangKy = {maDangKy}";

                // Thực thi câu lệnh SQL
                DataProvider provider = new DataProvider();
                int rowsAffected = provider.ExecuteNonQuery(query);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Đã hủy học phần!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Chưa chọn học phần.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
