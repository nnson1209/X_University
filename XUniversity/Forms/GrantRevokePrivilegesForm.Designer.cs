using System.Windows.Forms;
using System.Drawing; // Needed for Point, Size, Padding, etc.

namespace XUniversity.Forms
{
    partial class GrantRevokePrivilegesForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblGranteeType, lblGrantee, lblPrivilege, lblObjectType, lblObjectName, lblColumn;
        private RadioButton rdoUser, rdoRole;
        private ComboBox cmbGrantee, cmbPrivilege, cmbObjectType, cmbObjectName, cmbColumn;
        private CheckBox chkWithGrantOption;
        private Button btnGrant, btnRevoke, btnViewPrivileges;
        private DataGridView dgvPrivileges;

        // Add Layout Containers
        private TableLayoutPanel tlpMain; 
        private TableLayoutPanel tlpInput;
        private FlowLayoutPanel flpButtons; 
        private Panel pnlGranteeType; // Panel to hold RadioButtons horizontally

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // --- Initialize Layout Containers ---
            tlpMain = new TableLayoutPanel();
            tlpInput = new TableLayoutPanel();
            flpButtons = new FlowLayoutPanel();
            pnlGranteeType = new Panel(); 

            lblGranteeType = new Label();
            lblGrantee = new Label();
            lblPrivilege = new Label();
            lblObjectType = new Label();
            lblObjectName = new Label();
            lblColumn = new Label();

            rdoUser = new RadioButton();
            rdoRole = new RadioButton();

            cmbGrantee = new ComboBox();
            cmbPrivilege = new ComboBox();
            cmbObjectType = new ComboBox();
            cmbObjectName = new ComboBox();
            cmbColumn = new ComboBox();

            chkWithGrantOption = new CheckBox();

            btnGrant = new Button();
            btnRevoke = new Button();
            btnViewPrivileges = new Button();

            dgvPrivileges = new DataGridView();


            // --- Layout Setup ---

            // tlpMain: Main layout panel for the whole form area (excluding borders/title bar)
            tlpMain.Dock = DockStyle.Fill; // Make it fill the entire form client area
            tlpMain.ColumnCount = 1; // Single column
            // Define rows: AutoSize for input, AutoSize for buttons, Percent for DataGridView
            tlpMain.RowCount = 3;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Row 0: Input area
            tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // Row 1: Buttons
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Row 2: DataGridView (takes remaining space)
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Column 0: Takes full width
            tlpMain.Padding = new Padding(10); // Padding around the content inside tlpMain

            // tlpInput: Panel for labels and input controls (will go into tlpMain row 0)
            tlpInput.Dock = DockStyle.Top; // Dock to top within its cell in tlpMain
            tlpInput.AutoSize = true;
            tlpInput.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpInput.ColumnCount = 2; // Column 0 for labels, Column 1 for controls
            // Define columns: AutoSize for labels, Percent for controls (takes rest)
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpInput.Padding = new Padding(0, 0, 0, 10); // Add some space below the input area

            // pnlGranteeType: Panel for RadioButtons (will go into tlpInput column 1, row 0)
            pnlGranteeType.AutoSize = true; // Auto-size to fit radio buttons
            pnlGranteeType.Controls.Add(rdoRole); // Add in desired order
            pnlGranteeType.Controls.Add(rdoUser);
            // Arrange radio buttons horizontally within this panel (Panel defaults to left-to-right)
            rdoUser.Location = new System.Drawing.Point(0, 0); // Relative location within pnlGranteeType
            rdoRole.Location = new System.Drawing.Point(rdoUser.Width + 10, 0); // Position Role after User with space


            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblGranteeType.Text = "Grantee Type:";
            lblGranteeType.Anchor = AnchorStyles.Left | AnchorStyles.Top; 
            lblGranteeType.Margin = new Padding(0, 3, 10, 3); 
            tlpInput.Controls.Add(lblGranteeType, 0, 0);
            pnlGranteeType.Anchor = AnchorStyles.Left | AnchorStyles.Top; // Anchor the panel
            tlpInput.Controls.Add(pnlGranteeType, 1, 0); // Add the radio button panel

            // Row 1: Grantee
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblGrantee.Text = "Grantee:";
            lblGrantee.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblGrantee.Margin = new Padding(0, 3, 10, 3);
            tlpInput.Controls.Add(lblGrantee, 0, 1);
            cmbGrantee.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top; // Stretch ComboBox horizontally
            cmbGrantee.Margin = new Padding(0, 0, 0, 5); // Space below
            tlpInput.Controls.Add(cmbGrantee, 1, 1);

            // Row 2: Privilege
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblPrivilege.Text = "Privilege:";
            lblPrivilege.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblPrivilege.Margin = new Padding(0, 3, 10, 3);
            tlpInput.Controls.Add(lblPrivilege, 0, 2);
            cmbPrivilege.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            cmbPrivilege.Margin = new Padding(0, 0, 0, 5);
            tlpInput.Controls.Add(cmbPrivilege, 1, 2);

