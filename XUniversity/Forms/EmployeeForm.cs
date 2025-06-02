//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Oracle.ManagedDataAccess.Client;

//namespace XUniversity.Forms
//{
//    public partial class EmployeeForm : Form
//    {
//        private OracleConnection conn;
//        private string username;
//        private string role;
//        private string viewName;
//        private string connString;

//        public EmployeeForm(OracleConnection connection, string user, string userRole)
//        {
//            conn = connection;
//            connString = connection.ConnectionString;
//            username = user;
//            role = userRole;

//            InitializeComponent();
//            lblWelcome.Text = $"Xin chào {username} ({role})!";

//            switch (role)
//            {
//                case "TRGDV":
//                    viewName = "V_NHANVIEN_TRGDV";
//                    break;
//                case "NV_TCHC":
//                    viewName = "V_NHANVIEN_TCHC";
//                    break;
//                case "GV":
//                case "NV_PDT":
//                case "NV_PKT":
//                case "NV_CTSV":
//                case "NVCB":
//                    viewName = "V_NHANVIEN_NVCB";
//                    break;
//                case "SINHVIEN":
//                    break;
//                default:
//                    MessageBox.Show("Role không được hỗ trợ.");
//                    this.Close();
//                    return;
//            }
//            SetPermissionByRole();
//        }

//        private void SetPermissionByRole()
//        {
//            // Mặc định: tất cả đều không được sửa gì
//            txtHoTen.ReadOnly = true;
//            txtLuong.ReadOnly = true;
//            txtPhuCap.ReadOnly = true;
//            txtVaiTro.ReadOnly = true;
//            txtMaDV.ReadOnly = true;
//            txtNgaySinh.ReadOnly = true;
//            txtPhai.ReadOnly = true;
//            txtSoDT.ReadOnly = true;

//            btnInsert_NV.Enabled = false;
//            btnUpdate_NV.Enabled = true;
//            btnDelete_NV.Enabled = false;

//            // Quyền FULL của NV_TCHC
//            if (role == "NV_TCHC")
//            {
//                txtHoTen.ReadOnly = false;
//                txtLuong.ReadOnly = false;
//                txtPhuCap.ReadOnly = false;
//                txtVaiTro.ReadOnly = false;
//                txtMaDV.ReadOnly = false;
//                txtNgaySinh.ReadOnly = false;
//                txtPhai.ReadOnly = false;
//                txtSoDT.ReadOnly = false;

//                btnInsert_NV.Enabled = true;
//                btnUpdate_NV.Enabled = true;
//                btnDelete_NV.Enabled = true;
//            }
//            // Quyền TRGDV: chỉ xem các nhân viên cùng MADV, trừ lương và phụ cấp
//            else if (role == "TRGDV")
//            {
//                txtHoTen.ReadOnly = true;
//                txtLuong.ReadOnly = true;
//                txtPhuCap.ReadOnly = true;
//                txtVaiTro.ReadOnly = true;
//                txtMaDV.ReadOnly = true;
//                txtNgaySinh.ReadOnly = true;
//                txtPhai.ReadOnly = true;
//                txtSoDT.ReadOnly = true;
//            }
//            else if (role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB")
//            {
//                txtSoDT.ReadOnly = false;
//            }
//            else
//            {
//                btnLoadData_NV.Enabled = false;
//                btnInsert_NV.Enabled = false;
//                btnUpdate_NV.Enabled = false;
//                btnDelete_NV.Enabled = false;
//            }
//        }


//        private void BtnLogout_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//            new LoginForm().ShowDialog();
//            this.Close();
//        }

//        private void btnLoadData_NV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();

//                    string sqlQuery = $"SELECT * FROM ADMIN_OLS.{viewName}";

//                    OracleDataAdapter adapter = new OracleDataAdapter(sqlQuery, connectionNew);
//                    DataTable dt = new DataTable();
//                    adapter.Fill(dt);
//                    dgvNhanVien.DataSource = dt;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Load failed: " + ex.Message);
//            }
//        }

