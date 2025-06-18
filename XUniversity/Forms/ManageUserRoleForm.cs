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
            DisplayData(
                "SELECT username FROM all_users WHERE oracle_maintained = 'N'"
            );
        }

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            DisplayDataFromProcedure("get_local_roles");
        }


        private void DisplayData(string sql)
        {
            pnlMain.Controls.Clear();
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            pnlMain.Controls.Add(dgv);

            try
            {
                using (var cmd = new OracleCommand(sql, connection))
                using (var adapter = new OracleDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgv.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayDataFromProcedure(string procName)
        {
            pnlMain.Controls.Clear();
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            pnlMain.Controls.Add(dgv);

            try
            {
                using (var cmd = new OracleCommand(procName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);
                        dgv.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Lỗi khi gọi procedure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblUsername = new Label { Text = "Username", Top = 10, Left = 10 };
            var txtUsername = new TextBox { Top = 30, Left = 10, Width = 200 };
            var lblPassword = new Label { Text = "Password", Top = 60, Left = 10 };
            var txtPassword = new TextBox { Top = 80, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Create", Top = 110, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_username", txtUsername.Text),
            new OracleParameter("p_password", txtPassword.Text)
                };

                ExecProcedure("proc_create_user", parameters, "User created successfully.", "Lỗi tạo user");
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
            var btnSubmit = new Button { Text = "Update", Top = 110, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_username", txtUsername.Text),
            new OracleParameter("p_new_password", txtNewPassword.Text)
                };

                ExecProcedure("proc_update_user_password", parameters, "User password updated successfully.", "Lỗi cập nhật user");
            };

            pnlMain.Controls.AddRange(new Control[] { lblUsername, txtUsername, lblNewPassword, txtNewPassword, btnSubmit });
        }


        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblUsername = new Label { Text = "Username", Top = 10, Left = 10 };
            var txtUsername = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Delete", Top = 60, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_username", txtUsername.Text)
                };

                ExecProcedure("proc_drop_user", parameters, "User deleted successfully.", "Lỗi xóa user");
            };

            pnlMain.Controls.AddRange(new Control[] { lblUsername, txtUsername, btnSubmit });
        }


        private void btnCreateRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Create Role", Top = 60, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_role_name", txtRole.Text)
                };

                ExecProcedure("proc_create_role", parameters, "Role created successfully.", "Lỗi tạo role");
            };

            pnlMain.Controls.AddRange(new Control[] { lblRole, txtRole, btnSubmit });
        }

        private void btnEditRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Old Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var lblNewRole = new Label { Text = "New Role Name", Top = 60, Left = 10 };
            var txtNewRole = new TextBox { Top = 80, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Rename", Top = 110, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_old_name", txtRole.Text),
            new OracleParameter("p_new_name", txtNewRole.Text)
                };

                ExecProcedure("proc_rename_role", parameters, "Role renamed successfully.", "Lỗi cập nhật role");
            };

            pnlMain.Controls.AddRange(new Control[] { lblRole, txtRole, lblNewRole, txtNewRole, btnSubmit });
        }


        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            var lblRole = new Label { Text = "Role Name", Top = 10, Left = 10 };
            var txtRole = new TextBox { Top = 30, Left = 10, Width = 200 };
            var btnSubmit = new Button { Text = "Delete Role", Top = 60, Left = 10 };

            btnSubmit.Click += (s, args) =>
            {
                var parameters = new OracleParameter[]
                {
            new OracleParameter("p_role_name", txtRole.Text)
                };

                ExecProcedure("proc_drop_role", parameters, "Role deleted successfully.", "Lỗi xóa role");
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

        private void ExecProcedure(string procName, OracleParameter[] parameters, string successMessage, string errorTitle)
        {
            try
            {
                using (var cmd = new OracleCommand(procName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show(successMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, errorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
