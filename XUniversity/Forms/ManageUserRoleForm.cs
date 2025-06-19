using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace XUniversity.Forms
{
    public partial class ManageUserRoleForm : Form
    {
        private OracleConnection connection;

        // Constructor không tham số: tự đọc connection string và mở kết nối
        public ManageUserRoleForm()
        {
            InitializeComponent();

            // Đọc connection string từ app.config
            string cs = ConfigurationManager
                            .ConnectionStrings["OracleDbContext"]
                            .ConnectionString;

            connection = new OracleConnection(cs);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới Oracle: " + ex.Message,
                                "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Constructor cho phép truyền connection từ bên ngoài nếu cần
        public ManageUserRoleForm(OracleConnection conn) : this()
        {
            if (conn != null)
            {
                connection.Close();
                connection = conn;
            }
        }

        private void btnViewUsers_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách user trên dgvUsers
            try
            {
                using (var cmd = new OracleCommand("SELECT username FROM all_users WHERE oracle_maintained = 'N'", connection))
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            // Hiển thị danh sách role trên dgvRoles
            try
            {
                using (var cmd = new OracleCommand("SELECT role FROM dba_roles WHERE COMMON = 'NO'", connection))
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvRoles.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblUsername = new Label { Text = "Username", Top = 10, Left = 10 };
            var txtUsername = new TextBox { Top = 30, Left = 10, Width = 200 };
            var lblPassword = new Label { Text = "Password", Top = 60, Left = 10 };
            var txtPassword = new TextBox { Top = 80, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Create", Top = 120, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"CREATE USER {txtUsername.Text} IDENTIFIED BY {txtPassword.Text}",
                    "User created successfully.",
                    "Lỗi tạo user"
                );

                ExecNonQuery(
                    $"GRANT CONNECT TO {txtUsername.Text}",
                    $"Quyền CONNECT đã được cấp cho user {txtUsername.Text}.",
                    $"Lỗi cấp quyền CONNECT cho user {txtUsername.Text}"
                );
                btnViewUsers_Click(null, null); // Refresh danh sách
            };

            pnlMain.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblPassword, txtPassword, btnSubmit });
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblUsername = new Label { Text = "Username", Top = 10, Left = 10 };
            var txtUsername = new TextBox { Top = 30, Left = 10, Width = 200 };
            var lblNewPassword = new Label { Text = "New Password", Top = 60, Left = 10 };
            var txtNewPassword = new TextBox { Top = 80, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Update", Top = 120, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"ALTER USER {txtUsername.Text} IDENTIFIED BY {txtNewPassword.Text}",
                    "User password updated successfully.",
                    "Lỗi cập nhật user"
                );
                ExecNonQuery(
                    $"GRANT CONNECT TO {txtUsername.Text}",
                    $"Quyền CONNECT đã được cấp cho user {txtUsername.Text}.",
                    $"Lỗi cấp quyền CONNECT cho user {txtUsername.Text}"
                );
                btnViewUsers_Click(null, null);
            };

            pnlMain.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblNewPassword, txtNewPassword, btnSubmit });
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblUsername = new Label { Text = "Username", Top = 10, Left = 10 };
            var txtUsername = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Delete", Top = 70, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"DROP USER {txtUsername.Text} CASCADE",
                    "User deleted successfully.",
                    "Lỗi xóa user"
                );
                btnViewUsers_Click(null, null);
            };

            pnlMain.Controls.AddRange(new Control[] { lblUsername, txtUsername, btnSubmit });
        }

        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Create Role", Top = 70, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"CREATE ROLE {txtRole.Text}",
                    "Role created successfully.",
                    "Lỗi tạo role"
                );
                btnViewRoles_Click(null, null);
            };

            pnlMain.Controls.AddRange(new Control[] { lblRole, txtRole, btnSubmit });
        }

        private void btnEditRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var lblNewRole = new Label { Text = "New Role Name", Top = 60, Left = 10 };
            var txtNewRole = new TextBox { Top = 80, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Rename", Top = 120, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"ALTER ROLE {txtRole.Text} RENAME TO {txtNewRole.Text}",
                    "Role renamed successfully.",
                    "Lỗi cập nhật role"
                );
                btnViewRoles_Click(null, null);
            };

            pnlMain.Controls.AddRange(new Control[] { lblRole, txtRole, lblNewRole, txtNewRole, btnSubmit });
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Delete Role", Top = 70, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                ExecNonQuery(
                    $"DROP ROLE {txtRole.Text}",
                    "Role deleted successfully.",
                    "Lỗi xóa role"
                );
                btnViewRoles_Click(null, null);
            };

            pnlMain.Controls.AddRange(new Control[] { lblRole, txtRole, btnSubmit });
        }

        /// <summary>
        /// Thực thi câu lệnh SQL không trả về dữ liệu
        /// </summary>
        private void ExecNonQuery(string sql, string successMsg, string errorTitle)
        {
            try
            {
                using (var cmd = new OracleCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show(successMsg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Đảm bảo đóng kết nối khi form bị đóng
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
