using Oracle.ManagedDataAccess.Client;
using OracleAdminTool.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using XUniversity.Forms;

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
            this.ClientSize = new System.Drawing.Size(900, 600);
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
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new LoginForm().ShowDialog();
            this.Close();
        }
    }
}
