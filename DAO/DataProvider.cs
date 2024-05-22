using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Đăng_Ký_Tín.DAO
{
    public class DataProvider
    {
        private string connectionSTR = "Data Source=MINH\\SQLEXPRESS;Initial Catalog=quanlydangkytin;Integrated Security=True";

        public DataTable ExecuteQuery(string query)
        {
            // Khởi tạo một đối tượng DataTable để lưu trữ kết quả truy vấn
            DataTable data = new DataTable();
            // Sử dụng khối using để đảm bảo việc giải phóng tài nguyên kết nối
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                // Mở kết nối tới cơ sở dữ liệu
                connection.Open();
                // Khởi tạo đối tượng SqlCommand để thực thi truy vấn trên kết nối đã mở
                SqlCommand command = new SqlCommand(query, connection);
                // Khởi tạo đối tượng SqlDataAdapter để điền kết quả của truy vấn vào DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                adapter.Fill(data);
                // Đóng kết nối sau khi hoàn thành việc lấy dữ liệu
                connection.Close();
            }
            // Trả về DataTable chứa kết quả của truy vấn
            return data;
        }

        public bool CheckLogin(string username, string password, out string role)
        {
            string query = "SELECT Vaitro FROM Account WHERE Username = @username AND Password = @password AND Vaitro IN ('Admin', 'User');";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();

                object result = command.ExecuteScalar(); // Lấy giá trị của cột Vaitro

                connection.Close();

                // Kiểm tra kết quả trả về từ câu truy vấn
                if (result != null && (result.ToString() == "Admin" || result.ToString() == "User"))
                {
                    role = result.ToString(); // Gán vai trò
                    return true; // Đăng nhập thành công
                }
                else
                {
                    role = null; // Không có vai trò hợp lệ
                    return false; // Đăng nhập thất bại
                }
            }
        }
        public DataTable GetUserDataByUsername(string username)
        {
            string query = @"
               SELECT 
                   SinhVien.MaSinhVien AS 'Mã sinh viên',
                   SinhVien.HoTen AS 'Tên sinh viên',
                   SinhVien.NgaySinh AS 'Ngày sinh',
                   SinhVien.GioiTinh AS 'Giới tính',
                   SinhVien.QueQuanHuyen AS 'Quê quán huyện',
                   SinhVien.QueQuanTinh AS 'Tỉnh/TP',
                   SinhVien.TenLop AS 'Tên lớp',
                   SinhVien.TenNganh AS 'Tên ngành',
                   SinhVien.TenKhoa AS 'Tên khoa'
               FROM 
                   Account
               JOIN 
                   SinhVien ON Account.MaSinhVien = SinhVien.MaSinhVien
               WHERE 
                   Account.Username = @username AND Account.Vaitro = 'User';";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();

                return dataTable;
            }
        }

        public DataTable GetUserDataByUsername1(string username)
        {
            string query = @"
               SELECT
                   GiangVien.MaGiangVien AS 'Mã giảng viên',
                   GiangVien.HoTen AS 'Họ và tên',
                   GiangVien.NgaySinh AS 'Ngày sinh',
                   GiangVien.GioiTinh AS 'Giới tính',
                   GiangVien.QueQuanHuyen AS 'Quê quán huyện',
                   GiangVien.QueQuanTinh AS 'Tỉnh/TP',
                   Khoa.TenKhoa AS 'Tên khoa',
                   Khoa.TruongKhoa AS 'Trưởng khoa'
               FROM
                   Account
               JOIN
                   GiangVien ON Account.MaGiangVien = GiangVien.MaGiangVien
               LEFT JOIN
                   Khoa ON GiangVien.TenKhoa = Khoa.TenKhoa
               WHERE
                   Account.Username = @username AND Account.Vaitro = 'Admin';"; 

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                connection.Close();

                return dataTable;
            }
        }
        public bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            string query = "UPDATE Account SET Password = @newPassword WHERE Username = @username AND Password = @currentPassword;";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@currentPassword", currentPassword);
                command.Parameters.AddWithValue("@newPassword", newPassword);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }

        //Sinh Viên
        public DataTable SearchSV(string hoTen, string maSinhVien)
        {
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
            FROM SinhVien
            WHERE HoTen LIKE @hoTen OR MaSinhVien LIKE @maSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@hoTen", "%" + hoTen + "%");
                command.Parameters.AddWithValue("@maSinhVien", "%" + maSinhVien + "%");

                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm sinh viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

                return dataTable;
            }
        }

        public bool DeleteStudentInformation(string maSinhVien, string hoTen, DateTime ngaySinh, string gioiTinh, string queQuanHuyen, string queQuanTinh, string tenLop, string tenNganh, string tenKhoa)
        {
            string query = @"
               DELETE SinhVien WHERE MaSinhVien = @maSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maSinhVien", maSinhVien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenLop", tenLop);
                command.Parameters.AddWithValue("@tenNganh", tenNganh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }

        public bool UpdateStudentInformation(string maSinhVien, string hoTen, DateTime ngaySinh, string gioiTinh, string queQuanHuyen, string queQuanTinh, string tenLop, string tenNganh, string tenKhoa)
        {
            string query = @"
               UPDATE SinhVien 
               SET HoTen = @hoTen,
                   NgaySinh = @ngaySinh,
                   GioiTinh = @gioiTinh,
                   QueQuanHuyen = @queQuanHuyen,
                   QueQuanTinh = @queQuanTinh,
                   TenLop = @tenLop,
                   TenNganh = @tenNganh,
                   TenKhoa = @tenKhoa
               WHERE MaSinhVien = @maSinhVien";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maSinhVien", maSinhVien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenLop", tenLop);
                command.Parameters.AddWithValue("@tenNganh", tenNganh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }    
        public bool AddStudent(string maSinhVien, string hoTen, DateTime ngaySinh, string queQuanHuyen, string gioiTinh, string queQuanTinh, string tenLop, string tenNganh, string tenKhoa)
        {
            // Kiểm tra xem tenNganh có tồn tại trong bảng Nganh không
            bool isNganhExists = CheckNganhExists(tenNganh);

            // Kiểm tra xem tenKhoa có tồn tại trong bảng Khoa không
            bool isKhoaExists = CheckKhoaExists(tenKhoa);

            // Kiểm tra xem tenLop có tồn tại trong bảng Lớp không
            bool isLopExists = CheckLopExists(tenLop);

            if (!isNganhExists || !isKhoaExists || !isLopExists)
            {
                // Hiển thị thông báo lỗi hoặc xử lý tương ứng nếu tenNganh hoặc tenKhoa không tồn tại
                MessageBox.Show("Tên ngành hoặc tên khoa không tồn tại trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string query = @"
               INSERT INTO SinhVien (MaSinhVien, HoTen, NgaySinh, QueQuanHuyen, GioiTinh, QueQuanTinh, TenLop, TenNganh, TenKhoa)
               VALUES (@maSinhVien, @hoTen, @ngaySinh, @queQuanHuyen, @gioiTinh, @queQuanTinh, @tenLop, @tenNganh, @tenKhoa);";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maSinhVien", maSinhVien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenLop", tenLop);
                command.Parameters.AddWithValue("@tenNganh", tenNganh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được thêm vào cơ sở dữ liệu
            }
        }
        //Giảng viên
        public DataTable SearchGV(string hoTen)
        {
            string query = @"
            SELECT                 
                MaGiangVien AS 'Mã giảng viên',
                HoTen AS 'Họ và tên',
                NgaySinh AS 'Ngày sinh',
                GioiTinh AS 'Giới tính',
                QueQuanHuyen AS 'Quê quán huyện',
                QueQuanTinh AS 'Tỉnh/TP',
                TenKhoa AS 'Tên khoa'
            FROM GiangVien
            WHERE HoTen LIKE @hoTen";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@hoTen", "%" + hoTen + "%");

                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm giảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

                return dataTable;
            }
        }
        public bool DeleteTeacherInformation(string maGiangvien, string hoTen, DateTime ngaySinh, string gioiTinh, string queQuanHuyen, string queQuanTinh, string tenKhoa)
        {
            string query = @"
               DELETE GiangVien WHERE MaGiangVien = @maGiangVien";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maGiangVien", maGiangvien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public bool UpdateTeacherInformation(string maGiangvien, string hoTen, DateTime ngaySinh, string gioiTinh, string queQuanHuyen, string queQuanTinh, string tenKhoa)
        {
            string query = @"
               UPDATE GiangVien 
               SET HoTen = @hoTen,
                   NgaySinh = @ngaySinh,
                   GioiTinh = @gioiTinh,
                   QueQuanHuyen = @queQuanHuyen,
                   QueQuanTinh = @queQuanTinh,
                   TenKhoa = @tenKhoa
               WHERE MaGiangVien = @maGiangVien";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maGiangVien", maGiangvien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public bool AddTeacher(string maGiangvien, string hoTen, DateTime ngaySinh, string queQuanHuyen, string gioiTinh, string queQuanTinh, string tenKhoa)
        {

            // Kiểm tra xem tenKhoa có tồn tại trong bảng Khoa không
            bool isKhoaExists = CheckKhoaExists(tenKhoa);

            if ( !isKhoaExists )
            {
                // Hiển thị thông báo lỗi hoặc xử lý tương ứng nếu tenNganh hoặc tenKhoa không tồn tại
                MessageBox.Show("Tên ngành hoặc tên khoa không tồn tại trong cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string query = @"
               INSERT INTO GiangVien (MaGiangVien, HoTen, NgaySinh, QueQuanHuyen, GioiTinh, QueQuanTinh, TenKhoa)
               VALUES (@maGiangVien, @hoTen, @ngaySinh, @queQuanHuyen, @gioiTinh, @queQuanTinh, @tenKhoa);";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maGiangVien", maGiangvien);
                command.Parameters.AddWithValue("@hoTen", hoTen);
                command.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                command.Parameters.AddWithValue("@queQuanHuyen", queQuanHuyen);
                command.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                command.Parameters.AddWithValue("@queQuanTinh", queQuanTinh);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được thêm vào cơ sở dữ liệu
            }
        }
        //Lớp
        public bool AddClass(string Tenlop, string malop, string Cvht)
        {

            string query = @"
               INSERT INTO Lop (TenLop, MaLop, CoVanHocTap)
               VALUES (@Tenlop, @maLop, @Cvht);";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maLop", malop);
                command.Parameters.AddWithValue("@Tenlop", Tenlop);
                command.Parameters.AddWithValue("@Cvht", Cvht);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được thêm vào cơ sở dữ liệu
            }
        }
        public bool UpdateClassInformation(string Tenlop, string malop, string Cvht)
        {
            string query = @"
               UPDATE Lop
               SET TenLop = @Tenlop,
                   MaLop = @maLop,
                   CoVanHocTap = @Cvht
               WHERE TenLop = @Tenlop";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maLop", malop);
                command.Parameters.AddWithValue("@Tenlop", Tenlop);
                command.Parameters.AddWithValue("@Cvht", Cvht);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public bool DeleteClassInformation(string Tenlop, string malop, string Cvht)
        {
            string query = @"
               DELETE Lop WHERE MaLop = @maLop";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@maLop", malop);
                command.Parameters.AddWithValue("@Tenlop", Tenlop);
                command.Parameters.AddWithValue("@Cvht", Cvht);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public DataTable SearchClass(string Tenlop)
        {
            string query = @"
            SELECT 
               MaLop AS 'Mã lớp',
               TenLop AS 'Tên lớp',               
               CoVanHocTap AS 'Cố vấn học tập'
            FROM Lop
            WHERE TenLop LIKE @Tenlop";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Tenlop", "%" + Tenlop + "%");

                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

                return dataTable;
            }
        }
        private bool CheckNganhExists(string tenNganh)
        {
            string query = "SELECT COUNT(*) FROM Nganh WHERE TenNganh = @tenNganh;";
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenNganh", tenNganh);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count > 0;
            }
        }

        private bool CheckKhoaExists(string tenKhoa)
        {
            string query = "SELECT COUNT(*) FROM Khoa WHERE TenKhoa = @tenKhoa;";
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenKhoa", tenKhoa);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count > 0;
            }
        }

        private bool CheckLopExists(string tenLop)
        {
            string query = "SELECT COUNT(*) FROM Lop WHERE TenLop = @tenLop;";
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenLop", tenLop);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                return count > 0;
            }
        }

        // Phương thức lấy danh sách Tên ngành từ cơ sở dữ liệu
        public List<string> GetTenNganhList()
        {
            List<string> tenNganhList = new List<string>();

            string query = "SELECT TenNganh FROM Nganh";

            DataTable data = ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string tenNganh = row["TenNganh"].ToString();
                tenNganhList.Add(tenNganh);
            }

            return tenNganhList;
        }

        // Phương thức lấy danh sách Tên khoa từ cơ sở dữ liệu
        public List<string> GetTenKhoaList()
        {
            List<string> tenKhoaList = new List<string>();

            string query = "SELECT TenKhoa FROM Khoa";

            DataTable data = ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string tenKhoa = row["TenKhoa"].ToString();
                tenKhoaList.Add(tenKhoa);
            }

            return tenKhoaList;
        }
        // Phương thức lấy danh sách Tên lớp từ cơ sở dữ liệu
        public List<string> GetTenLopList()
        {
            List<string> tenLopList = new List<string>();

            string query = "SELECT TenLop FROM Lop";

            DataTable data = ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string tenLop = row["TenLop"].ToString();
                tenLopList.Add(tenLop);
            }

            return tenLopList;
        }
        public List<string> GetMGVList()
        {
            List<string> mgvList = new List<string>();

            string query = "SELECT MaGiangVien FROM GiangVien";

            DataTable data = ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string mgv= row["MaGiangVien"].ToString();
                mgvList.Add(mgv);
            }

            return mgvList;
        }
        public List<string> GetMDKList()
        {
            List<string> mdkList = new List<string>();

            string query = "SELECT MaDangKy FROM HocPhan";

            DataTable data = ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string mdk = row["MaDangKy"].ToString();
                mdkList.Add(mdk);
            }

            return mdkList;
        }
        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        //Lớp học phần 
        public bool AddLHP(string mplhp, string tenLop, DateTime ngayHoc, string tenHocPhan, int tc, string siSo, string maHP, string maGV, string hocKy)
        {
            string query = @"
       INSERT INTO LopHocPhan (MaLopHocPhan, TenLop, NgayHoc, TenHocPhan, TC, SiSo, MaHP, MaGV, HocKy)
       VALUES (@MPLHP, @TenLop, @NgayHoc, @TenHocPhan, @TC, @SiSo, @MaHP, @MaGV, @HocKy);";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MPLHP", mplhp);
                command.Parameters.AddWithValue("@TenLop", tenLop);
                command.Parameters.AddWithValue("@NgayHoc", ngayHoc);
                command.Parameters.AddWithValue("@TenHocPhan", tenHocPhan);
                command.Parameters.AddWithValue("@TC", tc);
                command.Parameters.AddWithValue("@SiSo", siSo);
                command.Parameters.AddWithValue("@MaHP", maHP);
                command.Parameters.AddWithValue("@MaGV", maGV);
                command.Parameters.AddWithValue("@HocKy", hocKy);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Return true if at least one row was inserted into the database
            }
        }
        public bool UpdateLHP(string mplhp, string tenLop, DateTime ngayHoc, string tenHocPhan, int tc, string siSo, string maHP, string maGV, string hocKy)
        {
            string query = @"
            UPDATE LopHocPhan
            SET TenLop = @TenLop,
                NgayHoc = @NgayHoc,
                TenHocPhan = @TenHocPhan,
                TC = @TC,
                SiSo = @SiSo,
                MaHP = @MaHP,
                MaGV = @MaGV,
                HocKy = @HocKy
            WHERE MaLopHocPhan = @MPLHP;";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MPLHP", mplhp);
                command.Parameters.AddWithValue("@TenLop", tenLop);
                command.Parameters.AddWithValue("@NgayHoc", ngayHoc);
                command.Parameters.AddWithValue("@TenHocPhan", tenHocPhan);
                command.Parameters.AddWithValue("@TC", tc);
                command.Parameters.AddWithValue("@SiSo", siSo);
                command.Parameters.AddWithValue("@MaHP", maHP);
                command.Parameters.AddWithValue("@MaGV", maGV);
                command.Parameters.AddWithValue("@HocKy", hocKy);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public bool DeleteLHP(string mplhp, string tenLop, DateTime ngayHoc, string tenHocPhan, int tc, string siSo, string maHP, string maGV, string hocKy)
        {
            string query = @"
               DELETE LopHocPhan WHERE MaLopHocPhan = @MPLHP";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MPLHP", mplhp);
                command.Parameters.AddWithValue("@TenLop", tenLop);
                command.Parameters.AddWithValue("@NgayHoc", ngayHoc);
                command.Parameters.AddWithValue("@TenHocPhan", tenHocPhan);
                command.Parameters.AddWithValue("@TC", tc);
                command.Parameters.AddWithValue("@SiSo", siSo);
                command.Parameters.AddWithValue("@MaHP", maHP);
                command.Parameters.AddWithValue("@MaGV", maGV);
                command.Parameters.AddWithValue("@HocKy", hocKy);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Trả về true nếu có ít nhất một hàng được cập nhật
            }
        }
        public DataTable SearchLHP(string tenHocPhan)
        {
            string query = @"
        SELECT 
            MaLopHocPhan AS 'Mã lớp HP',
            TenLop AS 'Tên lớp',
            NgayHoc AS 'Ngày học',
            TenHocPhan AS 'Tên học phần',
            TC,
            SiSo AS 'Sĩ số',
            MaHP AS 'Mã học phần',
            MaGV AS 'Mã giảng viên',
            HocKy AS 'Học kỳ'
        FROM 
            LopHocPhan
        WHERE TenHocPhan LIKE @TenHocPhan;";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenHocPhan", "%" + tenHocPhan + "%");

                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm môn HP: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connection.Close();
                }

                return dataTable;
            }
        }

        //Account
        public bool AddAC(string tuser, string pass, string vaitro, string msv, string magv)
        {
            string query = @"
            INSERT INTO Account (Username, Password, Vaitro, MaSinhVien, MaGiangVien)
            VALUES (@tuser, @pass, @vtro, @msv, @magv);";

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tuser", tuser);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@vtro", vaitro);
                command.Parameters.AddWithValue("@msv", msv);
                command.Parameters.AddWithValue("@magv", magv);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                return rowsAffected > 0; // Return true if at least one row was inserted into the database
            }
        }

    }
}

