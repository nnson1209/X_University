using Oracle.ManagedDataAccess.Client;
using XUniversity.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace XUniversity
{
    public partial class MainForm : Form
    {
        private MenuStrip menuStrip;
        private ToolStripMenuItem menuQuanLy;
        private ToolStripMenuItem menuNhanVien;
        private ToolStripMenuItem menuSinhVien;
        private ToolStripMenuItem menuHocPhan;
        private ToolStripMenuItem menuMoMon;
        private ToolStripMenuItem menuDangKy;
        private DataGridView dgvData;
        private Button btnRefresh;
        private Label lblUserInfo;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            LoadUserPermissions();
        }

        private void InitializeCustomComponents()
        {
            // Menu Strip
            menuStrip = new MenuStrip();
            menuQuanLy = new ToolStripMenuItem("Quản lý");
            menuNhanVien = new ToolStripMenuItem("Nhân viên");
            menuSinhVien = new ToolStripMenuItem("Sinh viên");
            menuHocPhan = new ToolStripMenuItem("Học phần");
            menuMoMon = new ToolStripMenuItem("Mở môn");
            menuDangKy = new ToolStripMenuItem("Đăng ký");

            menuQuanLy.DropDownItems.AddRange(new ToolStripItem[] {
                menuNhanVien,
                menuSinhVien,
                menuHocPhan,
                menuMoMon,
                menuDangKy
            });

            menuStrip.Items.Add(menuQuanLy);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // DataGridView
            dgvData = new DataGridView();
            dgvData.Dock = DockStyle.Fill;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Controls.Add(dgvData);

            // Refresh Button
            btnRefresh = new Button();
            btnRefresh.Text = "Làm mới";
            btnRefresh.Location = new System.Drawing.Point(10, 30);
            btnRefresh.Click += BtnRefresh_Click;
            this.Controls.Add(btnRefresh);

            // User Info Label
            lblUserInfo = new Label();
            lblUserInfo.AutoSize = true;
            lblUserInfo.Location = new System.Drawing.Point(10, 60);
            this.Controls.Add(lblUserInfo);

            // Menu Click Events
            menuNhanVien.Click += (s, e) => LoadData("NHANVIEN");
            menuSinhVien.Click += (s, e) => LoadData("SINHVIEN");
            menuHocPhan.Click += (s, e) => LoadData("HOCPHAN");
            menuMoMon.Click += (s, e) => LoadData("MOMON");
            menuDangKy.Click += (s, e) => LoadData("DANGKY");

            // Form Closing Event
            this.FormClosing += MainForm_FormClosing;
        }

        private void LoadUserPermissions()
        {
            if (SessionManager.CurrentUser != null)
            {
                lblUserInfo.Text = $"Người dùng: {SessionManager.CurrentUser.Username} - Vai trò: {SessionManager.CurrentUser.Role}";
                
                // Ẩn/hiện menu dựa trên role
                switch (SessionManager.CurrentUser.Role)
                {
                    case "TRGDV":
                        // Trưởng đơn vị có quyền xem nhân viên trong đơn vị
                        menuSinhVien.Visible = false;
                        menuHocPhan.Visible = false;
                        menuMoMon.Visible = false;
                        menuDangKy.Visible = false;
                        break;
                    case "GV":
                        // Giảng viên có quyền xem môn học và đăng ký
                        menuNhanVien.Visible = false;
                        menuSinhVien.Visible = false;
                        menuHocPhan.Visible = false;
                        break;
                    case "NV PĐT":
                        // Nhân viên phòng đào tạo có quyền xem đăng ký
                        menuNhanVien.Visible = false;
                        menuSinhVien.Visible = false;
                        menuHocPhan.Visible = false;
                        menuMoMon.Visible = false;
                        break;
                    case "SV":
                        // Sinh viên chỉ xem được đăng ký của mình
                        menuQuanLy.Visible = false;
                        break;
                    default:
                        // Các role khác chỉ xem được dữ liệu
                        menuQuanLy.Visible = false;
                        break;
                }
            }
        }

        private void LoadData(string tableName)
        {
            try
            {
                string query = $"SELECT * FROM X_ADMIN.{tableName}";
                var dt = DatabaseHelper.ExecuteTable(query);
                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (dgvData.DataSource != null)
            {
                string tableName = ((DataTable)dgvData.DataSource).TableName;
                LoadData(tableName);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
