using System.Windows.Forms;

namespace XUniversity.Forms
{
    partial class GrantRevokePrivilegesForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private Label lblGranteeType, lblGrantee, lblPrivilege, lblObjectType, lblObjectName, lblColumn;
        private RadioButton rdoUser, rdoRole;
        private ComboBox cmbGrantee, cmbPrivilege, cmbObjectType, cmbObjectName, cmbColumn;
        private CheckBox chkWithGrantOption;
        private Button btnGrant, btnRevoke, btnViewPrivileges;
        private DataGridView dgvPrivileges;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ---------- Labels ----------
            lblGranteeType = new Label
            {
                Text = "Grantee Type:",
                Location = new System.Drawing.Point(30, 25),
                AutoSize = true
            };

            lblGrantee = new Label
            {
                Text = "Grantee:",
                Location = new System.Drawing.Point(30, 65),
                AutoSize = true
            };

            lblPrivilege = new Label
            {
                Text = "Privilege:",
                Location = new System.Drawing.Point(30, 105),
                AutoSize = true
            };

            lblObjectType = new Label
            {
                Text = "Object Type:",
                Location = new System.Drawing.Point(30, 145),
                AutoSize = true
            };

            lblObjectName = new Label
            {
                Text = "Object Name:",
                Location = new System.Drawing.Point(30, 185),
                AutoSize = true
            };

            lblColumn = new Label
            {
                Text = "Column (for SELECT/UPDATE):",
                Location = new System.Drawing.Point(30, 225),
                AutoSize = true
            };

            // ---------- Radio Buttons ----------
            rdoUser = new RadioButton
            {
                Text = "User",
                Location = new System.Drawing.Point(150, 23),
                AutoSize = true
            };
            rdoUser.CheckedChanged += new System.EventHandler(this.rdoUser_CheckedChanged);

            rdoRole = new RadioButton
            {
                Text = "Role",
                Location = new System.Drawing.Point(220, 23),
                AutoSize = true
            };
            rdoRole.CheckedChanged += new System.EventHandler(this.rdoRole_CheckedChanged);

            // ---------- ComboBoxes ----------
            cmbGrantee = new ComboBox
            {
                Location = new System.Drawing.Point(150, 62),
                Size = new System.Drawing.Size(300, 24)
            };

            cmbPrivilege = new ComboBox
            {
                Location = new System.Drawing.Point(150, 102),
                Size = new System.Drawing.Size(300, 24)
            };

            cmbObjectType = new ComboBox
            {
                Location = new System.Drawing.Point(150, 142),
                Size = new System.Drawing.Size(300, 24)
            };
            cmbObjectType.SelectedIndexChanged += new System.EventHandler(this.cmbObjectType_SelectedIndexChanged);

            cmbObjectName = new ComboBox
            {
                Location = new System.Drawing.Point(150, 182),
                Size = new System.Drawing.Size(300, 24)
            };
            cmbObjectName.SelectedIndexChanged += new System.EventHandler(this.cmbObjectName_SelectedIndexChanged);

            cmbColumn = new ComboBox
            {
                Location = new System.Drawing.Point(240, 222),
                Size = new System.Drawing.Size(210, 24)
            };

            // ---------- CheckBox ----------
            chkWithGrantOption = new CheckBox
            {
                Text = "WITH GRANT OPTION",
                Location = new System.Drawing.Point(150, 260),
                AutoSize = true
            };

            // ---------- Buttons ----------
            btnGrant = new Button
            {
                Text = "Grant",
                Location = new System.Drawing.Point(80, 300),
                Size = new System.Drawing.Size(100, 30)
            };
            btnGrant.Click += new System.EventHandler(this.btnGrant_Click);

            btnRevoke = new Button
            {
                Text = "Revoke",
                Location = new System.Drawing.Point(200, 300),
                Size = new System.Drawing.Size(100, 30)
            };
            btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);

            btnViewPrivileges = new Button
            {
                Text = "View Privileges",
                Location = new System.Drawing.Point(320, 300),
                Size = new System.Drawing.Size(130, 30)
            };
            btnViewPrivileges.Click += new System.EventHandler(this.btnViewPrivileges_Click);

            // ---------- DataGridView ----------
            dgvPrivileges = new DataGridView
            {
                Location = new System.Drawing.Point(30, 350),
                Size = new System.Drawing.Size(620, 250),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            // ---------- Form Settings ----------
            this.ClientSize = new System.Drawing.Size(700, 630);
            this.Text = "Grant/Revoke Privileges – Oracle Admin";
            this.Controls.AddRange(new Control[]
            {
                lblGranteeType, rdoUser, rdoRole,
                lblGrantee, cmbGrantee,
                lblPrivilege, cmbPrivilege,
                lblObjectType, cmbObjectType,
                lblObjectName, cmbObjectName,
                lblColumn, cmbColumn,
                chkWithGrantOption,
                btnGrant, btnRevoke, btnViewPrivileges,
                dgvPrivileges
            });

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivileges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
