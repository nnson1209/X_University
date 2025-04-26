using System.Windows.Forms;
using System.Drawing; // Needed for Point, Size, Padding, etc.

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

        // Add containers for the new layout
        private Panel pnlLeftMenu; // Container for the buttons
        private FlowLayoutPanel flpButtons; // Auto-arranges buttons vertically


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
            // Instantiate the new containers
            this.pnlLeftMenu = new Panel();
            this.flpButtons = new FlowLayoutPanel();

            // Instantiate existing controls
            this.pnlMain = new Panel(); // Keep the main panel
            this.btnViewUsers = new Button();
            this.btnViewRoles = new Button();
            this.btnCreateUser = new Button();
            this.btnEditUser = new Button();
            this.btnDeleteUser = new Button();
            this.btnCreateRole = new Button();
            this.btnEditRole = new Button();
            this.btnDeleteRole = new Button();

            // --- Layout Setup ---

            // pnlLeftMenu: Panel on the left for buttons
            this.pnlLeftMenu.Controls.Add(this.flpButtons); // Add the flow panel to the left panel
            this.pnlLeftMenu.Dock = DockStyle.Left;       // Dock it to the left side of the form
            this.pnlLeftMenu.Width = 180;                 // Set a fixed width (or adjust as needed)
            this.pnlLeftMenu.Padding = new Padding(10);   // Add padding around the edges

            // flpButtons: FlowLayoutPanel inside pnlLeftMenu to arrange buttons vertically
            this.flpButtons.FlowDirection = FlowDirection.TopDown; // Arrange controls from top to bottom
            this.flpButtons.Dock = DockStyle.Fill;                // Make it fill the pnlLeftMenu (within padding)
            this.flpButtons.AutoSize = true;                      // Auto-size the flow panel based on contents
            this.flpButtons.WrapContents = false;                 // Don't wrap buttons to the next column/row

            // Add buttons to the FlowLayoutPanel (removing them from the form's Controls later)
            this.flpButtons.Controls.Add(this.btnViewUsers);
            this.flpButtons.Controls.Add(this.btnViewRoles);
            this.flpButtons.Controls.Add(this.btnCreateUser);
            this.flpButtons.Controls.Add(this.btnEditUser);
            this.flpButtons.Controls.Add(this.btnDeleteUser);
            this.flpButtons.Controls.Add(this.btnCreateRole);
            this.flpButtons.Controls.Add(this.btnEditRole);
            this.flpButtons.Controls.Add(this.btnDeleteRole);

            // Configure Button Appearance and Spacing (Apply to all buttons)
            Size buttonSize = new Size(150, 30); // Consistent size for all buttons
            Padding buttonMargin = new Padding(0, 0, 0, 10); // Add space below each button

            this.btnViewUsers.Size = buttonSize;
            this.btnViewUsers.Margin = buttonMargin; // Margin within FlowLayoutPanel provides spacing
            this.btnViewUsers.Text = "View Users";
            this.btnViewUsers.Click += new System.EventHandler(this.btnViewUsers_Click);

            this.btnViewRoles.Size = buttonSize;
            this.btnViewRoles.Margin = buttonMargin;
            this.btnViewRoles.Text = "View Roles";
            this.btnViewRoles.Click += new System.EventHandler(this.btnViewRoles_Click);

            this.btnCreateUser.Size = buttonSize;
            this.btnCreateUser.Margin = buttonMargin;
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);

            this.btnEditUser.Size = buttonSize;
            this.btnEditUser.Margin = buttonMargin;
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);

            this.btnDeleteUser.Size = buttonSize;
            this.btnDeleteUser.Margin = buttonMargin;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);

            this.btnCreateRole.Size = buttonSize;
            this.btnCreateRole.Margin = buttonMargin;
            this.btnCreateRole.Text = "Create Role";
            this.btnCreateRole.Click += new System.EventHandler(this.btnCreateRole_Click);

            this.btnEditRole.Size = buttonSize;
            this.btnEditRole.Margin = buttonMargin;
            this.btnEditRole.Text = "Edit Role";
            this.btnEditRole.Click += new System.EventHandler(this.btnEditRole_Click);

            this.btnDeleteRole.Size = buttonSize;
            // Use a different margin for the last button if you don't want space below it
            this.btnDeleteRole.Margin = new Padding(0); // No bottom margin for the last button
            this.btnDeleteRole.Text = "Delete Role";
            this.btnDeleteRole.Click += new System.EventHandler(this.btnDeleteRole_Click);

            // pnlMain: Panel on the right for content
            // Remove hardcoded Location and Size - Dock will handle this
            this.pnlMain.Dock = DockStyle.Fill; // Make it fill the remaining space
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle; // Keep the border

            // --- Form Setup ---

            this.ClientSize = new Size(800, 450); // Adjusted default size
            this.Controls.Add(this.pnlMain);      // Add pnlMain first so it fills the rest
            this.Controls.Add(this.pnlLeftMenu); // Add the left panel (docked left)

            this.Text = "User and Role Management";
            // Ensure AutoScaleMode is set appropriately for DPI scaling if needed
            // this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ResumeLayout(false);
        }
    }
}