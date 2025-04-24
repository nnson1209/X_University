using System.Windows.Forms;
using System;
using OracleAdminTool.DAL;
using System.Configuration;

namespace XUniversity
{
    partial class MainForm
    {
        private Panel panelSidebar;
        private Panel panelMain;
        private Button btnManageUserRole, btnGrant, btnRevoke, btnView;
        private Button btnTestConnection;
        private Button btnViewData;
        private DataGridView dgvPreview;

        private void BuildInitializeComponent()
        {
            panelSidebar = new Panel { Dock = DockStyle.Left, Width = 180 };
            panelMain = new Panel { Dock = DockStyle.Fill };
            btnRevoke = new Button { Text = "Privileges Management", Dock = DockStyle.Top };
            btnManageUserRole = new Button { Text = "User and Role Management", Dock = DockStyle.Top };
            panelSidebar.Controls.AddRange(new Control[] { btnRevoke, btnManageUserRole });
            Controls.AddRange(new Control[] { panelMain, panelSidebar });
            Text = "XUniversity Admin";
            WindowState = FormWindowState.Maximized;
            btnManageUserRole.Click += (s, e) => LoadForm(new Forms.ManageUserRoleForm());
            btnRevoke.Click += (s, e) => LoadForm(new Forms.GrantRevokePrivilegesForm());

            btnTestConnection = new Button { Text = "Test Connection", Dock = DockStyle.Top };
            panelSidebar.Controls.Add(btnTestConnection);
            btnTestConnection.Click += new EventHandler(this.btnTestConnection_Click);

            btnViewData = new Button
            {
                Text = "View Data",
                Dock = DockStyle.Top
            };
            dgvPreview = new DataGridView { Dock = DockStyle.Bottom, Height = 250 };

            panelSidebar.Controls.Add(btnViewData);
            Controls.Add(dgvPreview);

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