//        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
//        {
//            if (dgvNhanVien.SelectedRows.Count > 0)
//            {
//                var row = dgvNhanVien.SelectedRows[0];
//                txtMaNV.Text = row.Cells["MANLD"].Value?.ToString();
//                txtHoTen.Text = row.Cells["HOTEN"].Value?.ToString();
//                txtPhai.Text = row.Cells["PHAI"].Value?.ToString();
//                txtNgaySinh.Text = Convert.ToDateTime(row.Cells["NGSINH"].Value).ToString("yyyy-MM-dd");
//                txtSoDT.Text = row.Cells["DT"].Value?.ToString();
//                txtLuong.Text = dgvNhanVien.Columns.Contains("LUONG") ? row.Cells["LUONG"].Value?.ToString() : "";
//                txtPhuCap.Text = dgvNhanVien.Columns.Contains("PHUCAP") ? row.Cells["PHUCAP"].Value?.ToString() : "";
//                txtVaiTro.Text = row.Cells["VAITRO"].Value?.ToString();
//                txtMaDV.Text = row.Cells["MADV"].Value?.ToString();
//            }
//        }

//        private void btnInsert_NV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();

//                    string sql = $@"INSERT INTO ADMIN_OLS.{viewName}
//                                (MANLD, HOTEN, PHAI, NGSINH, DT, LUONG, PHUCAP, VAITRO, MADV)
//                                VALUES (:manld, :hoten, :phai, TO_DATE(:ngsinh, 'YYYY-MM-DD'), :dt, :luong, :phucap, :vaitro, :madv)";

//                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
//                    {
//                        cmd.Parameters.Add("manld", txtMaNV.Text);
//                        cmd.Parameters.Add("hoten", txtHoTen.Text);
//                        cmd.Parameters.Add("phai", txtPhai.Text);
//                        cmd.Parameters.Add("ngsinh", txtNgaySinh.Text);
//                        cmd.Parameters.Add("dt", txtSoDT.Text);
//                        cmd.Parameters.Add("luong", string.IsNullOrEmpty(txtLuong.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtLuong.Text));
//                        cmd.Parameters.Add("phucap", string.IsNullOrEmpty(txtPhuCap.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPhuCap.Text));
//                        cmd.Parameters.Add("vaitro", txtVaiTro.Text);
//                        cmd.Parameters.Add("madv", txtMaDV.Text);

//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Inserted {rows} row(s).");
//                    }
//                }

//                btnLoadData_NV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Insert failed: " + ex.Message);
//            }
//        }

//        private void btnUpdate_NV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();

//                    string sql = "";
//                    using (OracleCommand cmd = new OracleCommand())
//                    {
//                        cmd.Connection = connectionNew;

//                        if (role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB")
//                        {
//                            // Chỉ được update số điện thoại
//                            sql = $"UPDATE ADMIN_OLS.{viewName} SET DT = :dt WHERE MANLD = :manld";
//                            cmd.CommandText = sql;
//                            cmd.Parameters.Add("dt", txtSoDT.Text);
//                            cmd.Parameters.Add("manld", txtMaNV.Text);
//                        }
//                        else if (role == "NV_TCHC") // Được update toàn bộ
//                        {
//                            sql = @"UPDATE ADMIN_OLS.V_NHANVIEN_TCHC SET
//                                    HOTEN=:hoten, PHAI=:phai, NGSINH=TO_DATE(:ngsinh, 'YYYY-MM-DD'), DT=:dt,
//                                    LUONG=:luong, PHUCAP=:phucap, VAITRO=:vaitro, MADV=:madv WHERE MANLD=:manld";
//                            cmd.CommandText = sql;
//                            cmd.Parameters.Add("hoten", txtHoTen.Text);
//                            cmd.Parameters.Add("phai", txtPhai.Text);
//                            cmd.Parameters.Add("ngsinh", txtNgaySinh.Text);
//                            cmd.Parameters.Add("dt", txtSoDT.Text);
//                            cmd.Parameters.Add("luong", string.IsNullOrEmpty(txtLuong.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtLuong.Text));
//                            cmd.Parameters.Add("phucap", string.IsNullOrEmpty(txtPhuCap.Text) ? (object)DBNull.Value : Convert.ToDecimal(txtPhuCap.Text));
//                            cmd.Parameters.Add("vaitro", txtVaiTro.Text);
//                            cmd.Parameters.Add("madv", txtMaDV.Text);
//                            cmd.Parameters.Add("manld", txtMaNV.Text);
//                        }
//                        else if (role == "TRGDV")
//                        {
//                            MessageBox.Show("Bạn không có quyền cập nhật thông tin.");
//                            return;
//                        }
//                        else
//                        {
//                            MessageBox.Show("Bạn không có quyền cập nhật thông tin.");
//                            return;
//                        }

