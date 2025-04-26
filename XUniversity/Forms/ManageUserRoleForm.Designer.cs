using System.Windows.Forms;
using System.Drawing;

namespace XUniversity.Forms
{
    partial class ManageUserRoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage tabUsers, tabRoles;
        private DataGridView dgvUsers, dgvRoles;
        private FlowLayoutPanel pnlUserButtons, pnlRoleButtons;
        private Button btnViewUsers, btnCreateUser, btnEditUser, btnDeleteUser;
        private Button btnViewRoles, btnCreateRole, btnEditRole, btnDeleteRole;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabUsers = new TabPage("Quản lý User");
            this.tabRoles = new TabPage("Quản lý Role");

            // DataGridView cho User
            this.dgvUsers = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            // DataGridView cho Role
            this.dgvRoles = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };

            // Các nút cho User
            this.btnViewUsers = new Button { Text = "Xem User", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 230, 241), FlatStyle = FlatStyle.Flat };
            this.btnCreateUser = new Button { Text = "Tạo User", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 241, 220), FlatStyle = FlatStyle.Flat };
            this.btnEditUser = new Button { Text = "Sửa User", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(241, 241, 220), FlatStyle = FlatStyle.Flat };
            this.btnDeleteUser = new Button { Text = "Xóa User", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(241, 220, 220), FlatStyle = FlatStyle.Flat };

            // Các nút cho Role
            this.btnViewRoles = new Button { Text = "Xem Role", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 230, 241), FlatStyle = FlatStyle.Flat };
            this.btnCreateRole = new Button { Text = "Tạo Role", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 241, 220), FlatStyle = FlatStyle.Flat };
            this.btnEditRole = new Button { Text = "Sửa Role", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(241, 241, 220), FlatStyle = FlatStyle.Flat };
            this.btnDeleteRole = new Button { Text = "Xóa Role", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(241, 220, 220), FlatStyle = FlatStyle.Flat };

            // Panel nút cho User
            this.pnlUserButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 8, 0, 8),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            this.pnlUserButtons.Controls.AddRange(new Control[] { btnViewUsers, btnCreateUser, btnEditUser, btnDeleteUser });

            // Panel nút cho Role
            this.pnlRoleButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10, 8, 0, 8),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            this.pnlRoleButtons.Controls.AddRange(new Control[] { btnViewRoles, btnCreateRole, btnEditRole, btnDeleteRole });

            // Tab User
            this.tabUsers.Controls.Add(dgvUsers);
            this.tabUsers.Controls.Add(pnlUserButtons);

            // Tab Role
            this.tabRoles.Controls.Add(dgvRoles);
            this.tabRoles.Controls.Add(pnlRoleButtons);

            // TabControl
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.tabControl.TabPages.Add(tabUsers);
            this.tabControl.TabPages.Add(tabRoles);

            // Form settings
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.tabControl);
            this.Text = "Quản lý User và Role";
            this.Font = new Font("Segoe UI", 10);

            // Sự kiện
            this.btnViewUsers.Click += new System.EventHandler(this.btnViewUsers_Click);
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            this.btnViewRoles.Click += new System.EventHandler(this.btnViewRoles_Click);
            this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);
            this.btnEditRole.Click += new System.EventHandler(this.btnEditRole_Click);
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);

            this.ResumeLayout(false);
        }
    }
}
