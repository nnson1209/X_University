using System;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using XUniversity.DAL;

namespace XUniversity.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.btnLogin.Click += new EventHandler(btnLogin_Click);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Kiểm tra đăng nhập với Oracle
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT MANLD as USERNAME, VAITRO as ROLE 
                                   FROM X_ADMIN.NHANVIEN 
                                   WHERE MANLD = :username 
                                   AND PASSWORD = :password
                                   UNION ALL
                                   SELECT MASV as USERNAME, 'SV' as ROLE
                                   FROM X_ADMIN.SINHVIEN
                                   WHERE MASV = :username
                                   AND PASSWORD = :password";
                    
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add(":password", OracleDbType.Varchar2).Value = password;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["ROLE"].ToString();
                                
                                // Lưu thông tin user vào session
                                SessionManager.CurrentUser = new UserSession
                                {
                                    Username = username,
                                    Role = role
                                };

                                // Mở form chính
                                MainForm mainForm = new MainForm();
                                this.Hide();
                                mainForm.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Test kết nối database
            if (!DatabaseHelper.TestConnection(out string error))
            {
                MessageBox.Show($"Không thể kết nối đến database: {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }

    public class UserSession
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }

    public static class SessionManager
    {
        public static UserSession CurrentUser { get; set; }
    }
} 