//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Updated {rows} row(s).");
//                    }
//                }

//                btnLoadData_NV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Update failed: " + ex.Message);
//            }
//        }

//        private void btnDelete_NV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                //DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
//                //if (dialogResult == DialogResult.No)
//                //{
//                //    return;
//                //}

//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();

//                    string sql = $"DELETE FROM ADMIN_OLS.{viewName} WHERE MANLD = :manld";
//                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
//                    {
//                        cmd.Parameters.Add("manld", txtMaNV.Text);

//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Deleted {rows} row(s).");
//                    }
//                }

//                btnLoadData_NV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Delete failed: " + ex.Message);
//            }
//        }

//        // Tab MOMON
//        private void btnLoadData_MM_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();
//                    string sqlQuery = $"SELECT * FROM ADMIN_OLS.{viewName}";
//                    OracleDataAdapter adapter = new OracleDataAdapter(sqlQuery, connectionNew);
//                    DataTable dt = new DataTable();
//                    adapter.Fill(dt);
//                    dgvMoMon.DataSource = dt;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Load failed: " + ex.Message);
//            }
//        }

//        private void dgvMoMon_SelectionChanged(object sender, EventArgs e)
//        {
//            if (dgvMoMon.SelectedRows.Count > 0)
//            {
//                var row = dgvMoMon.SelectedRows[0];
//                txtMaMM.Text = row.Cells["MAMM"].Value?.ToString();
//                txtMaHP.Text = row.Cells["MAHP"].Value?.ToString();
//                txtMaGV.Text = row.Cells["MAGV"].Value?.ToString();
//                txtHK.Text = row.Cells["HK"].Value?.ToString();
//                txtNam.Text = row.Cells["NAM"].Value?.ToString();
//            }
//        }

//        private void btnInsert_MM_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();
//                    string sql = $@"INSERT INTO ADMIN_OLS.{viewName}
//                            (MAMM, MAHP, MAGV, HK, NAM)
//                            VALUES (:mamm, :mahp, :magv, :hk, :nam)";
//                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
//                    {
//                        cmd.Parameters.Add("mamm", txtMaMM.Text);
//                        cmd.Parameters.Add("mahp", txtMaHP.Text);
//                        cmd.Parameters.Add("magv", txtMaGV.Text);
//                        cmd.Parameters.Add("hk", Convert.ToInt32(txtHK.Text));
//                        cmd.Parameters.Add("nam", Convert.ToInt32(txtNam.Text));

//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Inserted {rows} row(s).");
//                    }
//                }
//                btnLoadData_MM.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Insert failed: " + ex.Message);
//            }
//        }

//        private void btnUpdate_MM_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();
//                    string sql = $@"UPDATE ADMIN_OLS.{viewName}
//                                SET MAGV = :magv
//                                WHERE MAMM = :mamm";

//                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
//                    {
//                        cmd.Parameters.Add("mamm", txtMaMM.Text);
//                        cmd.Parameters.Add("mahp", txtMaHP.Text);
//                        cmd.Parameters.Add("magv", txtMaGV.Text);
//                        cmd.Parameters.Add("hk", Convert.ToInt32(txtHK.Text));
//                        cmd.Parameters.Add("nam", Convert.ToInt32(txtNam.Text));

//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Updated {rows} row(s).");
//                    }
//                }
//                btnLoadData_MM.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Update failed: " + ex.Message);
//            }
//        }

//        private void btnDelete_MM_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                using (OracleConnection connectionNew = new OracleConnection(connString))
//                {
//                    connectionNew.Open();
//                    string sql = $@"DELETE FROM ADMIN_OLS.{viewName} WHERE MAMM = :mamm";
//                    using (OracleCommand cmd = new OracleCommand(sql, connectionNew))
//                    {
//                        cmd.Parameters.Add("mahp", txtMaMM.Text);
//                        int rows = cmd.ExecuteNonQuery();
//                        MessageBox.Show($"Deleted {rows} row(s).");
//                    }
//                }
//                btnLoadData_MM.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Delete failed: " + ex.Message);
//            }
//        }

