using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XUniversity.Forms
{
    public partial class LoginForm : Form
    {
        private OracleConnection connection;

        public LoginForm()
        {
            InitializeComponent();
            string connStr = "User Id=ADMIN_OLS;Password=123;Data Source=localhost:1521/ORCL21PDB1;";
            connection = new OracleConnection(connStr); // dùng user quản trị để kiểm tra thông tin đăng nhập
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim().ToUpper();
            string password = txtPassword.Text;

            try
            {
                connection.Open();

                string query = @"
                    SELECT ROLE
                    FROM USER_ROLES
                    WHERE USERNAME = :username AND PASSWORD = :password";

                OracleCommand cmd = new OracleCommand(query, connection);
                cmd.Parameters.Add(new OracleParameter("username", username));
                cmd.Parameters.Add(new OracleParameter("password", password));
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string role = result.ToString();

                    MessageBox.Show($"Welcome, {username} ({role})!", "Login Successful");

                    this.Hide();

                    if (role == "ADMIN")
                    {
                        MainForm mainForm = new MainForm();
                        mainForm.ShowDialog();
                    }
                    else
                    {
                        EmployeeForm empForm = new EmployeeForm(connection, username, role);
                        empForm.ShowDialog();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
