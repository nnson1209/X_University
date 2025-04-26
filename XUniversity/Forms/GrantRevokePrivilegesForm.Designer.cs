using System.Windows.Forms;
using System.Drawing;

namespace XUniversity.Forms
{
    partial class GrantRevokePrivilegesForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private TableLayoutPanel tblGrantRevoke;
        private FlowLayoutPanel pnlButtons;
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
            lblGranteeType = new Label { Text = "Loại đối tượng:", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };
            lblGrantee = new Label { Text = "Tên đối tượng:", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };
            lblPrivilege = new Label { Text = "Quyền:", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };
            lblObjectType = new Label { Text = "Loại đối tượng CSDL:", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };
            lblObjectName = new Label { Text = "Tên đối tượng CSDL:", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };
            lblColumn = new Label { Text = "Cột (nếu có):", AutoSize = true, Anchor = AnchorStyles.Right, Font = new Font("Segoe UI", 10) };

            // ---------- Radio Buttons ----------
            rdoUser = new RadioButton { Text = "User", AutoSize = true, Font = new Font("Segoe UI", 10) };
            rdoRole = new RadioButton { Text = "Role", AutoSize = true, Font = new Font("Segoe UI", 10) };
            rdoUser.CheckedChanged += new System.EventHandler(this.rdoUser_CheckedChanged);
            rdoRole.CheckedChanged += new System.EventHandler(this.rdoRole_CheckedChanged);

            // ---------- ComboBoxes ----------
            cmbGrantee = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbPrivilege = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbObjectType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbObjectType.SelectedIndexChanged += new System.EventHandler(this.cmbObjectType_SelectedIndexChanged);
            cmbObjectName = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };
            cmbObjectName.SelectedIndexChanged += new System.EventHandler(this.cmbObjectName_SelectedIndexChanged);
            cmbColumn = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            // ---------- CheckBox ----------
            chkWithGrantOption = new CheckBox { Text = "WITH GRANT OPTION", AutoSize = true, Font = new Font("Segoe UI", 10) };

            // ---------- Buttons ----------
            btnGrant = new Button { Text = "Cấp quyền", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 241, 220), FlatStyle = FlatStyle.Flat };
            btnRevoke = new Button { Text = "Thu hồi quyền", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(241, 220, 220), FlatStyle = FlatStyle.Flat };
            btnViewPrivileges = new Button { Text = "Xem quyền", Width = 120, Height = 36, Font = new Font("Segoe UI", 10, FontStyle.Bold), BackColor = Color.FromArgb(220, 230, 241), FlatStyle = FlatStyle.Flat };
            btnGrant.Click += new System.EventHandler(this.btnGrant_Click);
            btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);
            btnViewPrivileges.Click += new System.EventHandler(this.btnViewPrivileges_Click);

            // ---------- TableLayoutPanel ----------
            tblGrantRevoke = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 6,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(660, 180),
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            tblGrantRevoke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblGrantRevoke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblGrantRevoke.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            for (int i = 0; i < 6; i++)
                tblGrantRevoke.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));

            // Add controls to table
            tblGrantRevoke.Controls.Add(lblGranteeType, 0, 0);
            tblGrantRevoke.Controls.Add(rdoUser, 1, 0);
            tblGrantRevoke.Controls.Add(rdoRole, 2, 0);

            tblGrantRevoke.Controls.Add(lblGrantee, 0, 1);
            tblGrantRevoke.Controls.Add(cmbGrantee, 1, 1);

            tblGrantRevoke.Controls.Add(lblPrivilege, 0, 2);
            tblGrantRevoke.Controls.Add(cmbPrivilege, 1, 2);

            tblGrantRevoke.Controls.Add(lblObjectType, 0, 3);
            tblGrantRevoke.Controls.Add(cmbObjectType, 1, 3);

            tblGrantRevoke.Controls.Add(lblObjectName, 0, 4);
            tblGrantRevoke.Controls.Add(cmbObjectName, 1, 4);

            tblGrantRevoke.Controls.Add(lblColumn, 0, 5);
            tblGrantRevoke.Controls.Add(cmbColumn, 1, 5);
            tblGrantRevoke.Controls.Add(chkWithGrantOption, 2, 5);

            // ---------- FlowLayoutPanel cho nút ----------
            pnlButtons = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(10, 8, 0, 8),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            pnlButtons.Controls.AddRange(new Control[] { btnGrant, btnRevoke, btnViewPrivileges });

            // ---------- DataGridView ----------
            dgvPrivileges = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White
            };

            // ---------- Form Settings ----------
            this.ClientSize = new System.Drawing.Size(700, 630);
            this.Text = "Quản lý Cấp/Thu hồi Quyền – Oracle Admin";
            this.Font = new Font("Segoe UI", 10);
            this.Controls.Add(dgvPrivileges);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(tblGrantRevoke);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivileges)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
