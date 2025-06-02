
using System.Drawing;
using System.Windows.Forms;

namespace XUniversity.Forms
{
    partial class EmployeeForm
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabControlMain;
        private TabPage tabNhanVien, tabMoMon, tabSinhVien, tabDangKy;
        private Label lblWelcome;

        // NHANVIEN controls
        private DataGridView dgvNhanVien;
        private TextBox txtMaNV, txtHoTen, txtPhai, txtNgaySinh, txtSoDT, txtLuong, txtPhuCap, txtVaiTro, txtMaDV;
        private Button btnLoadData_NV, btnInsert_NV, btnUpdate_NV, btnDelete_NV;
        private Label[] lblFields_NV;

        // MOMON controls
        private DataGridView dgvMoMon;
        private TextBox txtMaMM, txtMaHP, txtMaGV, txtHK, txtNam;
        private Button btnLoadData_MM, btnInsert_MM, btnUpdate_MM, btnDelete_MM;

        // SINHVIEN controls
        private DataGridView dgvSinhVien;
        private TextBox txtMaSV, txtHoTenSV, txtPhaiSV, txtNgaySinhSV, txtDchiSV, txtDtSV, txtKhoaSV, txtTinhTrang;
        private Button btnLoadData_SV, btnInsert_SV, btnUpdate_SV, btnDelete_SV;

        // DANGKY controls
        private DataGridView dgvDangKy;
        private TextBox txtMaSVDK, txtMaMMDK, txtDiemTH, txtDiemQT, txtDiemCK, txtDiemTK;
        private Button btnLoadData_DK, btnInsert_DK, btnUpdate_DK, btnDelete_DK;

        // THONGBAO
        private Button btnThongBao;

        private Button btnLogout;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblWelcome = new Label();
            this.tabControlMain = new TabControl();
            this.tabNhanVien = new TabPage("NHANVIEN");
            this.tabMoMon = new TabPage("MOMON");
            this.tabSinhVien = new TabPage("SINHVIEN");
            this.tabDangKy = new TabPage("DANGKY");

            this.Text = "Employee Dashboard";
            this.Size = new System.Drawing.Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(10, 10);
            this.lblWelcome.Size = new System.Drawing.Size(300, 20);
            this.lblWelcome.Text = "Welcome";

            btnThongBao = new Button();
            btnThongBao.Text = "Thông báo";
            btnThongBao.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnThongBao.Size = new Size(100, 30);

            btnThongBao.Location = new Point(this.Width - 220, 10);
            btnThongBao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThongBao.Click += btnThongBao_Click;
            this.Controls.Add(btnThongBao);

            btnLogout = new Button();
            btnLogout.Text = "Đăng xuất";
            btnLogout.Font = new Font("Segoe UI", 9F);
            btnLogout.Size = new Size(100, 30);

            btnLogout.Location = new Point(this.Width - 110, 10);
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Click += BtnLogout_Click;
            this.Controls.Add(btnLogout);

            this.tabControlMain.Location = new System.Drawing.Point(10, 40);
            this.tabControlMain.Size = new System.Drawing.Size(960, 600);

            this.tabControlMain.Controls.AddRange(new Control[] {
                this.tabNhanVien, this.tabMoMon, this.tabSinhVien, this.tabDangKy
            });

