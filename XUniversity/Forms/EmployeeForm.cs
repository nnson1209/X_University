using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace XUniversity.Forms
{
    public partial class EmployeeForm : Form
    {
        private OracleConnection conn;
        private string username;
        private string role;
        private string connString;

        public EmployeeForm(OracleConnection connection, string user, string userRole)
        {
            conn = connection;
            connString = connection.ConnectionString;
            username = user;
            role = userRole;

            InitializeComponent();
            lblWelcome.Text = $"Xin chào {username} ({role})!";

            // XỬ lý theo tab được chọn
            this.tabControlMain.SelectedIndexChanged += new EventHandler(this.tabControlMain_SelectedIndexChanged);
            tabControlMain_SelectedIndexChanged(this.tabControlMain, EventArgs.Empty);
        }

        //Xử lý khi tab được chọn thay đổi.
        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab == tabNhanVien)
            {
                SetPermissionByRole_NhanVien();
                btnLoadData_NV.PerformClick();
            }
            else if (tabControlMain.SelectedTab == tabMoMon)
            {
                SetPermissionByRole_MoMon();
                btnLoadData_MM.PerformClick();
            }
            else if (tabControlMain.SelectedTab == tabSinhVien)
            {
                SetPermissionByRole_SinhVien();
                btnLoadData_SV.PerformClick();
            }
            else if (tabControlMain.SelectedTab == tabDangKy)
            {
                SetPermissionByRole_DangKy();
                btnLoadData_DK.PerformClick();
            }
            // else
            // {
            //     MessageBox.Show("Bạn đang ở tab: " + tabControlMain.SelectedTab.Text);
            // }
        }

        private void SetPermissionByRole_NhanVien()
        {
            txtHoTen.ReadOnly = true;
            txtLuong.ReadOnly = true;
            txtPhuCap.ReadOnly = true;
            txtVaiTro.ReadOnly = true;
            txtMaDV.ReadOnly = true;
            txtNgaySinh.ReadOnly = true;
            txtPhai.ReadOnly = true;
            txtSoDT.ReadOnly = true;

            btnInsert_NV.Enabled = false;
            btnUpdate_NV.Enabled = true;
            btnDelete_NV.Enabled = false;

            // Quyền FULL của NV_TCHC
            if (role == "NV_TCHC")
            {
                txtHoTen.ReadOnly = false;
                txtLuong.ReadOnly = false;
                txtPhuCap.ReadOnly = false;
                txtVaiTro.ReadOnly = false;
                txtMaDV.ReadOnly = false;
                txtNgaySinh.ReadOnly = false;
                txtPhai.ReadOnly = false;
                txtSoDT.ReadOnly = false;

                btnInsert_NV.Enabled = true;
                btnUpdate_NV.Enabled = true;
                btnDelete_NV.Enabled = true;
            }
            // Quyền TRGDV: chỉ xem các nhân viên cùng MADV, trừ lương và phụ cấp
            else if (role == "TRGDV")
            {
                txtHoTen.ReadOnly = true;
                txtLuong.ReadOnly = false;
                txtPhuCap.ReadOnly = false;
                txtVaiTro.ReadOnly = true;
                txtMaDV.ReadOnly = true;
                txtNgaySinh.ReadOnly = true;
                txtPhai.ReadOnly = true;
                txtSoDT.ReadOnly = true;
            }
            else if (role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB")
            {
                txtSoDT.ReadOnly = false;
            }
            else
            {
                btnLoadData_NV.Enabled = false;
                btnInsert_NV.Enabled = false;
                btnUpdate_NV.Enabled = false;
                btnDelete_NV.Enabled = false;
            }
        }

        private void SetPermissionByRole_MoMon()
        {
            txtMaMM.ReadOnly = true;
            txtMaHP.ReadOnly = true;
            txtMaGV.ReadOnly = true;
            txtHK.ReadOnly = true;
            txtNam.ReadOnly = true;

            btnLoadData_MM.Enabled = true;
            btnInsert_MM.Enabled = false;
            btnUpdate_MM.Enabled = false;
            btnDelete_MM.Enabled = false;

            if (role == "NV_PDT")
            {
                txtMaMM.ReadOnly = false;
                txtMaHP.ReadOnly = false;
                txtMaGV.ReadOnly = false;
                txtHK.ReadOnly = false;
                txtNam.ReadOnly = false;

                btnInsert_MM.Enabled = true;
                btnUpdate_MM.Enabled = true;
                btnDelete_MM.Enabled = true;
            }
            else if (role == "GV")
            {
                // Chỉ cho phép load
            }
            else if (role == "TRGDV")
            {
                // Chỉ cho phép load
            }
            else if (role == "SINHVIEN")
            {
                // Chỉ cho phép load
            }
            else
            {
                btnLoadData_MM.Enabled = false;
            }
        }

        // Trong EmployeeForm.cs
        private void SetPermissionByRole_SinhVien()
        {
            // Mặc định: tất cả các trường chỉ xem, các nút chỉnh sửa tắt
            txtMaSV.ReadOnly = true;
            txtHoTenSV.ReadOnly = true;
            txtPhaiSV.ReadOnly = true;
            txtNgaySinhSV.ReadOnly = true;
            txtDchiSV.ReadOnly = true;
            txtDtSV.ReadOnly = true;
            txtKhoaSV.ReadOnly = true;
            txtTinhTrang.ReadOnly = true;

            btnLoadData_SV.Enabled = true; // Luôn cho phép load data nếu user có thể thấy tab
            btnInsert_SV.Enabled = false;
            btnUpdate_SV.Enabled = false;
            btnDelete_SV.Enabled = false;

            // Dựa vào chính sách VPD f_sinhvien_policy và ADD_POLICY:
            if (role == "SINHVIEN")
            {
                // SINHVIEN: SELECT (của mình), UPDATE (của mình), DELETE (của mình)
                // Không có quyền INSERT sinh viên mới
                txtHoTenSV.ReadOnly = false;
                txtPhaiSV.ReadOnly = false;
                txtNgaySinhSV.ReadOnly = false;
                txtDchiSV.ReadOnly = false;
                txtDtSV.ReadOnly = false;
                txtKhoaSV.ReadOnly = true; // SV không thể thay đổi Khoa của mình
                txtTinhTrang.ReadOnly = true; // SV không thể thay đổi Tình Trạng

                btnUpdate_SV.Enabled = true; // SV có thể update thông tin của mình
                btnDelete_SV.Enabled = true; // SV có thể delete (hủy hồ sơ? cần xem xét nghiệp vụ)
                                             // btnInsert_SV.Enabled = false; // SV không có quyền thêm mới SV

                MessageBox.Show("Chào mừng sinh viên! Bạn có thể xem và cập nhật thông tin cá nhân của mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "NV_CTSV")
            {
                // NV_CTSV: SELECT, INSERT, UPDATE, DELETE (toàn bộ sinh viên)
                txtMaSV.ReadOnly = false; // Khi insert, có thể nhập mã SV
                txtHoTenSV.ReadOnly = false;
                txtPhaiSV.ReadOnly = false;
                txtNgaySinhSV.ReadOnly = false;
                txtDchiSV.ReadOnly = false;
                txtDtSV.ReadOnly = false;
                txtKhoaSV.ReadOnly = false;
                txtTinhTrang.ReadOnly = true; // TINHTRANG được set NULL bởi trigger khi INSERT, và NV_CTSV không sửa trực tiếp

                btnInsert_SV.Enabled = true;
                btnUpdate_SV.Enabled = true;
                btnDelete_SV.Enabled = true;

                MessageBox.Show("Chào mừng NV CTSV! Bạn có toàn quyền quản lý thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "NV_PDT")
            {
                // NV_PDT: SELECT, UPDATE, DELETE (toàn bộ sinh viên)
                txtMaSV.ReadOnly = true; // NV_PDT không thêm mã SV mới
                txtHoTenSV.ReadOnly = false;
                txtPhaiSV.ReadOnly = false;
                txtNgaySinhSV.ReadOnly = false;
                txtDchiSV.ReadOnly = false;
                txtDtSV.ReadOnly = false;
                txtKhoaSV.ReadOnly = false;
                txtTinhTrang.ReadOnly = false; // NV_PDT có thể sửa Tình trạng

                btnInsert_SV.Enabled = false; // Policy không cấp quyền INSERT cho NV_PDT
                btnUpdate_SV.Enabled = true;
                btnDelete_SV.Enabled = true;

                MessageBox.Show("Chào mừng NV PĐT! Bạn có quyền xem và sửa thông tin sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "GV")
            {
                // GV: SELECT (sinh viên trong khoa mình)
                // Các textbox và nút điều khiển mặc định là ReadOnly/Disabled
                MessageBox.Show("Chào mừng Giảng viên! Bạn chỉ có quyền xem thông tin sinh viên trong khoa của mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "TRGDV")
            {
                // TRGDV: SELECT (sinh viên trong khoa mình)
                // Các textbox và nút điều khiển mặc định là ReadOnly/Disabled
                MessageBox.Show("Chào mừng Trưởng Đơn Vị! Bạn chỉ có quyền xem thông tin sinh viên trong đơn vị của mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Các role khác: 1=0 (không quyền)
                btnLoadData_SV.Enabled = false;
                MessageBox.Show("Bạn không có quyền truy cập tab Sinh viên.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SetPermissionByRole_DangKy()
        {
            // Mặc định: tất cả các trường chỉ xem, các nút chỉnh sửa tắt
            txtMaSVDK.ReadOnly = true;
            txtMaMMDK.ReadOnly = true;
            txtDiemTH.ReadOnly = true;
            txtDiemQT.ReadOnly = true;
            txtDiemCK.ReadOnly = true;
            txtDiemTK.ReadOnly = true;

            btnLoadData_DK.Enabled = true;
            btnInsert_DK.Enabled = false;
            btnUpdate_DK.Enabled = false;
            btnDelete_DK.Enabled = false;

            // Chú ý: Sử dụng tên vai trò đúng như trong bảng USER_ROLES và NHANVIEN (ví dụ: 'NV_PDT' thay vì 'NV PĐT')
            if (role == "SINHVIEN")
            {
                txtMaSVDK.ReadOnly = false; // Sinh viên có thể nhập MASV của mình
                txtMaMMDK.ReadOnly = false;

                btnInsert_DK.Enabled = true;
                btnUpdate_DK.Enabled = true;
                btnDelete_DK.Enabled = true;

                MessageBox.Show("Chào mừng sinh viên! Bạn có thể đăng ký, sửa hoặc hủy môn học của mình trong thời gian cho phép nếu chưa có điểm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "NV_PDT") // Vai trò 'NV_PDT' trong database
            {
                txtMaSVDK.ReadOnly = false;
                txtMaMMDK.ReadOnly = false;

                btnInsert_DK.Enabled = true;
                btnUpdate_DK.Enabled = true;
                btnDelete_DK.Enabled = true;

                MessageBox.Show("Chào mừng NV PĐT! Bạn có thể quản lý đăng ký môn học trong thời gian cho phép.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "NV_PKT") // Vai trò 'NV_PKT' trong database
            {
                txtDiemTH.ReadOnly = false;
                txtDiemQT.ReadOnly = false;
                txtDiemCK.ReadOnly = false;
                txtDiemTK.ReadOnly = false;

                btnUpdate_DK.Enabled = true; // NV PKT có quyền cập nhật điểm

                MessageBox.Show("Chào mừng NV PKT! Bạn có quyền xem toàn bộ đăng ký và cập nhật điểm số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "GV") // Vai trò 'GV' trong database
            {
                // GV chỉ có quyền SELECT, không có quyền INSERT/UPDATE/DELETE trên DANGKY thông thường
                // Chính sách vpd_gv_dangky đã giới hạn SELECT theo MAGV
                MessageBox.Show("Chào mừng Giảng viên! Bạn chỉ có quyền xem danh sách lớp và bảng điểm các lớp mình dạy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (role == "TRGDV") // Vai trò TRGDV trong database
            {
                // TRGDV thường có quyền SELECT toàn bộ hoặc theo đơn vị, không có quyền INSERT/UPDATE/DELETE điểm
                // Có thể mở rộng quyền nếu cần thiết
                txtMaSVDK.ReadOnly = true;
                txtMaMMDK.ReadOnly = true;
                txtDiemTH.ReadOnly = true;
                txtDiemQT.ReadOnly = true;
                txtDiemCK.ReadOnly = true;
                txtDiemTK.ReadOnly = true;

                btnInsert_DK.Enabled = false;
                btnUpdate_DK.Enabled = false;
                btnDelete_DK.Enabled = false;

                MessageBox.Show("Chào mừng Trưởng Đơn Vị! Bạn có quyền xem toàn bộ đăng ký trong đơn vị của mình.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                btnLoadData_DK.Enabled = false;
                MessageBox.Show("Bạn không có quyền truy cập tab Đăng Ký.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnLoadData_NV_Click(object sender, EventArgs e)
        {
            string viewName = "";
            switch (role)
            {
                case "TRGDV":
                    viewName = "V_NHANVIEN_TRGDV";
                    break;
                case "NV_TCHC":
                    viewName = "V_NHANVIEN_TCHC";
                    break;
                case "GV":
                case "NV_PDT":
                case "NV_PKT":
                case "NV_CTSV":
                case "NVCB":
                    viewName = "V_NHANVIEN_NVCB";
                    break;
                case "SINHVIEN":
                    MessageBox.Show("Bạn không có quyền truy cập tab Nhân viên.");
                    dgvNhanVien.DataSource = null;
                    return;
                default:
                    MessageBox.Show("Role không được hỗ trợ cho tab Nhân viên.");
                    dgvNhanVien.DataSource = null;
                    return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sqlQuery = $"SELECT * FROM ADMIN_OLS.{viewName}";
                    OracleDataAdapter adapter = new OracleDataAdapter(sqlQuery, connectionNew);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load data for Nhan Vien failed: " + ex.Message);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var row = dgvNhanVien.SelectedRows[0];
                txtMaNV.Text = row.Cells["MANLD"].Value?.ToString();
                txtHoTen.Text = row.Cells["HOTEN"].Value?.ToString();
                txtPhai.Text = row.Cells["PHAI"].Value?.ToString();
                txtNgaySinh.Text = row.Cells["NGSINH"].Value?.ToString();
                txtSoDT.Text = row.Cells["DT"].Value?.ToString();
                txtLuong.Text = dgvNhanVien.Columns.Contains("LUONG") ? row.Cells["LUONG"].Value?.ToString() : "";
                txtPhuCap.Text = dgvNhanVien.Columns.Contains("PHUCAP") ? row.Cells["PHUCAP"].Value?.ToString() : "";
                txtVaiTro.Text = row.Cells["VAITRO"].Value?.ToString();
                txtMaDV.Text = row.Cells["MADV"].Value?.ToString();
            }
            else
            {
                // Clear textboxes if no row is selected
                txtMaNV.Text = "";
                txtHoTen.Text = "";
                txtPhai.Text = "";
                txtNgaySinh.Text = "";
                txtSoDT.Text = "";
                txtLuong.Text = "";
                txtPhuCap.Text = "";
                txtVaiTro.Text = "";
                txtMaDV.Text = "";
            }
        }

        private void btnInsert_NV_Click(object sender, EventArgs e)
        {
            string viewName = "V_NHANVIEN_TCHC"; // Chỉ NV_TCHC mới có quyền INSERT
            if (role != "NV_TCHC")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Insert trên tab Nhân Viên.");
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"INSERT INTO ADMIN_OLS.{viewName}
                                    (MANLD, HOTEN, PHAI, NGSINH, DT, LUONG, PHUCAP, VAITRO, MADV)
                                    VALUES (:manld, :hoten, :phai, TO_DATE(:ngsinh, 'DD-MM-YYYY'), :dt, :luong, :phucap, :vaitro, :madv)";
                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("manld", txtMaNV.Text);
                        cmd.Parameters.Add("hoten", txtHoTen.Text);
                        cmd.Parameters.Add("phai", txtPhai.Text);
                        cmd.Parameters.Add("ngsinh", txtNgaySinh.Text);
                        cmd.Parameters.Add("dt", txtSoDT.Text);
                        cmd.Parameters.Add("luong", string.IsNullOrEmpty(txtLuong.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtLuong.Text));
                        cmd.Parameters.Add("phucap", string.IsNullOrEmpty(txtPhuCap.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPhuCap.Text));
                        cmd.Parameters.Add("vaitro", txtVaiTro.Text);
                        cmd.Parameters.Add("madv", txtMaDV.Text);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Inserted {rows} row(s).");
                    }
                }
                btnLoadData_NV.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert for Nhan Vien failed: " + ex.Message);
            }
        }

        private void btnUpdate_NV_Click(object sender, EventArgs e)
        {
            string viewName = "";

            if (role == "NV_TCHC")
            {
                viewName = "V_NHANVIEN_TCHC";
            }
            else if (role == "TRGDV" || role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB")
            {
                viewName = "V_NHANVIEN_NVCB"; 
            }
            else
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Update trên tab Nhân Viên.");
                return;
            }

            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.");
                return;
            }

            string originalMaNV = dgvNhanVien.SelectedRows[0].Cells["MANLD"].Value?.ToString();
            if (string.IsNullOrEmpty(originalMaNV))
            {
                MessageBox.Show("Không tìm thấy mã nhân viên để cập nhật.");
                return;
            }

            // Đối với các role chỉ được sửa SĐT của mình
            if ((role == "TRGDV" || role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB") && originalMaNV != username)
            {
                MessageBox.Show("Bạn chỉ có thể cập nhật thông tin của chính mình.");
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql;
                    OracleCommand cmd;

                    if (role == "NV_TCHC")
                    {
                        sql = $@"UPDATE ADMIN_OLS.{viewName}
                                    SET HOTEN = :hoten,
                                        PHAI = :phai,
                                        NGSINH = TO_DATE(:ngsinh, 'DD-MM-YYYY'),
                                        DT = :dt,
                                        LUONG = :luong,
                                        PHUCAP = :phucap,
                                        VAITRO = :vaitro,
                                        MADV = :madv
                                    WHERE MANLD = :original_manld";
                        cmd = new OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("hoten", txtHoTen.Text);
                        cmd.Parameters.Add("phai", txtPhai.Text);
                        cmd.Parameters.Add("ngsinh", txtNgaySinh.Text);
                        cmd.Parameters.Add("dt", txtSoDT.Text);
                        cmd.Parameters.Add("luong", string.IsNullOrEmpty(txtLuong.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtLuong.Text));
                        cmd.Parameters.Add("phucap", string.IsNullOrEmpty(txtPhuCap.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPhuCap.Text));
                        cmd.Parameters.Add("vaitro", txtVaiTro.Text);
                        cmd.Parameters.Add("madv", txtMaDV.Text);
                        cmd.Parameters.Add("original_manld", originalMaNV);
                    }
                    else
                    {
                        sql = $@"UPDATE ADMIN_OLS.{viewName}
                                    SET DT = :dt
                                    WHERE MANLD = :original_manld";
                        cmd = new OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("dt", txtSoDT.Text);
                        cmd.Parameters.Add("original_manld", originalMaNV);
                    }

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Updated {rows} row(s).");
                }
                btnLoadData_NV.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update for Nhan Vien failed: " + ex.Message);
            }
        }

        private void btnDelete_NV_Click(object sender, EventArgs e)
        {
            string viewName = "V_NHANVIEN_TCHC"; // Chỉ NV_TCHC mới có quyền DELETE
            if (role != "NV_TCHC")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Delete trên tab Nhân Viên.");
                return;
            }

            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
                return;
            }
            string manvToDelete = dgvNhanVien.SelectedRows[0].Cells["MANLD"].Value?.ToString();

            if (string.IsNullOrEmpty(manvToDelete))
            {
                MessageBox.Show("Không tìm thấy mã nhân viên để xóa.");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên có mã {manvToDelete}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"DELETE FROM ADMIN_OLS.{viewName} WHERE MANLD = :manld_param";
                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("manld_param", manvToDelete);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Deleted {rows} row(s).");
                    }
                }
                btnLoadData_NV.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete for Nhan Vien failed: " + ex.Message);
            }
        }

        private void btnLoadData_MM_Click(object sender, EventArgs e)
        {
            string viewName = "";
            switch (role)
            {
                case "GV":
                    viewName = "V_MOMON_GV";
                    break;
                case "NV_PDT":
                    viewName = "V_MOMON_NV_PDT";
                    break;
                case "TRGDV":
                    viewName = "V_MOMON_TRGDV";
                    break;
                case "SINHVIEN":
                    viewName = "V_MOMON_SINHVIEN";
                    break;
                default:
                    MessageBox.Show("Role không được hỗ trợ cho tab Mở Môn.");
                    dgvMoMon.DataSource = null;
                    return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sqlQuery = $"SELECT * FROM ADMIN_OLS.{viewName}";
                    OracleDataAdapter adapter = new OracleDataAdapter(sqlQuery, connectionNew);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvMoMon.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load failed: " + ex.Message);
            }
        }

        private void dgvMoMon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMoMon.SelectedRows.Count > 0)
            {
                var row = dgvMoMon.SelectedRows[0];
                txtMaMM.Text = row.Cells["MAMM"].Value?.ToString();
                txtMaHP.Text = row.Cells["MAHP"].Value?.ToString();
                txtMaGV.Text = row.Cells["MAGV"].Value?.ToString();
                txtHK.Text = row.Cells["HK"].Value?.ToString(); // Tên cột là HK trong DDL bạn cung cấp
                txtNam.Text = row.Cells["NAM"].Value?.ToString();
            }
            else
            {
                // Clear textboxes if no row is selected
                txtMaMM.Text = "";
                txtMaHP.Text = "";
                txtMaGV.Text = "";
                txtHK.Text = "";
                txtNam.Text = "";
            }
        }

        private void btnInsert_MM_Click(object sender, EventArgs e)
        {
            string viewName = "V_MOMON_NV_PDT"; // Chỉ NV_PDT mới có quyền INSERT
            if (role != "NV_PDT")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Insert trên tab Mở Môn.");
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"INSERT INTO ADMIN_OLS.{viewName}
                                    (MAMM, MAHP, MAGV, HK, NAM)
                                    VALUES (:mamm, :mahp, :magv, :hk, :nam)";
                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("mamm", txtMaMM.Text);
                        cmd.Parameters.Add("mahp", txtMaHP.Text);
                        cmd.Parameters.Add("magv", txtMaGV.Text);
                        cmd.Parameters.Add("hk", string.IsNullOrEmpty(txtHK.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHK.Text));
                        cmd.Parameters.Add("nam", string.IsNullOrEmpty(txtNam.Text) ? (object)DBNull.Value : Convert.ToInt32(txtNam.Text));

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Inserted {rows} row(s).");
                    }
                }
                btnLoadData_MM.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert for Mo Mon failed: " + ex.Message);
            }
        }

        private void btnUpdate_MM_Click(object sender, EventArgs e)
        {
            string viewName = "V_MOMON_NV_PDT"; // Chỉ NV_PDT mới có quyền UPDATE
            if (role != "NV_PDT")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Update trên tab Mở Môn.");
                return;
            }

            if (dgvMoMon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.");
                return;
            }

            string originalMaMM = dgvMoMon.SelectedRows[0].Cells["MAMM"].Value?.ToString();
            if (string.IsNullOrEmpty(originalMaMM))
            {
                MessageBox.Show("Không tìm thấy mã mở môn để cập nhật.");
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"UPDATE ADMIN_OLS.{viewName}
                                    SET MAHP = :mahp,
                                        MAGV = :magv,
                                        HK = :hk,
                                        NAM = :nam
                                    WHERE MAMM = :original_mamm";

                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("mahp", txtMaHP.Text);
                        cmd.Parameters.Add("magv", txtMaGV.Text);
                        cmd.Parameters.Add("hk", string.IsNullOrEmpty(txtHK.Text) ? (object)DBNull.Value : Convert.ToInt32(txtHK.Text));
                        cmd.Parameters.Add("nam", string.IsNullOrEmpty(txtNam.Text) ? (object)DBNull.Value : Convert.ToInt32(txtNam.Text));
                        cmd.Parameters.Add("original_mamm", originalMaMM);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Updated {rows} row(s).");
                    }
                }
                btnLoadData_MM.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update for Mo Mon failed: " + ex.Message);
            }
        }

        private void btnDelete_MM_Click(object sender, EventArgs e)
        {
            string viewName = "V_MOMON_NV_PDT"; // Chỉ NV_PDT mới có quyền DELETE
            if (role != "NV_PDT")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Delete trên tab Mở Môn.");
                return;
            }

            if (dgvMoMon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.");
                return;
            }
            string mammToDelete = dgvMoMon.SelectedRows[0].Cells["MAMM"].Value?.ToString();

            if (string.IsNullOrEmpty(mammToDelete))
            {
                MessageBox.Show("Không tìm thấy mã mở môn để xóa.");
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa mở môn có mã {mammToDelete}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                using (OracleConnection connectionNew = new OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"DELETE FROM ADMIN_OLS.{viewName} WHERE MAMM = :mamm_param";
                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("mamm_param", mammToDelete);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Deleted {rows} row(s).");
                    }
                }
                btnLoadData_MM.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete for Mo Mon failed: " + ex.Message);
            }
        }

        // Các hàm CRUD cho SINHVIEN và DANGKY sẽ được bỏ qua hoặc comment
        private void btnLoadData_SV_Click(object sender, EventArgs e)
        {
            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    // VPD sẽ tự động áp dụng chính sách lọc dữ liệu
                    string sqlQuery = "SELECT MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA, TINHTRANG FROM ADMIN_OLS.SINHVIEN";
                    Oracle.ManagedDataAccess.Client.OracleDataAdapter adapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sqlQuery, connectionNew);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    dgvSinhVien.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load data for Sinh Vien failed: " + ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count > 0)
            {
                var row = dgvSinhVien.SelectedRows[0];
                txtMaSV.Text = row.Cells["MASV"].Value?.ToString();
                txtHoTenSV.Text = row.Cells["HOTEN"].Value?.ToString();
                txtPhaiSV.Text = row.Cells["PHAI"].Value?.ToString();
                // Định dạng lại ngày sinh
                DateTime ngSinh;
                if (row.Cells["NGSINH"].Value != DBNull.Value && DateTime.TryParse(row.Cells["NGSINH"].Value?.ToString(), out ngSinh))
                {
                    txtNgaySinhSV.Text = ngSinh.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtNgaySinhSV.Text = "";
                }
                txtDchiSV.Text = row.Cells["DCHI"].Value?.ToString();
                txtDtSV.Text = row.Cells["DT"].Value?.ToString();
                txtKhoaSV.Text = row.Cells["KHOA"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["TINHTRANG"].Value?.ToString();
            }
            else
            {
                // Xóa nội dung các textbox nếu không có hàng nào được chọn
                txtMaSV.Text = "";
                txtHoTenSV.Text = "";
                txtPhaiSV.Text = "";
                txtNgaySinhSV.Text = "";
                txtDchiSV.Text = "";
                txtDtSV.Text = "";
                txtKhoaSV.Text = "";
                txtTinhTrang.Text = "";
            }
        }
        private void btnInsert_SV_Click(object sender, EventArgs e)
        {
            // Chỉ NV_CTSV mới có quyền INSERT sinh viên (theo phân tích chính sách và trigger)
            if (role != "NV_CTSV")
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Thêm (Insert) trên tab Sinh viên.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHoTenSV.Text) || string.IsNullOrEmpty(txtKhoaSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã SV, Họ Tên và Khoa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = @"INSERT INTO ADMIN_OLS.SINHVIEN
                                    (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA, TINHTRANG)
                                    VALUES (:masv, :hoten, :phai, TO_DATE(:ngsinh, 'DD/MM/YYYY'), :dchi, :dt, :khoa, :tinhtrang)";
                    using (Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("masv", txtMaSV.Text);
                        cmd.Parameters.Add("hoten", txtHoTenSV.Text);
                        cmd.Parameters.Add("phai", txtPhaiSV.Text);
                        cmd.Parameters.Add("ngsinh", string.IsNullOrEmpty(txtNgaySinhSV.Text) ? (object)DBNull.Value : txtNgaySinhSV.Text); // TO_DATE sẽ xử lý định dạng
                        cmd.Parameters.Add("dchi", string.IsNullOrEmpty(txtDchiSV.Text) ? (object)DBNull.Value : txtDchiSV.Text);
                        cmd.Parameters.Add("dt", string.IsNullOrEmpty(txtDtSV.Text) ? (object)DBNull.Value : txtDtSV.Text);
                        cmd.Parameters.Add("khoa", txtKhoaSV.Text);
                        // Trigger trg_insert_sinhvien_ctsv sẽ set TINHTRANG = NULL nếu là NV_CTSV
                        // Nên gửi giá trị từ textbox, hoặc DBNull.Value nếu muốn mặc định là NULL cho các role khác không có trigger
                        cmd.Parameters.Add("tinhtrang", string.IsNullOrEmpty(txtTinhTrang.Text) ? (object)DBNull.Value : txtTinhTrang.Text);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Đã thêm {rows} dòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnLoadData_SV.PerformClick(); // Tải lại dữ liệu sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu Sinh viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Trong EmployeeForm.cs
        private void btnUpdate_SV_Click(object sender, EventArgs e)
        {
            // Kiểm tra vai trò chính xác theo database
            if (!(role == "SINHVIEN" || role == "NV_CTSV" || role == "NV_PDT"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Cập nhật (Update) trên tab Sinh viên.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string originalMaSV = dgvSinhVien.SelectedRows[0].Cells["MASV"].Value?.ToString();
            if (string.IsNullOrEmpty(originalMaSV))
            {
                MessageBox.Show("Không tìm thấy Mã Sinh viên để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql;
                    Oracle.ManagedDataAccess.Client.OracleCommand cmd;

                    // Xây dựng câu lệnh UPDATE tùy thuộc vào vai trò
                    if (role == "SINHVIEN")
                    {
                        // SINHVIEN chỉ update thông tin cá nhân của mình
                        sql = @"UPDATE ADMIN_OLS.SINHVIEN
                            SET HOTEN = :hoten, PHAI = :phai, NGSINH = TO_DATE(:ngsinh, 'DD/MM/YYYY'),
                                DCHI = :dchi, DT = :dt
                            WHERE MASV = :original_masv";
                        cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("hoten", txtHoTenSV.Text);
                        cmd.Parameters.Add("phai", txtPhaiSV.Text);
                        cmd.Parameters.Add("ngsinh", string.IsNullOrEmpty(txtNgaySinhSV.Text) ? (object)DBNull.Value : txtNgaySinhSV.Text);
                        cmd.Parameters.Add("dchi", string.IsNullOrEmpty(txtDchiSV.Text) ? (object)DBNull.Value : txtDchiSV.Text);
                        cmd.Parameters.Add("dt", string.IsNullOrEmpty(txtDtSV.Text) ? (object)DBNull.Value : txtDtSV.Text);
                        cmd.Parameters.Add("original_masv", originalMaSV);
                    }
                    else if (role == "NV_CTSV" || role == "NV_PDT")
                    {
                        // NV_CTSV và NV_PDT có thể update hầu hết các trường (trừ MASV)
                        sql = @"UPDATE ADMIN_OLS.SINHVIEN
                            SET HOTEN = :hoten, PHAI = :phai, NGSINH = TO_DATE(:ngsinh, 'DD/MM/YYYY'),
                                DCHI = :dchi, DT = :dt, KHOA = :khoa, TINHTRANG = :tinhtrang
                            WHERE MASV = :original_masv";
                        cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("hoten", txtHoTenSV.Text);
                        cmd.Parameters.Add("phai", txtPhaiSV.Text);
                        cmd.Parameters.Add("ngsinh", string.IsNullOrEmpty(txtNgaySinhSV.Text) ? (object)DBNull.Value : txtNgaySinhSV.Text);
                        cmd.Parameters.Add("dchi", string.IsNullOrEmpty(txtDchiSV.Text) ? (object)DBNull.Value : txtDchiSV.Text);
                        cmd.Parameters.Add("dt", string.IsNullOrEmpty(txtDtSV.Text) ? (object)DBNull.Value : txtDtSV.Text);
                        cmd.Parameters.Add("khoa", txtKhoaSV.Text);
                        // NV_CTSV không sửa TINHTRANG, NV_PDT có thể sửa. Để giá trị từ textbox.
                        cmd.Parameters.Add("tinhtrang", string.IsNullOrEmpty(txtTinhTrang.Text) ? (object)DBNull.Value : txtTinhTrang.Text);
                        cmd.Parameters.Add("original_masv", originalMaSV);
                    }
                    else
                    {
                        MessageBox.Show("Vai trò của bạn không được phép cập nhật dữ liệu này.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Đã cập nhật {rows} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnLoadData_SV.PerformClick(); // Tải lại dữ liệu sau khi cập nhật
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật dữ liệu Sinh viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Trong EmployeeForm.cs
        private void btnDelete_SV_Click(object sender, EventArgs e)
        {
            // Kiểm tra vai trò chính xác theo database
            if (!(role == "SINHVIEN" || role == "NV_CTSV" || role == "NV_PDT"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Xóa (Delete) trên tab Sinh viên.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string masvToDelete = dgvSinhVien.SelectedRows[0].Cells["MASV"].Value?.ToString();
            if (string.IsNullOrEmpty(masvToDelete))
            {
                MessageBox.Show("Không tìm thấy Mã Sinh viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên có mã {masvToDelete}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"DELETE FROM ADMIN_OLS.SINHVIEN WHERE MASV = :masv_param";
                    using (Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("masv_param", masvToDelete);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Đã xóa {rows} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnLoadData_SV.PerformClick(); // Tải lại dữ liệu sau khi xóa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa dữ liệu Sinh viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadData_DK_Click(object sender, EventArgs e)
        {
            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    // VPD sẽ tự động áp dụng chính sách lọc dữ liệu dựa trên vai trò và SYSDATE
                    string sqlQuery = "SELECT DK.MASV, DK.MAMM, DK.DIEMTH, DK.DIEMQT, DK.DIEMCK, DK.DIEMTK FROM ADMIN_OLS.DANGKY DK";
                    Oracle.ManagedDataAccess.Client.OracleDataAdapter adapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sqlQuery, connectionNew);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);
                    dgvDangKy.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load data for Dang Ky failed: " + ex.Message, "Lỗi tải dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDangKy_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDangKy.SelectedRows.Count > 0)
            {
                var row = dgvDangKy.SelectedRows[0];
                txtMaSVDK.Text = row.Cells["MASV"].Value?.ToString();
                txtMaMMDK.Text = row.Cells["MAMM"].Value?.ToString();
                txtDiemTH.Text = row.Cells["DIEMTH"].Value?.ToString();
                txtDiemQT.Text = row.Cells["DIEMQT"].Value?.ToString();
                txtDiemCK.Text = row.Cells["DIEMCK"].Value?.ToString();
                txtDiemTK.Text = row.Cells["DIEMTK"].Value?.ToString();
            }
            else
            {
                // Xóa nội dung các textbox nếu không có hàng nào được chọn
                txtMaSVDK.Text = "";
                txtMaMMDK.Text = "";
                txtDiemTH.Text = "";
                txtDiemQT.Text = "";
                txtDiemCK.Text = "";
                txtDiemTK.Text = "";
            }
        }

        private void btnInsert_DK_Click(object sender, EventArgs e)
        {
            // Kiểm tra vai trò chính xác theo database
            if (!(role == "SINHVIEN" || role == "NV_PDT"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Thêm (Insert) trên tab Đăng Ký.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(txtMaSVDK.Text) || string.IsNullOrEmpty(txtMaMMDK.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã Sinh viên và Mã Mở môn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = @"INSERT INTO ADMIN_OLS.DANGKY
                                    (MASV, MAMM, DIEMTH, DIEMQT, DIEMCK, DIEMTK)
                                    VALUES (:masv, :mamm, :diemth, :diemqt, :diemck, :diemtk)";
                    using (Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("masv", txtMaSVDK.Text);
                        cmd.Parameters.Add("mamm", txtMaMMDK.Text);
                        // Điểm ban đầu luôn là NULL khi đăng ký
                        cmd.Parameters.Add("diemth", DBNull.Value);
                        cmd.Parameters.Add("diemqt", DBNull.Value);
                        cmd.Parameters.Add("diemck", DBNull.Value);
                        cmd.Parameters.Add("diemtk", DBNull.Value);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Đã thêm {rows} dòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnLoadData_DK.PerformClick(); // Tải lại dữ liệu sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm dữ liệu Đăng Ký thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_DK_Click(object sender, EventArgs e)
        {
            // Kiểm tra vai trò chính xác theo database
            if (!(role == "SINHVIEN" || role == "NV_PDT" || role == "NV_PKT"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Cập nhật (Update) trên tab Đăng Ký.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDangKy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string originalMaSV = dgvDangKy.SelectedRows[0].Cells["MASV"].Value?.ToString();
            string originalMaMM = dgvDangKy.SelectedRows[0].Cells["MAMM"].Value?.ToString();

            if (string.IsNullOrEmpty(originalMaSV) || string.IsNullOrEmpty(originalMaMM))
            {
                MessageBox.Show("Không tìm thấy Mã Sinh viên hoặc Mã Mở môn của dòng đã chọn. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql;
                    Oracle.ManagedDataAccess.Client.OracleCommand cmd;

                    if (role == "SINHVIEN")
                    {
                        // SV chỉ được cập nhật MAMM của mình nếu các cột điểm là NULL
                        // Chính sách vpd_sv_dangky_update sẽ kiểm tra điều kiện thời gian và điểm NULL
                        sql = $@"UPDATE ADMIN_OLS.DANGKY
                                SET MAMM = :new_mamm
                                WHERE MASV = :original_masv AND MAMM = :original_mamm";
                        cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("new_mamm", txtMaMMDK.Text); // Sinh viên có thể đổi MAMM
                        cmd.Parameters.Add("original_masv", originalMaSV);
                        cmd.Parameters.Add("original_mamm", originalMaMM);
                    }
                    else if (role == "NV_PDT") // NV PĐT
                    {
                        // NV PĐT có thể cập nhật các trường thông tin đăng ký (không phải điểm)
                        // Chính sách vpd_pdt_dangky_update sẽ kiểm tra điều kiện thời gian và điểm NULL
                        sql = $@"UPDATE ADMIN_OLS.DANGKY
                                SET MAMM = :new_mamm
                                WHERE MASV = :original_masv AND MAMM = :original_mamm";
                        cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("new_mamm", txtMaMMDK.Text);
                        cmd.Parameters.Add("original_masv", originalMaSV);
                        cmd.Parameters.Add("original_mamm", originalMaMM);
                    }
                    else if (role == "NV_PKT") // NV PKT
                    {
                        // NV PKT chỉ được phép cập nhật các cột điểm
                        // Chính sách vpd_pkt_dangky đã được đặt là 1=1 cho UPDATE, nhưng sec_relevant_cols giới hạn cột
                        sql = $@"UPDATE ADMIN_OLS.DANGKY
                                SET DIEMTH = :diemth, DIEMQT = :diemqt, DIEMCK = :diemck, DIEMTK = :diemtk
                                WHERE MASV = :original_masv AND MAMM = :original_mamm";
                        cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew);
                        cmd.Parameters.Add("diemth", string.IsNullOrEmpty(txtDiemTH.Text) ? (object)DBNull.Value : (object)Convert.ToDecimal(txtDiemTH.Text));
                        cmd.Parameters.Add("diemqt", string.IsNullOrEmpty(txtDiemQT.Text) ? (object)DBNull.Value : (object)Convert.ToDecimal(txtDiemQT.Text));
                        cmd.Parameters.Add("diemck", string.IsNullOrEmpty(txtDiemCK.Text) ? (object)DBNull.Value : (object)Convert.ToDecimal(txtDiemCK.Text));
                        cmd.Parameters.Add("diemtk", string.IsNullOrEmpty(txtDiemTK.Text) ? (object)DBNull.Value : (object)Convert.ToDecimal(txtDiemTK.Text));
                        cmd.Parameters.Add("original_masv", originalMaSV);
                        cmd.Parameters.Add("original_mamm", originalMaMM);
                    }
                    else
                    {
                        MessageBox.Show("Vai trò của bạn không được phép cập nhật dữ liệu này.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Đã cập nhật {rows} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnLoadData_DK.PerformClick(); // Tải lại dữ liệu sau khi cập nhật
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật dữ liệu Đăng Ký thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_DK_Click(object sender, EventArgs e)
        {
            // Kiểm tra vai trò chính xác theo database
            if (!(role == "SINHVIEN" || role == "NV_PDT"))
            {
                MessageBox.Show("Bạn không có quyền thực hiện thao tác Xóa (Delete) trên tab Đăng Ký.", "Lỗi quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDangKy.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string masvToDelete = dgvDangKy.SelectedRows[0].Cells["MASV"].Value?.ToString();
            string mammToDelete = dgvDangKy.SelectedRows[0].Cells["MAMM"].Value?.ToString();

            if (string.IsNullOrEmpty(masvToDelete) || string.IsNullOrEmpty(mammToDelete))
            {
                MessageBox.Show("Không tìm thấy Mã Sinh viên hoặc Mã Mở môn để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn hủy đăng ký môn {mammToDelete} của sinh viên {masvToDelete}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.No)
            {
                return;
            }

            try
            {
                using (Oracle.ManagedDataAccess.Client.OracleConnection connectionNew = new Oracle.ManagedDataAccess.Client.OracleConnection(connString))
                {
                    connectionNew.Open();
                    string sql = $@"DELETE FROM ADMIN_OLS.DANGKY WHERE MASV = :masv_param AND MAMM = :mamm_param";
                    using (Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand(sql, connectionNew))
                    {
                        cmd.Parameters.Add("masv_param", masvToDelete);
                        cmd.Parameters.Add("mamm_param", mammToDelete);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show($"Đã xóa {rows} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                btnLoadData_DK.PerformClick(); // Tải lại dữ liệu sau khi xóa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xóa dữ liệu Đăng Ký thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            try
            {
                string adminConnStr = "User Id=ADMIN_OLS;Password=123;Data Source=localhost:1521/ORCL21PDB1;";
                string password = null;

                using (OracleConnection adminConn = new OracleConnection(adminConnStr))
                {
                    adminConn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT PASSWORD FROM USER_ROLES WHERE USERNAME = :username", adminConn);
                    cmd.Parameters.Add("username", username);
                    object result = cmd.ExecuteScalar();
                    password = result?.ToString();
                }

                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Không thể xác thực mật khẩu người dùng.");
                    return;
                }

                string userConnStr = $"User Id={username};Password={password};Data Source=localhost:1521/ORCL21PDB1;";
                using (OracleConnection userConn = new OracleConnection(userConnStr))
                {
                    userConn.Open();

                    Form f = new Form();
                    f.Text = "Thông báo dành cho bạn";
                    f.Size = new Size(600, 400);
                    f.StartPosition = FormStartPosition.CenterParent;

                    DataGridView dgv = new DataGridView
                    {
                        Dock = DockStyle.Fill,
                        ReadOnly = true,
                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                    };
                    f.Controls.Add(dgv);

                    OracleDataAdapter adapter = new OracleDataAdapter("SELECT NOIDUNG FROM ADMIN_OLS.THONGBAO", userConn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;

                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông báo: " + ex.Message);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
            this.Close();
        }
    }
}