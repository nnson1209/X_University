using System;
using System.Drawing;
using System.Windows.Forms;
using XUniversity.Utils;
//using QuanLyDuLieuNoiBo.Models;
//using QuanLyDuLieuNoiBo.Utils;

namespace XUniversity.UI
{
    public partial class DashboardForm : Form
    {
        //private UserAccount currentUser;

        public DashboardForm()
        {
            InitializeComponent();
            //this.currentUser = SessionManager.CurrentUser;
            this.Load += DashboardForm_Load;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin người dùng đăng nhập
            if (SessionManager.CurrentUser != null)
            {
                lblWelcome.Text = $"Chào mừng {SessionManager.CurrentUser.Username} - {GetRoleDisplayName(SessionManager.CurrentUser.Role)}";
                lblStatus.Text = $"Đăng nhập với vai trò: {GetRoleDisplayName(SessionManager.CurrentUser.Role)}";
            }
        }

        private string GetRoleDisplayName(string roleCode)
        {
            switch (roleCode)
            {
                case "NVCB": return "Nhân viên cơ bản";
                case "GV": return "Giảng viên";
                case "NV PDT": return "Nhân viên Phòng Đào tạo";
                case "NV PKT": return "Nhân viên Phòng Khảo thí";
                case "NV TCHC": return "Nhân viên Phòng Tổ chức hành chính";
                case "NV CTSV": return "Nhân viên Phòng Công tác sinh viên";
                case "TRGDV": return "Trưởng đơn vị";
                case "SINHVIEN": return "Sinh viên";
                default: return roleCode;
            }
        }

        private void menuItemNhanVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemSinhVien_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemDonVi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemHocPhan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemMoMon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemDangKy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemThongBao_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemNhatKy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemBackup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuItemLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.CurrentUser = null;
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void OpenChildForm(Form childForm)
        {
            // Xóa tất cả các control hiện tại trong panel
            pnlMain.Controls.Clear();

            // Cấu hình form con
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm form con vào panel
            pnlMain.Controls.Add(childForm);
            childForm.Show();
        }

        private void DashboardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SessionManager.CurrentUser != null)
            {
                Application.Exit();
            }
        }
    }
}