            // Row 3: Object Type
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblObjectType.Text = "Object Type:";
            lblObjectType.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblObjectType.Margin = new Padding(0, 3, 10, 3);
            tlpInput.Controls.Add(lblObjectType, 0, 3);
            cmbObjectType.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            cmbObjectType.Margin = new Padding(0, 0, 0, 5);
            tlpInput.Controls.Add(cmbObjectType, 1, 3);

            // Row 4: Object Name
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblObjectName.Text = "Object Name:";
            lblObjectName.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblObjectName.Margin = new Padding(0, 3, 10, 3);
            tlpInput.Controls.Add(lblObjectName, 0, 4);
            cmbObjectName.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            cmbObjectName.Margin = new Padding(0, 0, 0, 5);
            tlpInput.Controls.Add(cmbObjectName, 1, 4);

            // Row 5: Column
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblColumn.Text = "Column (for SELECT/UPDATE):";
            lblColumn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            lblColumn.Margin = new Padding(0, 3, 10, 3);
            tlpInput.Controls.Add(lblColumn, 0, 5);
            cmbColumn.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            cmbColumn.Margin = new Padding(0, 0, 0, 5);
            tlpInput.Controls.Add(cmbColumn, 1, 5);

            // Row 6: CheckBox (Spans both columns)
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            chkWithGrantOption.Text = "WITH GRANT OPTION";
            chkWithGrantOption.Anchor = AnchorStyles.Left | AnchorStyles.Top; // Anchor to the left
            chkWithGrantOption.Margin = new Padding(0, 10, 0, 5); // Add space above
            tlpInput.Controls.Add(chkWithGrantOption, 0, 6);
            tlpInput.SetColumnSpan(chkWithGrantOption, 2); // Make it span two columns

            // Add tlpInput to the main layout panel
            tlpMain.Controls.Add(tlpInput, 0, 0); // Add tlpInput to row 0, column 0 of tlpMain

            flpButtons.Dock = DockStyle.Top; // Dock to top within its cell in tlpMain
            flpButtons.AutoSize = true; // Auto-size based on button size
            flpButtons.FlowDirection = FlowDirection.LeftToRight; // Arrange buttons horizontally
            flpButtons.Padding = new Padding(0, 10, 0, 10); // Add padding above and below the buttons

            // Configure Buttons and add to FlowLayoutPanel
            Size buttonSize = new Size(100, 30); // Consistent button size
            Padding buttonMargin = new Padding(0, 0, 10, 0); // Space between buttons horizontally

            btnGrant.Text = "Grant";
            btnGrant.Size = buttonSize;
            btnGrant.Margin = buttonMargin;
            flpButtons.Controls.Add(btnGrant);
            btnGrant.Click += new System.EventHandler(this.btnGrant_Click);

            btnRevoke.Text = "Revoke";
            btnRevoke.Size = buttonSize;
            btnRevoke.Margin = buttonMargin;
            flpButtons.Controls.Add(btnRevoke);
            btnRevoke.Click += new System.EventHandler(this.btnRevoke_Click);

            btnViewPrivileges.Text = "View Privileges";
            btnViewPrivileges.Size = new Size(130, 30); // Slightly wider
            btnViewPrivileges.Margin = new Padding(0); // No margin after the last button
            flpButtons.Controls.Add(btnViewPrivileges);
            btnViewPrivileges.Click += new System.EventHandler(this.btnViewPrivileges_Click);

            // Add flpButtons to the main layout panel
            tlpMain.Controls.Add(flpButtons, 0, 1); 
            flpButtons.Anchor = AnchorStyles.Top;

            dgvPrivileges.Dock = DockStyle.Fill; // Make it fill the rest of the space
            dgvPrivileges.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPrivileges.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPrivileges.MultiSelect = false;
            dgvPrivileges.ReadOnly = true; // Make it read-only
            dgvPrivileges.AllowUserToAddRows = false;
            dgvPrivileges.AllowUserToDeleteRows = false;
            dgvPrivileges.BackgroundColor = SystemColors.ControlLightLight;
            dgvPrivileges.BorderStyle = BorderStyle.FixedSingle; // Add a border to the grid area
            dgvPrivileges.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control; // Style headers
            dgvPrivileges.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;
            dgvPrivileges.ColumnHeadersDefaultCellStyle.Font = new Font(dgvPrivileges.Font, FontStyle.Bold);
            dgvPrivileges.EnableHeadersVisualStyles = false;

            // Add dgvPrivileges to the main layout panel
            tlpMain.Controls.Add(dgvPrivileges, 0, 2); // Add dgvPrivileges to row 2, column 0 of tlpMain


            // --- Form Settings ---
            this.ClientSize = new System.Drawing.Size(700, 630); // Keep a reasonable default size
            this.Text = "Grant/Revoke Privileges – Oracle Admin";

            this.Controls.Add(tlpMain);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPrivileges)).EndInit();
            this.ResumeLayout(false);
        }
    }
}