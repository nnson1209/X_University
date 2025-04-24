using System.Windows.Forms;

namespace XUniversity.Forms
{
    partial class ManageUserRoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlMain;
        private Button btnViewUsers;
        private Button btnViewRoles;
        private Button btnCreateUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Button btnCreateRole;
        private Button btnEditRole;
        private Button btnDeleteRole;

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
            this.pnlMain = new Panel();
            this.btnViewUsers = new Button();
            this.btnViewRoles = new Button();
            this.btnCreateUser = new Button();
            this.btnEditUser = new Button();
            this.btnDeleteUser = new Button();
            this.btnCreateRole = new Button();
            this.btnEditRole = new Button();
            this.btnDeleteRole = new Button();
            this.SuspendLayout();

            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(180, 10);
            this.pnlMain.Size = new System.Drawing.Size(600, 380);
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle;

            // 
            // btnViewUsers
            // 
            this.btnViewUsers.Location = new System.Drawing.Point(10, 10);
            this.btnViewUsers.Size = new System.Drawing.Size(150, 30);
            this.btnViewUsers.Text = "View Users";
            this.btnViewUsers.Click += new System.EventHandler(this.btnViewUsers_Click);

            // 
            // btnViewRoles
            // 
            this.btnViewRoles.Location = new System.Drawing.Point(10, 50);
            this.btnViewRoles.Size = new System.Drawing.Size(150, 30);
            this.btnViewRoles.Text = "View Roles";
            this.btnViewRoles.Click += new System.EventHandler(this.btnViewRoles_Click);

            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(10, 90);
            this.btnCreateUser.Size = new System.Drawing.Size(150, 30);
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);

            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(10, 130);
            this.btnEditUser.Size = new System.Drawing.Size(150, 30);
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);

            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(10, 170);
            this.btnDeleteUser.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            // 
            // btnCreateRole
            // 
            this.btnCreateRole.Location = new System.Drawing.Point(10, 210);
            this.btnCreateRole.Size = new System.Drawing.Size(150, 30);
            this.btnCreateRole.Text = "Create Role";
            this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);

            // 
            // btnEditRole
            // 
            this.btnEditRole.Location = new System.Drawing.Point(10, 250);
            this.btnEditRole.Size = new System.Drawing.Size(150, 30);
            this.btnEditRole.Text = "Edit Role";
            this.btnEditRole.Click += new System.EventHandler(this.btnEditRole_Click);

            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Location = new System.Drawing.Point(10, 290);
            this.btnDeleteRole.Size = new System.Drawing.Size(150, 30);
            this.btnDeleteRole.Text = "Delete Role";
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);

            // 
            // ManageUserRoleForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnViewUsers);
            this.Controls.Add(this.btnViewRoles);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.btnCreateRole);
            this.Controls.Add(this.btnEditRole);
            this.Controls.Add(this.btnDeleteRole);
            this.Text = "User and Role Management";
            this.ResumeLayout(false);
        }
    }
}