//        // 2. Tab SINHVIEN
//        private void btnLoadData_SV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM SINHVIEN", conn);
//                DataTable dt = new DataTable();
//                adapter.Fill(dt);
//                dgvSinhVien.DataSource = dt;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Load failed: " + ex.Message);
//            }
//        }

//        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
//        {
//            if (dgvSinhVien.SelectedRows.Count > 0)
//            {
//                var row = dgvSinhVien.SelectedRows[0];
//                txtMaSV.Text = row.Cells["MASV"].Value?.ToString();
//                txtHoTenSV.Text = row.Cells["HOTEN"].Value?.ToString();
//                txtPhaiSV.Text = row.Cells["PHAI"].Value?.ToString();
//                txtNgaySinhSV.Text = Convert.ToDateTime(row.Cells["NGSINH"].Value).ToString("yyyy-MM-dd");
//                txtDchiSV.Text = row.Cells["DCHI"].Value?.ToString();
//                txtDtSV.Text = row.Cells["DT"].Value?.ToString();
//                txtKhoaSV.Text = row.Cells["KHOA"].Value?.ToString();
//                txtTinhTrang.Text = row.Cells["TINHTRANG"].Value?.ToString();
//            }
//        }

//        private void btnInsert_SV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = @"INSERT INTO SINHVIEN (MASV, HOTEN, PHAI, NGSINH, DCHI, DT, KHOA, TINHTRANG) 
//                        VALUES (:masv, :hoten, :phai, TO_DATE(:ngsinh, 'YYYY-MM-DD'), :dchi, :dt, :khoa, :tinhtrang)";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("masv", txtMaSV.Text);
//                cmd.Parameters.Add("hoten", txtHoTenSV.Text);
//                cmd.Parameters.Add("phai", txtPhaiSV.Text);
//                cmd.Parameters.Add("ngsinh", txtNgaySinhSV.Text);
//                cmd.Parameters.Add("dchi", txtDchiSV.Text);
//                cmd.Parameters.Add("dt", txtDtSV.Text);
//                cmd.Parameters.Add("khoa", txtKhoaSV.Text);
//                cmd.Parameters.Add("tinhtrang", txtTinhTrang.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Inserted {rows} row(s).");
//                btnLoadData_SV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Insert failed: " + ex.Message);
//            }
//        }

//        private void btnUpdate_SV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = @"UPDATE SINHVIEN SET HOTEN = :hoten, PHAI = :phai, NGSINH = TO_DATE(:ngsinh, 'YYYY-MM-DD'),
//DCHI = :dchi, DT = :dt, KHOA = :khoa, TINHTRANG = :tinhtrang WHERE MASV = :masv";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("hoten", txtHoTenSV.Text);
//                cmd.Parameters.Add("phai", txtPhaiSV.Text);
//                cmd.Parameters.Add("ngsinh", txtNgaySinhSV.Text);
//                cmd.Parameters.Add("dchi", txtDchiSV.Text);
//                cmd.Parameters.Add("dt", txtDtSV.Text);
//                cmd.Parameters.Add("khoa", txtKhoaSV.Text);
//                cmd.Parameters.Add("tinhtrang", txtTinhTrang.Text);
//                cmd.Parameters.Add("masv", txtMaSV.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Updated {rows} row(s).");
//                btnLoadData_SV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Update failed: " + ex.Message);
//            }
//        }

//        private void btnDelete_SV_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = "DELETE FROM SINHVIEN WHERE MASV = :masv";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("masv", txtMaSV.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Deleted {rows} row(s).");
//                btnLoadData_SV.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Delete failed: " + ex.Message);
//            }
//        }

//        // 3. Tab DANGKY
//        private void btnLoadData_DK_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM DANGKY", conn);
//                DataTable dt = new DataTable();
//                adapter.Fill(dt);
//                dgvDangKy.DataSource = dt;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Load failed: " + ex.Message);
//            }
//        }

