using System.Windows.Forms;
using System;
using OracleAdminTool.DAL;
using System.Configuration;
using System.Drawing;

namespace XUniversity
{
    partial class MainForm
    {
        private Panel panelSidebar;
        private Panel panelMain;
        private Panel panelHeader;
        private Label lblTitle;
        private Button btnManageUserRole, btnGrant, btnRevoke, btnView;
        private Button btnTestConnection;
        private Button btnViewData;
        private DataGridView dgvPreview;

        private void BuildInitializeComponent()
        {
            // Header
            panelHeader = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(30, 60, 120) };
            lblTitle = new Label
            {
                Text = "XUniversity Oracle Admin",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            panelHeader.Controls.Add(lblTitle);

            // Sidebar
            panelSidebar = new Panel { Dock = DockStyle.Left, Width = 220, BackColor = Color.FromArgb(245, 245, 245) };

            btnManageUserRole = new Button
            {
                Text = "Quản lý User/Role",
                Dock = DockStyle.Top,
                Height = 55,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 230, 241),
                FlatStyle = FlatStyle.Flat
            };
            btnRevoke = new Button
            {
                Text = "Quản lý Quyền",
                Dock = DockStyle.Top,
                Height = 55,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(220, 230, 241),
                FlatStyle = FlatStyle.Flat
            };
            btnTestConnection = new Button
            {
                Text = "Kiểm tra Kết nối",
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(240, 240, 240),
                FlatStyle = FlatStyle.Flat
            };
            btnViewData = new Button
            {
                Text = "Xem Dữ liệu",
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(240, 240, 240),
                FlatStyle = FlatStyle.Flat
            };

            // Thêm khoảng cách giữa các nút
            panelSidebar.Padding = new Padding(0, 20, 0, 0);

            // Thứ tự add: từ dưới lên trên
            panelSidebar.Controls.Add(btnViewData);
            panelSidebar.Controls.Add(btnTestConnection);
            panelSidebar.Controls.Add(btnRevoke);
            panelSidebar.Controls.Add(btnManageUserRole);

            // Main panel
            panelMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10), BackColor = Color.WhiteSmoke };

            // DataGridView preview (ẩn mặc định)
            dgvPreview = new DataGridView
            {
                Dock = DockStyle.Bottom,
                Height = 220,
                Visible = false,
                BackgroundColor = Color.White
            };

            // Add controls
            Controls.Add(panelMain);
            Controls.Add(panelSidebar);
            Controls.Add(panelHeader);
            Controls.Add(dgvPreview);

            // Form settings
            Text = "XUniversity Admin";
            WindowState = FormWindowState.Maximized;
            Font = new Font("Segoe UI", 10);

            // Sự kiện
            btnManageUserRole.Click += (s, e) => LoadForm(new Forms.ManageUserRoleForm());
            btnRevoke.Click += (s, e) => LoadForm(new Forms.GrantRevokePrivilegesForm());
            btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);
            btnViewData.Click += new EventHandler(this.btnViewData_Click);
        }

        private void LoadForm(Form f)
        {
            panelMain.Controls.Clear();
            f.TopLevel = false; f.Dock = DockStyle.Fill;
            panelMain.Controls.Add(f); f.Show();
        }
    }
}