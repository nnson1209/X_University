using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace QuanLyDuLieuNoiBo.UI
{
    public partial class NhanVienForm : Form
    {
        private string _currentUserID;
        private string _vaiTro;
        private OracleConnection _connection;

        public NhanVienForm(string userID, string vaiTro, OracleConnection connection)
        {
            //InitializeComponent();
            _currentUserID = userID;
            _vaiTro = vaiTro;
            _connection = connection;

            InitializeUI();
            //LoadData();
        }

        private void InitializeUI()
        {
            // Set up form layout and controls
            this.Text = "Quản lý Nhân viên";
            this.Size = new System.Drawing.Size(900, 700);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create main layout panels
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 2;
            mainLayout.ColumnCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.Padding = new Padding(10);

            // Create input panel with border
            GroupBox inputGroupBox = new GroupBox();
            inputGroupBox.Text = "Thông tin Nhân viên";
            inputGroupBox.Dock = DockStyle.Fill;
            inputGroupBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Create the panel for data grid view
            Panel gridPanel = new Panel();
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Padding = new Padding(0, 5, 0, 0);

            // Create data grid view with better formatting
            dgvNhanVien = new DataGridView();
            dgvNhanVien.Dock = DockStyle.Fill;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.ReadOnly = _vaiTro != "NV TCHC";
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            dgvNhanVien.BorderStyle = BorderStyle.Fixed3D;
            dgvNhanVien.RowHeadersWidth = 25;
            dgvNhanVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dgvNhanVien.CellClick += DgvNhanVien_CellClick;
            dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;

            // Input controls layout
            TableLayoutPanel inputLayout = new TableLayoutPanel();
            inputLayout.Dock = DockStyle.Fill;
            inputLayout.ColumnCount = 3;
            inputLayout.RowCount = 5;
            inputLayout.Padding = new Padding(10);

            // Configure the columns
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            inputLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));

            // Configure the rows - 4 rows for input fields, 1 row for buttons
            for (int i = 0; i < 4; i++)
            {
                inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            }
            inputLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            // First column controls

            // MANV input
            Label lblMANV = new Label();
            lblMANV.Text = "Mã NV:";
            lblMANV.Dock = DockStyle.Fill;
            lblMANV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlMANV = new FlowLayoutPanel();
            pnlMANV.Dock = DockStyle.Fill;
            pnlMANV.Padding = new Padding(0, 0, 10, 0);

            txtMANV = new TextBox();
            txtMANV.Width = 180;
            txtMANV.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlMANV.Controls.Add(lblMANV);
            pnlMANV.Controls.Add(txtMANV);

            // HOTEN input
            Label lblHOTEN = new Label();
            lblHOTEN.Text = "Họ tên:";
            lblHOTEN.Dock = DockStyle.Fill;
            lblHOTEN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlHOTEN = new FlowLayoutPanel();
            pnlHOTEN.Dock = DockStyle.Fill;
            pnlHOTEN.Padding = new Padding(0, 0, 10, 0);

            txtHOTEN = new TextBox();
            txtHOTEN.Width = 180;
            txtHOTEN.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlHOTEN.Controls.Add(lblHOTEN);
            pnlHOTEN.Controls.Add(txtHOTEN);

            // PHAI input
            Label lblPHAI = new Label();
            lblPHAI.Text = "Phái:";
            lblPHAI.Dock = DockStyle.Fill;
            lblPHAI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlPHAI = new FlowLayoutPanel();
            pnlPHAI.Dock = DockStyle.Fill;
            pnlPHAI.Padding = new Padding(0, 0, 10, 0);

            cbPHAI = new ComboBox();
            cbPHAI.Width = 180;
            cbPHAI.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbPHAI.Items.AddRange(new object[] { "Nam", "Nữ" });
            cbPHAI.DropDownStyle = ComboBoxStyle.DropDownList;

            pnlPHAI.Controls.Add(lblPHAI);
            pnlPHAI.Controls.Add(cbPHAI);

            // NGSINH input
            Label lblNGSINH = new Label();
            lblNGSINH.Text = "Ngày sinh:";
            lblNGSINH.Dock = DockStyle.Fill;
            lblNGSINH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlNGSINH = new FlowLayoutPanel();
            pnlNGSINH.Dock = DockStyle.Fill;
            pnlNGSINH.Padding = new Padding(0, 0, 10, 0);

            dtpNGSINH = new DateTimePicker();
            dtpNGSINH.Width = 180;
            dtpNGSINH.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpNGSINH.Format = DateTimePickerFormat.Short;

            pnlNGSINH.Controls.Add(lblNGSINH);
            pnlNGSINH.Controls.Add(dtpNGSINH);

            // Second column controls

            // LUONG input
            Label lblLUONG = new Label();
            lblLUONG.Text = "Lương:";
            lblLUONG.Dock = DockStyle.Fill;
            lblLUONG.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlLUONG = new FlowLayoutPanel();
            pnlLUONG.Dock = DockStyle.Fill;
            pnlLUONG.Padding = new Padding(0, 0, 10, 0);

            txtLUONG = new TextBox();
            txtLUONG.Width = 180;
            txtLUONG.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlLUONG.Controls.Add(lblLUONG);
            pnlLUONG.Controls.Add(txtLUONG);

            // PHUCAP input
            Label lblPHUCAP = new Label();
            lblPHUCAP.Text = "Phụ cấp:";
            lblPHUCAP.Dock = DockStyle.Fill;
            lblPHUCAP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlPHUCAP = new FlowLayoutPanel();
            pnlPHUCAP.Dock = DockStyle.Fill;
            pnlPHUCAP.Padding = new Padding(0, 0, 10, 0);

            txtPHUCAP = new TextBox();
            txtPHUCAP.Width = 180;
            txtPHUCAP.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlPHUCAP.Controls.Add(lblPHUCAP);
            pnlPHUCAP.Controls.Add(txtPHUCAP);

            // DT input
            Label lblDT = new Label();
            lblDT.Text = "Điện thoại:";
            lblDT.Dock = DockStyle.Fill;
            lblDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlDT = new FlowLayoutPanel();
            pnlDT.Dock = DockStyle.Fill;
            pnlDT.Padding = new Padding(0, 0, 10, 0);

            txtDT = new TextBox();
            txtDT.Width = 180;
            txtDT.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            pnlDT.Controls.Add(lblDT);
            pnlDT.Controls.Add(txtDT);

            // Third column controls

            // VAITRO input
            Label lblVAITRO = new Label();
            lblVAITRO.Text = "Vai trò:";
            lblVAITRO.Dock = DockStyle.Fill;
            lblVAITRO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlVAITRO = new FlowLayoutPanel();
            pnlVAITRO.Dock = DockStyle.Fill;
            pnlVAITRO.Padding = new Padding(0, 0, 10, 0);

            cbVAITRO = new ComboBox();
            cbVAITRO.Width = 180;
            cbVAITRO.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbVAITRO.Items.AddRange(new object[] { "NVCB", "GV", "NV PĐT", "NV PKT", "NV TCHC", "NV CTSV", "TRGĐV" });
            cbVAITRO.DropDownStyle = ComboBoxStyle.DropDownList;

            pnlVAITRO.Controls.Add(lblVAITRO);
            pnlVAITRO.Controls.Add(cbVAITRO);

            // MADV input
            Label lblMADV = new Label();
            lblMADV.Text = "Mã ĐV:";
            lblMADV.Dock = DockStyle.Fill;
            lblMADV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            FlowLayoutPanel pnlMADV = new FlowLayoutPanel();
            pnlMADV.Dock = DockStyle.Fill;
            pnlMADV.Padding = new Padding(0, 0, 10, 0);

            cbMADV = new ComboBox();
            cbMADV.Width = 180;
            cbMADV.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cbMADV.DropDownStyle = ComboBoxStyle.DropDownList;

            pnlMADV.Controls.Add(lblMADV);
            pnlMADV.Controls.Add(cbMADV);

            // Action buttons
            FlowLayoutPanel pnlButtons = new FlowLayoutPanel();
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
            pnlButtons.WrapContents = false;
            pnlButtons.Padding = new Padding(0, 5, 0, 0);

            btnLamMoi = new Button();
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.Width = 100;
            btnLamMoi.Height = 35;
            //btnLamMoi.Click += BtnLamMoi_Click;
            btnLamMoi.Margin = new Padding(5, 0, 0, 0);
            btnLamMoi.BackColor = System.Drawing.Color.LightBlue;
            btnLamMoi.FlatStyle = FlatStyle.Flat;

            btnXoa = new Button();
            btnXoa.Text = "Xóa";
            btnXoa.Width = 100;
            btnXoa.Height = 35;
            //btnXoa.Click += BtnXoa_Click;
            btnXoa.Margin = new Padding(5, 0, 0, 0);
            btnXoa.Visible = _vaiTro == "NV TCHC";
            btnXoa.BackColor = System.Drawing.Color.LightCoral;
            btnXoa.FlatStyle = FlatStyle.Flat;

            btnSua = new Button();
            btnSua.Text = "Sửa";
            btnSua.Width = 100;
            btnSua.Height = 35;
            //btnSua.Click += BtnSua_Click;
            btnSua.Margin = new Padding(5, 0, 0, 0);
            btnSua.BackColor = System.Drawing.Color.LightGreen;
            btnSua.FlatStyle = FlatStyle.Flat;

            btnThem = new Button();
            btnThem.Text = "Thêm";
            btnThem.Width = 100;
            btnThem.Height = 35;
            //btnThem.Click += BtnThem_Click;
            btnThem.Margin = new Padding(5, 0, 0, 0);
            btnThem.Visible = _vaiTro == "NV TCHC";
            btnThem.BackColor = System.Drawing.Color.LightGreen;
            btnThem.FlatStyle = FlatStyle.Flat;

            pnlButtons.Controls.Add(btnLamMoi);
            pnlButtons.Controls.Add(btnXoa);
            pnlButtons.Controls.Add(btnSua);
            pnlButtons.Controls.Add(btnThem);

            // Add all controls to the input layout
            inputLayout.Controls.Add(pnlMANV, 0, 0);
            inputLayout.Controls.Add(pnlHOTEN, 0, 1);
            inputLayout.Controls.Add(pnlPHAI, 0, 2);
            inputLayout.Controls.Add(pnlNGSINH, 0, 3);

            inputLayout.Controls.Add(pnlLUONG, 1, 0);
            inputLayout.Controls.Add(pnlPHUCAP, 1, 1);
            inputLayout.Controls.Add(pnlDT, 1, 2);

            inputLayout.Controls.Add(pnlVAITRO, 2, 0);
            inputLayout.Controls.Add(pnlMADV, 2, 1);

            // Add button panel that spans all columns
            inputLayout.Controls.Add(pnlButtons, 0, 4);
            inputLayout.SetColumnSpan(pnlButtons, 3);

            // Add input layout to the group box
            inputGroupBox.Controls.Add(inputLayout);

            // Add data grid view to the grid panel
            gridPanel.Controls.Add(dgvNhanVien);

            // Add panels to the main layout
            mainLayout.Controls.Add(inputGroupBox, 0, 0);
            mainLayout.Controls.Add(gridPanel, 0, 1);

            // Add main layout to the form
            this.Controls.Add(mainLayout);

            // Configure form for different roles
            //ConfigureFormForRole();
        }

        // Control declarations
        private DataGridView dgvNhanVien;
        private TextBox txtMANV;
        private TextBox txtHOTEN;
        private ComboBox cbPHAI;
        private DateTimePicker dtpNGSINH;
        private TextBox txtLUONG;
        private TextBox txtPHUCAP;
        private TextBox txtDT;
        private ComboBox cbVAITRO;
        private ComboBox cbMADV;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // NhanVienForm
            // 
            this.ClientSize = new System.Drawing.Size(834, 470);
            this.Name = "NhanVienForm";
            this.ResumeLayout(false);

        }
    }
}