//        private void dgvDangKy_SelectionChanged(object sender, EventArgs e)
//        {
//            if (dgvDangKy.SelectedRows.Count > 0)
//            {
//                var row = dgvDangKy.SelectedRows[0];
//                txtMaSVDK.Text = row.Cells["MASV"].Value?.ToString();
//                txtMaMMDK.Text = row.Cells["MAMM"].Value?.ToString();
//                txtDiemTH.Text = row.Cells["DIEMTH"].Value?.ToString();
//                txtDiemQT.Text = row.Cells["DIEMQT"].Value?.ToString();
//                txtDiemCK.Text = row.Cells["DIEMCK"].Value?.ToString();
//                txtDiemTK.Text = row.Cells["DIEMTK"].Value?.ToString();
//            }
//        }

//        private void btnInsert_DK_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = @"INSERT INTO DANGKY (MASV, MAMM, DIEMTH, DIEMQT, DIEMCK, DIEMTK) 
//                        VALUES (:masv, :mamm, :diemth, :diemqt, :diemck, :diemtk)";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("masv", txtMaSVDK.Text);
//                cmd.Parameters.Add("mamm", txtMaMMDK.Text);
//                cmd.Parameters.Add("diemth", txtDiemTH.Text);
//                cmd.Parameters.Add("diemqt", txtDiemQT.Text);
//                cmd.Parameters.Add("diemck", txtDiemCK.Text);
//                cmd.Parameters.Add("diemtk", txtDiemTK.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Inserted {rows} row(s).");
//                btnLoadData_DK.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Insert failed: " + ex.Message);
//            }
//        }

//        private void btnUpdate_DK_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = @"UPDATE DANGKY SET DIEMTH = :diemth, DIEMQT = :diemqt, DIEMCK = :diemck, DIEMTK = :diemtk
//                        WHERE MASV = :masv AND MAMM = :mamm";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("diemth", txtDiemTH.Text);
//                cmd.Parameters.Add("diemqt", txtDiemQT.Text);
//                cmd.Parameters.Add("diemck", txtDiemCK.Text);
//                cmd.Parameters.Add("diemtk", txtDiemTK.Text);
//                cmd.Parameters.Add("masv", txtMaSVDK.Text);
//                cmd.Parameters.Add("mamm", txtMaMMDK.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Updated {rows} row(s).");
//                btnLoadData_DK.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Update failed: " + ex.Message);
//            }
//        }

//        private void btnDelete_DK_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string sql = "DELETE FROM DANGKY WHERE MASV = :masv AND MAMM = :mamm";
//                OracleCommand cmd = new OracleCommand(sql, conn);
//                cmd.Parameters.Add("masv", txtMaSVDK.Text);
//                cmd.Parameters.Add("mamm", txtMaMMDK.Text);

//                int rows = cmd.ExecuteNonQuery();
//                MessageBox.Show($"Deleted {rows} row(s).");
//                btnLoadData_DK.PerformClick();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Delete failed: " + ex.Message);
//            }
//        }

//        private void btnThongBao_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string adminConnStr = "User Id=ADMIN_OLS;Password=123;Data Source=localhost:1521/ORCL21PDB1;";
//                string password = null;

//                using (OracleConnection adminConn = new OracleConnection(adminConnStr))
//                {
//                    adminConn.Open();
//                    OracleCommand cmd = new OracleCommand("SELECT PASSWORD FROM USER_ROLES WHERE USERNAME = :username", adminConn);
//                    cmd.Parameters.Add("username", username);
//                    object result = cmd.ExecuteScalar();
//                    password = result?.ToString();
//                }

//                if (string.IsNullOrEmpty(password))
//                {
//                    MessageBox.Show("Không thể xác thực mật khẩu người dùng.");
//                    return;
//                }

//                string userConnStr = $"User Id={username};Password={password};Data Source=localhost:1521/ORCL21PDB1;";
//                using (OracleConnection userConn = new OracleConnection(userConnStr))
//                {
//                    userConn.Open();

//                    Form f = new Form();
//                    f.Text = "Thông báo dành cho bạn";
//                    f.Size = new Size(600, 400);
//                    f.StartPosition = FormStartPosition.CenterParent;