            InitNhanVienTab();
            InitMoMonTab();
            InitSinhVienTab();
            InitDangKyTab();

            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.tabControlMain);
        }

        private void InitNhanVienTab()
        {
            this.dgvNhanVien = new DataGridView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(920, 260),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            this.tabNhanVien.Controls.Add(this.dgvNhanVien);

            string[] fields = { "MANLD", "HOTEN", "PHAI", "NGSINH", "DT", "LUONG", "PHUCAP", "VAITRO", "MADV" };
            lblFields_NV = new Label[fields.Length];

            TextBox[] txts = {
                txtMaNV = new TextBox(), txtHoTen = new TextBox(), txtPhai = new TextBox(), txtNgaySinh = new TextBox(),
                txtSoDT = new TextBox(), txtLuong = new TextBox(), txtPhuCap = new TextBox(),
                txtVaiTro = new TextBox(), txtMaDV = new TextBox()
            };

            for (int i = 0; i < fields.Length; i++)
            {
                lblFields_NV[i] = new Label
                {
                    Text = fields[i],
                    Location = new System.Drawing.Point(10 + (i % 3) * 300, 280 + (i / 3) * 40),
                    Size = new System.Drawing.Size(80, 20)
                };

                txts[i].Location = new System.Drawing.Point(90 + (i % 3) * 300, 280 + (i / 3) * 40);
                txts[i].Size = new System.Drawing.Size(180, 23);

                this.tabNhanVien.Controls.Add(lblFields_NV[i]);
                this.tabNhanVien.Controls.Add(txts[i]);
            }

            btnLoadData_NV = new Button { Text = "Load", Location = new System.Drawing.Point(10, 480), Size = new System.Drawing.Size(80, 30) };
            btnInsert_NV = new Button { Text = "Insert", Location = new System.Drawing.Point(100, 480), Size = new System.Drawing.Size(80, 30) };
            btnUpdate_NV = new Button { Text = "Update", Location = new System.Drawing.Point(190, 480), Size = new System.Drawing.Size(80, 30) };
            btnDelete_NV = new Button { Text = "Delete", Location = new System.Drawing.Point(280, 480), Size = new System.Drawing.Size(80, 30) };

            btnLoadData_NV.Click += new System.EventHandler(this.btnLoadData_NV_Click);
            btnInsert_NV.Click += new System.EventHandler(this.btnInsert_NV_Click);
            btnUpdate_NV.Click += new System.EventHandler(this.btnUpdate_NV_Click);
            btnDelete_NV.Click += new System.EventHandler(this.btnDelete_NV_Click);

            this.tabNhanVien.Controls.AddRange(new Control[] {
                btnLoadData_NV, btnInsert_NV, btnUpdate_NV, btnDelete_NV
            });
        }

        private void InitMoMonTab()
        {
            this.dgvMoMon = new DataGridView
            {
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(920, 260),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.dgvMoMon.SelectionChanged += new System.EventHandler(this.dgvMoMon_SelectionChanged);
            this.tabMoMon.Controls.Add(this.dgvMoMon);

            string[] fields = { "MAMM", "MAHP", "MAGV", "HOCKY", "NAM" };
            Label[] labels = new Label[fields.Length];

            TextBox[] txts = {
                txtMaMM = new TextBox(), txtMaHP = new TextBox(), txtMaGV = new TextBox(),
                txtHK = new TextBox(), txtNam = new TextBox()
            };

            for (int i = 0; i < fields.Length; i++)
            {
                labels[i] = new Label
                {
                    Text = fields[i],
                    Location = new System.Drawing.Point(10 + (i % 3) * 300, 280 + (i / 3) * 40),
                    Size = new System.Drawing.Size(80, 20)
                };

                txts[i].Location = new System.Drawing.Point(90 + (i % 3) * 300, 280 + (i / 3) * 40);
                txts[i].Size = new System.Drawing.Size(180, 23);

                this.tabMoMon.Controls.Add(labels[i]);
                this.tabMoMon.Controls.Add(txts[i]);
            }

            btnLoadData_MM = new Button { Text = "Load", Location = new System.Drawing.Point(10, 400), Size = new System.Drawing.Size(80, 30) };
            btnInsert_MM = new Button { Text = "Insert", Location = new System.Drawing.Point(100, 400), Size = new System.Drawing.Size(80, 30) };
            btnUpdate_MM = new Button { Text = "Update", Location = new System.Drawing.Point(190, 400), Size = new System.Drawing.Size(80, 30) };
            btnDelete_MM = new Button { Text = "Delete", Location = new System.Drawing.Point(280, 400), Size = new System.Drawing.Size(80, 30) };

            btnLoadData_MM.Click += new System.EventHandler(this.btnLoadData_MM_Click);
            btnInsert_MM.Click += new System.EventHandler(this.btnInsert_MM_Click);
            btnUpdate_MM.Click += new System.EventHandler(this.btnUpdate_MM_Click);
            btnDelete_MM.Click += new System.EventHandler(this.btnDelete_MM_Click);

            this.tabMoMon.Controls.AddRange(new Control[] {
                btnLoadData_MM, btnInsert_MM, btnUpdate_MM, btnDelete_MM
            });
        }

        private void InitSinhVienTab()
        {
            this.dgvSinhVien = new DataGridView { Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(920, 260), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            this.dgvSinhVien.SelectionChanged += new System.EventHandler(this.dgvSinhVien_SelectionChanged);
            this.tabSinhVien.Controls.Add(this.dgvSinhVien);

            string[] fields = { "MASV", "HOTEN", "PHAI", "NGSINH", "DCHI", "DT", "KHOA", "TINHTRANG" };
            TextBox[] txts = {
                txtMaSV = new TextBox(), txtHoTenSV = new TextBox(), txtPhaiSV = new TextBox(), txtNgaySinhSV = new TextBox(),
                txtDchiSV = new TextBox(), txtDtSV = new TextBox(), txtKhoaSV = new TextBox(), txtTinhTrang = new TextBox()
            };

            for (int i = 0; i < fields.Length; i++)
            {
                Label lbl = new Label { Text = fields[i], Location = new System.Drawing.Point(10 + (i % 3) * 300, 280 + (i / 3) * 40), Size = new System.Drawing.Size(100, 20) };
                txts[i].Location = new System.Drawing.Point(110 + (i % 3) * 300, 280 + (i / 3) * 40);
                txts[i].Size = new System.Drawing.Size(180, 23);
                this.tabSinhVien.Controls.Add(lbl);
                this.tabSinhVien.Controls.Add(txts[i]);
            }

            btnLoadData_SV = new Button { Text = "Load", Location = new System.Drawing.Point(10, 480), Size = new System.Drawing.Size(80, 30) };
            btnInsert_SV = new Button { Text = "Insert", Location = new System.Drawing.Point(100, 480), Size = new System.Drawing.Size(80, 30) };
            btnUpdate_SV = new Button { Text = "Update", Location = new System.Drawing.Point(190, 480), Size = new System.Drawing.Size(80, 30) };
            btnDelete_SV = new Button { Text = "Delete", Location = new System.Drawing.Point(280, 480), Size = new System.Drawing.Size(80, 30) };

            btnLoadData_SV.Click += new System.EventHandler(this.btnLoadData_SV_Click);
            btnInsert_SV.Click += new System.EventHandler(this.btnInsert_SV_Click);
            btnUpdate_SV.Click += new System.EventHandler(this.btnUpdate_SV_Click);
            btnDelete_SV.Click += new System.EventHandler(this.btnDelete_SV_Click);

            this.tabSinhVien.Controls.AddRange(new Control[] { btnLoadData_SV, btnInsert_SV, btnUpdate_SV, btnDelete_SV });
        }

        private void InitDangKyTab()
        {
            this.dgvDangKy = new DataGridView { Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(920, 260), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            this.dgvDangKy.SelectionChanged += new System.EventHandler(this.dgvDangKy_SelectionChanged);
            this.tabDangKy.Controls.Add(this.dgvDangKy);

            string[] fields = { "MASV", "MAMM", "DIEMTH", "DIEMQT", "DIEMCK", "DIEMTK" };
            TextBox[] txts = {
                txtMaSVDK = new TextBox(), txtMaMMDK = new TextBox(), txtDiemTH = new TextBox(),
                txtDiemQT = new TextBox(), txtDiemCK = new TextBox(), txtDiemTK = new TextBox()
            };

            for (int i = 0; i < fields.Length; i++)
            {
                Label lbl = new Label { Text = fields[i], Location = new System.Drawing.Point(10 + (i % 3) * 300, 280 + (i / 3) * 40), Size = new System.Drawing.Size(100, 20) };
                txts[i].Location = new System.Drawing.Point(110 + (i % 3) * 300, 280 + (i / 3) * 40);
                txts[i].Size = new System.Drawing.Size(180, 23);
                this.tabDangKy.Controls.Add(lbl);
                this.tabDangKy.Controls.Add(txts[i]);
            }

            btnLoadData_DK = new Button { Text = "Load", Location = new System.Drawing.Point(10, 440), Size = new System.Drawing.Size(80, 30) };
            btnInsert_DK = new Button { Text = "Insert", Location = new System.Drawing.Point(100, 440), Size = new System.Drawing.Size(80, 30) };
            btnUpdate_DK = new Button { Text = "Update", Location = new System.Drawing.Point(190, 440), Size = new System.Drawing.Size(80, 30) };
            btnDelete_DK = new Button { Text = "Delete", Location = new System.Drawing.Point(280, 440), Size = new System.Drawing.Size(80, 30) };

            btnLoadData_DK.Click += new System.EventHandler(this.btnLoadData_DK_Click);
            btnInsert_DK.Click += new System.EventHandler(this.btnInsert_DK_Click);
            btnUpdate_DK.Click += new System.EventHandler(this.btnUpdate_DK_Click);
            btnDelete_DK.Click += new System.EventHandler(this.btnDelete_DK_Click);

            this.tabDangKy.Controls.AddRange(new Control[] { btnLoadData_DK, btnInsert_DK, btnUpdate_DK, btnDelete_DK });
        }
    }
}
