using Oracle.ManagedDataAccess.Client;
using OracleAdminTool.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XUniversity
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            BuildInitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(744, 353);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (DatabaseHelper.TestConnection(out var error))
                MessageBox.Show("Kết nối thành công đến Oracle!", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"Lỗi kết nối: {error}", "Test Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnViewData_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = OracleAdminTool.DAL.DatabaseHelper.ExecuteTable("SELECT * FROM SINHVIEN FETCH FIRST 10 ROWS ONLY");
                dgvPreview.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn dữ liệu: {ex.Message}", "View Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
