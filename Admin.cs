using Đăng_Ký_Tín.DAO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Đăng_Ký_Tín
{
    public partial class Admin : Form
    {
        private DataProvider provider;
        public Admin()
        {
            InitializeComponent();
            // Khởi tạo DataProvider khi khởi động form
            provider = new DataProvider();

            FillCbNganh();
            FillCbKhoa();
            FillCbLop();
            FillCbLopHP();
            Fillmgv();
            Fillmdk();

        }

        // Phương thức điền dữ liệu vào ComboBox cho Tên ngành
        private void FillCbNganh()
        {
            List<string> tenNganhList = provider.GetTenNganhList();

            cbNganh.Items.Clear();
            cbNganh.Items.AddRange(tenNganhList.ToArray());
        }
        private void FillCbLop()
        {
            List<string> tenLopList = provider.GetTenLopList();

            cbTL.Items.Clear();
            cbTL.Items.AddRange(tenLopList.ToArray());
        }
        private void Fillmgv()
        {
            List<string> mgvList = provider.GetMGVList();

            cbMGV.Items.Clear();
            cbMGV.Items.AddRange(mgvList.ToArray());
        }
        private void Fillmdk()
        {
            List<string> mdkList = provider.GetMDKList();

            cbMDK.Items.Clear();
            cbMDK.Items.AddRange(mdkList.ToArray());
        }
        private void FillCbLopHP()
        {
            List<string> tenLopList = provider.GetTenLopList();

            cbLHP.Items.Clear();
            cbLHP.Items.AddRange(tenLopList.ToArray());
        }
        private void FillCbKhoa()
        {
            List<string> tenKhoaList = provider.GetTenKhoaList();

            cbKhoa.Items.Clear();
            cbKhoa.Items.AddRange(tenKhoaList.ToArray());

            cbKhoaGV.Items.AddRange(tenKhoaList.ToArray());
        }

        private void btnThemSinhVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string maSinhVien = txtmsv.Text;
                string hoTen = txttsv.Text;
                DateTime ngaySinh = dtNS.Value; // Chuyển đổi chuỗi thành kiểu DateTime
                string gioiTinh = cbGT.SelectedItem.ToString();
                string queQuanHuyen = txtqH.Text;
                string queQuanTinh = txtqT.Text;
                string tenLop = cbTL.SelectedItem.ToString();
                string tenNganh = cbNganh.SelectedItem.ToString();
                string tenKhoa = cbKhoa.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddStudent để thêm sinh viên mới vào cơ sở dữ liệu
                bool addSuccess = provider.AddStudent(maSinhVien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenLop, tenNganh, tenKhoa);

                if (addSuccess)
                {
                    // Thông báo thành công
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi thêm sinh viên thành công, có thể làm một số công việc khác ở đây (ví dụ: làm mới DataGridView)
                    LoadDataSVDataGridView();
                    ClearSVFields();
                }
                else
                {
                    // Thông báo thất bại
                    MessageBox.Show("Không thể thêm sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btFix_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string maSinhVien = txtmsv.Text;
                string hoTen = txttsv.Text;
                DateTime ngaySinh = dtNS.Value; // Chuyển đổi chuỗi thành kiểu DateTime
                string gioiTinh = cbGT.SelectedItem.ToString();
                string queQuanHuyen = txtqH.Text;
                string queQuanTinh = txtqT.Text;
                string tenLop = cbTL.SelectedItem.ToString();
                string tenNganh = cbNganh.SelectedItem.ToString();
                string tenKhoa = cbKhoa.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddStudent để thêm sinh viên mới vào cơ sở dữ liệu
                bool updateSuccess = provider.UpdateStudentInformation(maSinhVien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenLop, tenNganh, tenKhoa);

                if (updateSuccess)
                {
                    // Thông báo thành công
                    MessageBox.Show("Sửa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi thêm sinh viên thành công, có thể làm một số công việc khác ở đây (ví dụ: làm mới DataGridView)
                    LoadDataSVDataGridView();
                    ClearSVFields();
                }
                else
                {
                    // Thông báo thất bại
                    MessageBox.Show("Không thể sửa sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string maSinhVien = txtmsv.Text;
                string hoTen = txttsv.Text;
                DateTime ngaySinh = dtNS.Value; // Chuyển đổi chuỗi thành kiểu DateTime
                string gioiTinh = cbGT.SelectedItem.ToString();
                string queQuanHuyen = txtqH.Text;
                string queQuanTinh = txtqT.Text;
                string tenLop = cbTL.SelectedItem.ToString();
                string tenNganh = cbNganh.SelectedItem.ToString();
                string tenKhoa = cbKhoa.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức DeleteStudent để thêm sinh viên mới vào cơ sở dữ liệu
                bool DeleteSuccess = provider.DeleteStudentInformation(maSinhVien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenLop, tenNganh, tenKhoa);

                if (DeleteSuccess)
                {
                    // Thông báo thành công
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Sau khi xóa sinh viên thành công, làm mới DataGridView
                    LoadDataSVDataGridView();
                    ClearSVFields();
                }
                else
                {
                    // Thông báo thất bại
                    MessageBox.Show("Không thể xóa sinh viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu có lỗi xảy ra
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtSearchKeyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearchKeyword.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox và xóa khoảng trắng thừa

                if (!string.IsNullOrEmpty(keyword))
                {
                    DataProvider provider = new DataProvider();
                    DataTable searchResult = provider.SearchSV(keyword, keyword);

                    if (searchResult.Rows.Count > 0)
                    {
                        // Hiển thị kết quả tìm kiếm lên DataGridView tbSV
                        tbSV.DataSource = searchResult;
                        //MessageBox.Show("Tìm thấy sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //    else
                    //    {
                    //        // Nếu không tìm thấy kết quả
                    //        //MessageBox.Show("Không tìm thấy sinh viên nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                }
                //else
                //{
                //    MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btAgain_Click(object sender, EventArgs e)
        {
            ClearSVFields();
            LoadDataSVDataGridView();           
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            LoadDataSVDataGridView();
            LoadDataGVDataGridView();
            LoadDataLopDataGridView();
            LoadDataLHPDataGridView();
            LoadDataACDataGridView();
        }
        private void ClearSVFields()
        {
            // Xóa nội dung trong các ô textbox và combobox
            txtmsv.Clear();
            txttsv.Clear();
            dtNS.Value = DateTime.Now;
            cbGT.SelectedIndex = -1; 
            txtqH.Clear();
            txtqT.Clear();
            cbTL.SelectedIndex = -1; 
            cbNganh.SelectedIndex = -1; 
            cbKhoa.SelectedIndex = -1; 
        }
        private void ClearGVFields()
        {
            txtMgv.Clear();
            txtTGv.Clear();
            dtpGV.Value = DateTime.Now;
            cbgtGV.SelectedIndex = -1;
            txtqHgv.Clear();
            txtqTgv.Clear();
            cbKhoaGV.SelectedIndex = -1;
        }
        private void ClearLopFields()
        {
            txtML_Lop.Clear();
            txtTL_Lop.Clear();
            txtCVHT.Clear();
        }
        private void LoadDataSVDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();

                string query = @"
                SELECT 
                    MaSinhVien AS 'Mã sinh viên',
                    HoTen AS 'Tên sinh viên',
                    NgaySinh AS 'Ngày sinh',
                    GioiTinh AS 'Giới tính',
                    QueQuanHuyen AS 'Quê quán huyện',
                    QueQuanTinh AS 'Tỉnh/TP',
                    TenLop AS 'Tên lớp',
                    TenNganh AS 'Tên ngành',
                    TenKhoa AS 'Tên khoa'
                FROM 
                    SinhVien";

                DataTable dataTable = provider.ExecuteQuery(query);
                BindingSource b = new BindingSource();
                b.DataSource = dataTable;                       
                tbSV.DataSource = b;
                tbSV.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void tbSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu chỉ số dòng không phải là chỉ số header và không là dòng trống
            if (e.RowIndex >= 0 && e.RowIndex < tbSV.Rows.Count - 1)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = tbSV.Rows[e.RowIndex];
                // Hiển thị thông tin sinh viên lên các điều khiển trên form
                txtmsv.Text = row.Cells["Mã sinh viên"].Value.ToString();
                txttsv.Text = row.Cells["Tên sinh viên"].Value.ToString();
                dtNS.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                cbGT.SelectedItem = row.Cells["Giới tính"].Value.ToString();
                txtqH.Text = row.Cells["Quê quán huyện"].Value.ToString();
                txtqT.Text = row.Cells["Tỉnh/TP"].Value.ToString();
                cbTL.SelectedItem = row.Cells["Tên lớp"].Value.ToString();
                cbNganh.SelectedItem = row.Cells["Tên ngành"].Value.ToString();
                cbKhoa.SelectedItem = row.Cells["Tên khoa"].Value.ToString();
            }
        }

        private void LoadDataGVDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();

                string query = @"
	            SELECT 
                    MaGiangVien AS 'Mã giảng viên',
                    HoTen AS 'Tên giảng viên',
                    NgaySinh AS 'Ngày sinh',
                    GioiTinh AS 'Giới tính',
                    QueQuanHuyen AS 'Quê quán huyện',
                    QueQuanTinh AS 'Tỉnh/TP',
                    TenKhoa AS 'Tên khoa'
                FROM 
                    GiangVien";

                DataTable dataTable = provider.ExecuteQuery(query);

                //Tạo một BindingSource và gán DataTable cho nó
                BindingSource b = new BindingSource();
                b.DataSource = dataTable;
                // Gán dữ liệu vào DataGridView                          
                tbGv.DataSource = b;
                tbGv.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void tbGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu chỉ số dòng không phải là chỉ số header và không là dòng trống
            if (e.RowIndex >= 0 && e.RowIndex < tbSV.Rows.Count - 1)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = tbGv.Rows[e.RowIndex];
                // Hiển thị thông tin sinh viên lên các điều khiển trên form
                txtMgv.Text = row.Cells["Mã giảng viên"].Value.ToString();
                txtTGv.Text = row.Cells["Tên giảng viên"].Value.ToString();
                dtpGV.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                cbgtGV.SelectedItem = row.Cells["Giới tính"].Value.ToString();
                txtqHgv.Text = row.Cells["Quê quán huyện"].Value.ToString();
                txtqTgv.Text = row.Cells["Tỉnh/TP"].Value.ToString();
                cbKhoaGV.SelectedItem = row.Cells["Tên khoa"].Value.ToString();
            }
        }

        private void LoadDataLopDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();

                string query = @"
	            SELECT 
                    MaLop AS 'Mã lớp',
                    TenLop AS 'Tên lớp',
                    CoVanHocTap AS 'Cố vấn học tập'
                FROM 
                    Lop";

                DataTable dataTable = provider.ExecuteQuery(query);

                //Tạo một BindingSource và gán DataTable cho nó
                BindingSource b = new BindingSource();
                b.DataSource = dataTable;
                // Gán dữ liệu vào DataGridView                          
                tbLop.DataSource = b;
                tbLop.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void tbLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu chỉ số dòng không phải là chỉ số header và không là dòng trống
            if (e.RowIndex >= 0 && e.RowIndex < tbLop.Rows.Count - 1)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = tbLop.Rows[e.RowIndex];
                // Hiển thị thông tin lớp lên các điều khiển trên form
                txtML_Lop.Text = row.Cells["Mã lớp"].Value.ToString();
                txtTL_Lop.Text = row.Cells["Tên lớp"].Value.ToString();
                txtCVHT.Text = row.Cells["Cố vấn học tập"].Value.ToString();
            }
        }

        private void btAddGV_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string maGiangvien = txtMgv.Text;
                string hoTen = txtTGv.Text;
                DateTime ngaySinh = dtpGV.Value; // Chuyển đổi chuỗi thành kiểu DateTime
                string gioiTinh = cbgtGV.SelectedItem.ToString();
                string queQuanHuyen = txtqHgv.Text;
                string queQuanTinh = txtqTgv.Text;
                string tenKhoa = cbKhoaGV.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddTeacher để thêm giảng viên mới vào cơ sở dữ liệu
                bool addSuccess = provider.AddTeacher(maGiangvien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenKhoa);

                if (addSuccess)
                {
                    MessageBox.Show("Thêm giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi thêm giảng viên thành công, có thể làm một số công việc khác ở đây (ví dụ: làm mới DataGridView)
                    LoadDataGVDataGridView();
                    ClearGVFields();
                }
                else
                {
                    MessageBox.Show("Không thể thêm giảng viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btFixGV_Click(object sender, EventArgs e)
        {
            try
            {

                string maGiangvien = txtMgv.Text;
                string hoTen = txtTGv.Text;
                DateTime ngaySinh = dtpGV.Value; 
                string gioiTinh = cbgtGV.SelectedItem.ToString();
                string queQuanHuyen = txtqHgv.Text;
                string queQuanTinh = txtqTgv.Text;
                string tenKhoa = cbKhoaGV.SelectedItem.ToString();


                DataProvider provider = new DataProvider();

                bool updateSuccess = provider.UpdateTeacherInformation(maGiangvien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenKhoa);

                if (updateSuccess)
                {
                    MessageBox.Show("Sửa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGVDataGridView();
                    ClearGVFields();
                }
                else
                {

                    MessageBox.Show("Không thể sửa giảng viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btDeleteGV_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string maGiangvien = txtMgv.Text;
                string hoTen = txtTGv.Text;
                DateTime ngaySinh = dtpGV.Value; // Chuyển đổi chuỗi thành kiểu DateTime
                string gioiTinh = cbgtGV.SelectedItem.ToString();
                string queQuanHuyen = txtqHgv.Text;
                string queQuanTinh = txtqTgv.Text;
                string tenKhoa = cbKhoaGV.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức DeleteStudent để thêm giảng viên mới vào cơ sở dữ liệu
                bool DeleteSuccess = provider.DeleteTeacherInformation(maGiangvien, hoTen, ngaySinh, queQuanHuyen, gioiTinh, queQuanTinh, tenKhoa);

                if (DeleteSuccess)
                {
                    MessageBox.Show("Xóa giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi xóa giảng viên thành công, làm mới DataGridView và textbox
                    LoadDataGVDataGridView();
                    ClearGVFields();
                }
                else
                {
                    MessageBox.Show("Không thể xóa giảng viên. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchGV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearchGV.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox và xóa khoảng trắng thừa

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Gọi phương thức tìm kiếm theo tên giảng viên từ DataProvider
                    DataProvider provider = new DataProvider();
                    DataTable searchResult = provider.SearchGV(keyword);

                    if (searchResult.Rows.Count > 0)
                    {
                        // Hiển thị kết quả tìm kiếm lên DataGridView tbSV
                        tbGv.DataSource = searchResult;
                        //MessageBox.Show("Tìm thấy giảng viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm giảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btAgainGV_Click(object sender, EventArgs e)
        {
            ClearGVFields();
            LoadDataGVDataGridView();
        }
        //Lớp
        private void btAddLop_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox trên giao diện người dùng
                string malop = txtML_Lop.Text;
                string Tenlop = txtTL_Lop.Text;
                string Cvht = txtCVHT.Text;

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddStudent để thêm lớp mới vào cơ sở dữ liệu
                bool addSuccess = provider.AddClass(Tenlop, malop, Cvht);

                if (addSuccess)
                {
                    MessageBox.Show("Thêm lớp mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi thêm lớp thành công
                    LoadDataLopDataGridView();
                    ClearLopFields();
                }
                else
                {
                    MessageBox.Show("Không thể thêm lớp. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btFixLop_Click(object sender, EventArgs e)
        {
            try
            {
                string malop = txtML_Lop.Text;
                string Tenlop = txtTL_Lop.Text;
                string Cvht = txtCVHT.Text;

                DataProvider provider = new DataProvider();

                bool updateSuccess = provider.UpdateClassInformation(Tenlop, malop, Cvht);

                if (updateSuccess)
                {
                    MessageBox.Show("Sửa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataLopDataGridView(); // Nạp lại dữ liệu lớp vào DataGridView
                    ClearLopFields(); // Xóa các trường nhập liệu
                }
                else
                {
                    MessageBox.Show("Không thể sửa lớp. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btDeleteLop_Click(object sender, EventArgs e)
        {
            try
            {
                string malop = txtML_Lop.Text;
                string Tenlop = txtTL_Lop.Text;
                string Cvht = txtCVHT.Text;

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức DeleteStudent để thêm giảng viên mới vào cơ sở dữ liệu
                bool DeleteSuccess = provider.DeleteClassInformation(Tenlop, malop, Cvht);

                if (DeleteSuccess)
                {
                    MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataLopDataGridView();
                    ClearLopFields();
                }
                else
                {
                    MessageBox.Show("Không thể xóa lớp. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btAgainLop_Click(object sender, EventArgs e)
        {
            LoadDataLopDataGridView();
            ClearLopFields();
        }

        private void btSearchLop_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearchLop.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox và xóa khoảng trắng thừa

                if (!string.IsNullOrEmpty(keyword))
                {
                    DataProvider provider = new DataProvider();
                    DataTable searchResult = provider.SearchClass(keyword);

                    if (searchResult.Rows.Count > 0)
                    {
                        // Hiển thị kết quả tìm kiếm lên DataGridView tbSV
                        tbLop.DataSource = searchResult;
                        MessageBox.Show("Tìm thấy lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu không tìm thấy kết quả
                        MessageBox.Show("Không tìm thấy lớp nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Môn học phần
        private void LoadDataLHPDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();

                string query = @"
                SELECT 
                    MaLopHocPhan AS 'Mã lớp HP',
                    TenLop AS 'Tên lớp',
                    NgayHoc AS 'Ngày học',
                    TenHocPhan AS 'Tên học phần',
                    TC ,
                    SiSo AS 'Sĩ số',
                    MaHP AS 'Mã học phần',
                    MaGV AS 'Mã giảng viên',
                    HocKy AS 'Học kỳ'
                FROM 
                    LopHocPhan";

                DataTable dataTable = provider.ExecuteQuery(query);
                BindingSource b = new BindingSource();
                b.DataSource = dataTable;
                tbLHP.DataSource = b;
                tbLHP.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void ClearLHPFields()
        {
            txtMLHP.Clear();
            cbLHP.SelectedIndex = -1;
            dtpLHP.Value = DateTime.Now;
            txtTMHP.Clear();
            txtTC.Clear();
            txtSS.Clear();
            cbMDK.SelectedIndex = -1;
            cbMGV.SelectedIndex = -1;
            cbHKHP.SelectedIndex = -1;
        }

        private void addLHP_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox và ComboBox trên giao diện người dùng
                string mplhp = txtMLHP.Text;
                string tenLop = cbLHP.SelectedItem.ToString();
                DateTime ngayHoc = dtpLHP.Value;
                string tenHocPhan = txtTMHP.Text;
                int tc = int.Parse(txtTC.Text);
                string siSo = txtSS.Text;
                string maHP = cbMDK.SelectedItem.ToString();
                string maGV = cbMGV.SelectedItem.ToString();
                string hocKy = cbHKHP.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddLHP để thêm lớp học phần mới vào cơ sở dữ liệu
                bool addSuccess = provider.AddLHP(mplhp,tenLop, ngayHoc, tenHocPhan, tc, siSo, maHP, maGV, hocKy);

                if (addSuccess)
                {
                    MessageBox.Show("Thêm môn học phần mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataLHPDataGridView();
                    ClearLHPFields();
                }
                else
                {
                    MessageBox.Show("Không thể thêm môn học phần. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btUD_LHP_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox và ComboBox trên giao diện người dùng
                string mplhp = txtMLHP.Text;
                string tenLop = cbLHP.SelectedItem.ToString();
                DateTime ngayHoc = dtpLHP.Value;
                string tenHocPhan = txtTMHP.Text;
                int tc = int.Parse(txtTC.Text);
                string siSo = txtSS.Text;
                string maHP = cbMDK.SelectedItem.ToString();
                string maGV = cbMGV.SelectedItem.ToString();
                string hocKy = cbHKHP.SelectedItem.ToString();

                DataProvider provider = new DataProvider();

                bool updateSuccess = provider.UpdateLHP(mplhp, tenLop, ngayHoc, tenHocPhan, tc, siSo, maHP, maGV, hocKy);

                if (updateSuccess)
                {

                    MessageBox.Show("Sửa môn học phần thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataLHPDataGridView();
                    ClearLHPFields();
                }
                else
                {
                    MessageBox.Show("Không thể sửa môn HP. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btDeleteLHP_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox và ComboBox trên giao diện người dùng
                string mplhp = txtMLHP.Text;
                string tenLop = cbLHP.SelectedItem.ToString();
                DateTime ngayHoc = dtpLHP.Value;
                string tenHocPhan = txtTMHP.Text;
                int tc = int.Parse(txtTC.Text);
                string siSo = txtSS.Text;
                string maHP = cbMDK.SelectedItem.ToString();
                string maGV = cbMGV.SelectedItem.ToString();
                string hocKy = cbHKHP.SelectedItem.ToString();

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                bool DeleteSuccess = provider.DeleteLHP(mplhp, tenLop, ngayHoc, tenHocPhan, tc, siSo, maHP, maGV, hocKy);

                if (DeleteSuccess)
                {
                    MessageBox.Show("Xóa môn HP thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDataLHPDataGridView();
                    ClearLHPFields();
                }
                else
                {
                    MessageBox.Show("Không thể xóa môn HP. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbLHP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu chỉ số dòng không phải là chỉ số header và không là dòng trống
            if (e.RowIndex >= 0 && e.RowIndex < tbLHP.Rows.Count - 1)
            {
                // Lấy dòng được chọn
                DataGridViewRow row = tbLHP.Rows[e.RowIndex];
                // Hiển thị thông tin sinh viên lên các điều khiển trên form
                txtMLHP.Text = row.Cells["Mã lớp HP"].Value.ToString();
                cbLHP.SelectedItem = row.Cells["Tên lớp"].Value.ToString();
                dtpLHP.Value = Convert.ToDateTime(row.Cells["Ngày học"].Value);
                txtTMHP.Text = row.Cells["Tên học phần"].Value.ToString();
                txtTC.Text = row.Cells["TC"].Value.ToString();
                txtSS.Text = row.Cells["Sĩ số"].Value.ToString();
                cbMDK.SelectedItem = row.Cells["Mã học phần"].Value.ToString();
                cbMGV.SelectedItem = row.Cells["Mã giảng viên"].Value.ToString();
                cbHKHP.SelectedItem = row.Cells["Học kỳ"].Value.ToString();
            }
        }
        private void txtSearchSJ_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearchSJ.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox và xóa khoảng trắng thừa

                if (!string.IsNullOrEmpty(keyword))
                {
                    // Gọi phương thức tìm kiếm theo tên giảng viên từ DataProvider
                    DataProvider provider = new DataProvider();
                    DataTable searchResult = provider.SearchLHP(keyword);

                    if (searchResult.Rows.Count > 0)
                    {
                        tbLHP.DataSource = searchResult;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm môn học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAgainLHP_Click(object sender, EventArgs e)
        {
            ClearLHPFields();
        }
        //Account
        private void LoadDataACDataGridView()
        {
            try
            {
                DataProvider provider = new DataProvider();

                string query = @"
                SELECT 
                    Username AS 'Tên tài khoản',
                    Password AS 'Mật khẩu',
                    Vaitro AS 'Vai trò',
                    MaSinhVien AS 'Mã sinh viên',
                    MaGiangVien AS 'Mã giảng viên'
                FROM 
                    Account";

                DataTable dataTable = provider.ExecuteQuery(query);
                BindingSource b = new BindingSource();
                b.DataSource = dataTable;
                tbAC.DataSource = b;
                tbAC.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btAddAC_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các TextBox và ComboBox trên giao diện người dùng
                string tuser = txtUser.Text;
                string pass = txtPW.Text;
                string vaitro = cbVaiTro.SelectedItem.ToString();
                string msv = txtMsvAC.Text;
                string magv = txtMgvAC.Text;

                // Tạo một instance của DataProvider
                DataProvider provider = new DataProvider();

                // Gọi phương thức AddLHP để thêm lớp học phần mới vào cơ sở dữ liệu
                bool addSuccess = provider.AddAC(tuser, pass, vaitro, msv, magv);

                if (addSuccess)
                {
                    MessageBox.Show("Thêm tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Sau khi thêm lớp học phần thành công
                    LoadDataACDataGridView();
                }
                else
                {
                    MessageBox.Show("Không thể thêm tài khoản. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btUD_AC_Click(object sender, EventArgs e)
        {

        }

        private void btDelete_AC_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchAC_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