//                    DataGridView dgv = new DataGridView
//                    {
//                        Dock = DockStyle.Fill,
//                        ReadOnly = true,
//                        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
//                    };
//                    f.Controls.Add(dgv);

//                    OracleDataAdapter adapter = new OracleDataAdapter("SELECT NOIDUNG FROM ADMIN_OLS.THONGBAO", userConn);
//                    DataTable dt = new DataTable();
//                    adapter.Fill(dt);
//                    dgv.DataSource = dt;

//                    f.ShowDialog();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Lỗi khi tải thông báo: " + ex.Message);
//            }
//        }





//    }
//}


using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

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
            // else if (tabControlMain.SelectedTab == tabSinhVien)
            // {
            //     // Logic cho tab SINHVIEN (nếu có, bạn có thể uncomment và thêm vào sau)
            //     // SetPermissionByRole_SinhVien();
            //     // btnLoadData_SV.PerformClick();
            // }
            // else if (tabControlMain.SelectedTab == tabDangKy)
            // {
            //     // Logic cho tab DANGKY (nếu có, bạn có thể uncomment và thêm vào sau)
            //     // SetPermissionByRole_DangKy();
            //     // btnLoadData_DK.PerformClick();
            // }
            // else
            // {
            //     MessageBox.Show("Bạn đang ở tab: " + tabControlMain.SelectedTab.Text);
            // }
        }

        private void SetPermissionByRole_NhanVien()
        {
            //txtMaNV.ReadOnly = true;
            //txtHoTen.ReadOnly = true;
            //txtPhai.ReadOnly = true;
            //txtNgaySinh.ReadOnly = true;
            //txtSoDT.ReadOnly = true;
            //txtLuong.ReadOnly = true;
            //txtPhuCap.ReadOnly = true;
            //txtVaiTro.ReadOnly = true;
            //txtMaDV.ReadOnly = true;

            //btnLoadData_NV.Enabled = true;
            //btnInsert_NV.Enabled = false;
            //btnUpdate_NV.Enabled = false;
            //btnDelete_NV.Enabled = false;

            //if (role == "NV_TCHC")
            //{
            //    txtMaNV.ReadOnly = false;
            //    txtHoTen.ReadOnly = false;
            //    txtPhai.ReadOnly = false;
            //    txtNgaySinh.ReadOnly = false;
            //    txtSoDT.ReadOnly = false;
            //    txtLuong.ReadOnly = false;
            //    txtPhuCap.ReadOnly = false;
            //    txtVaiTro.ReadOnly = false;
            //    txtMaDV.ReadOnly = false;

            //    btnInsert_NV.Enabled = true;
            //    btnUpdate_NV.Enabled = true;
            //    btnDelete_NV.Enabled = true;
            //}
            //else if (role == "TRGDV" || role == "GV" || role == "NV_PDT" || role == "NV_PKT" || role == "NV_CTSV" || role == "NVCB")
            //{
            //    txtSoDT.ReadOnly = false;
            //    btnUpdate_NV.Enabled = true;
            //}
            //else
            //{
            //    btnLoadData_NV.Enabled = false;
            //}
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

        /*
        // Các hàm SetPermissionByRole_SinhVien và SetPermissionByRole_DangKy sẽ được bỏ qua hoặc comment theo yêu cầu
        private void SetPermissionByRole_SinhVien() { // ... }
        private void SetPermissionByRole_DangKy() { // ... }
        */

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
        private void btnLoadData_SV_Click(object sender, EventArgs e) { }
        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e) { }
        private void btnInsert_SV_Click(object sender, EventArgs e) { }
        private void btnUpdate_SV_Click(object sender, EventArgs e) { }
        private void btnDelete_SV_Click(object sender, EventArgs e) { }

        private void btnLoadData_DK_Click(object sender, EventArgs e) { }
        private void dgvDangKy_SelectionChanged(object sender, EventArgs e) { }
        private void btnInsert_DK_Click(object sender, EventArgs e) { }
        private void btnUpdate_DK_Click(object sender, EventArgs e) { }
        private void btnDelete_DK_Click(object sender, EventArgs e) { }

        private void btnThongBao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thông báo sẽ được phát triển sau!");
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
            this.Close();
        }
    }
}