using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace XUniversity.Forms
{
    public partial class GrantRevokePrivilegesForm : Form
    {
        private OracleConnection connection;

        public GrantRevokePrivilegesForm(OracleConnection conn)
        {
            InitializeComponent();
            connection = conn;
            InitializeEvents();
            LoadInitialData();
        }

        public GrantRevokePrivilegesForm()
        {
            InitializeComponent();
            string connStr = "User Id=ADMIN_OLS;Password=123;Data Source=localhost:1521/ORCL21PDB1;";
            connection = new OracleConnection(connStr);
            try
            {
                connection.Open();
                InitializeEvents();
                LoadInitialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }
        }

        /// <summary>
        /// Gán sự kiện cho các control.
        /// </summary>
        private void InitializeEvents()
        {
            cmbObjectType.SelectedIndexChanged += cmbObjectType_SelectedIndexChanged;
            cmbObjectName.SelectedIndexChanged += cmbObjectName_SelectedIndexChanged;
            rdoUser.CheckedChanged += rdoUser_CheckedChanged;
            rdoRole.CheckedChanged += rdoRole_CheckedChanged;
            btnGrant.Click += btnGrant_Click;
            btnRevoke.Click += btnRevoke_Click;
            btnViewPrivileges.Click += btnViewPrivileges_Click;
        }

        /// <summary>
        /// Load thông tin ban đầu.
        /// </summary>
        private void LoadInitialData()
        {
            LoadObjectTypes();
            LoadPrivileges();
            // Danh sách Grantee sẽ được load theo radio button chọn (User/Role)
        }

        /// <summary>
        /// Load danh sách Grantee dựa theo loại người nhận (User hoặc Role).
        /// </summary>
        private void LoadGranteeList()
        {
            cmbGrantee.Items.Clear();

            if (rdoUser.Checked)
            {
                using (OracleCommand cmd = new OracleCommand("SELECT USERNAME FROM ALL_USERS ORDER BY USERNAME", connection))
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        cmbGrantee.Items.Add(reader.GetString(0));
                }
            }
            else if (rdoRole.Checked)
            {
                using (OracleCommand cmd = new OracleCommand("SELECT ROLE FROM DBA_ROLES ORDER BY ROLE", connection))
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        cmbGrantee.Items.Add(reader.GetString(0));
                }
            }
        }

        /// <summary>
        /// Load danh sách loại đối tượng.
        /// </summary>
        private void LoadObjectTypes()
        {
            cmbObjectType.Items.Clear();
            cmbObjectType.Items.AddRange(new string[] { "TABLE", "VIEW", "PROCEDURE", "FUNCTION" });
        }

        /// <summary>
        /// Load danh sách Privilege.
        /// </summary>
        private void LoadPrivileges()
        {
            cmbPrivilege.Items.Clear();
            cmbPrivilege.Items.AddRange(new string[] { "SELECT", "INSERT", "UPDATE", "DELETE", "EXECUTE" });
        }

        /// <summary>
        /// Khi chọn Object Type, load danh sách Object Name từ view ALL_*.
        /// </summary>
        private void cmbObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbObjectName.Items.Clear();
            string type = cmbObjectType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type))
                return;

            string query = "";
            switch (type)
            {
                case "TABLE":
                    query = "SELECT TABLE_NAME FROM ALL_TABLES WHERE OWNER = USER";
                    break;
                case "VIEW":
                    query = "SELECT VIEW_NAME FROM ALL_VIEWS WHERE OWNER = USER";
                    break;
                case "PROCEDURE":
                    query = "SELECT OBJECT_NAME FROM ALL_PROCEDURES WHERE OBJECT_TYPE = 'PROCEDURE' AND OWNER = USER";
                    break;
                case "FUNCTION":
                    query = "SELECT OBJECT_NAME FROM ALL_PROCEDURES WHERE OBJECT_TYPE = 'FUNCTION' AND OWNER = USER";
                    break;
            }

            using (OracleCommand cmd = new OracleCommand(query, connection))
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    cmbObjectName.Items.Add(reader.GetString(0));
            }
        }

        /// <summary>
        /// Khi chọn Object Name, load danh sách Column từ view ALL_TAB_COLUMNS.
        /// </summary>
        private void cmbObjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbColumn.Items.Clear();
            string tableName = cmbObjectName.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(tableName))
                return;

            string query = $"SELECT COLUMN_NAME FROM ALL_TAB_COLUMNS WHERE TABLE_NAME = '{tableName}' AND OWNER = USER";
            using (OracleCommand cmd = new OracleCommand(query, connection))
            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    cmbColumn.Items.Add(reader.GetString(0));
            }
        }

        private void rdoUser_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoUser.Checked)
                LoadGranteeList();
        }

        private void rdoRole_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRole.Checked)
                LoadGranteeList();
        }

        /// <summary>
        /// Xử lý cấp quyền (GRANT) cho đối tượng.
        /// </summary>
        private void btnGrant_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra các trường bắt buộc
                if (string.IsNullOrEmpty(cmbPrivilege.Text.Trim()))
                {
                    MessageBox.Show("Please select a Privilege.");
                    return;
                }
                if (string.IsNullOrEmpty(cmbObjectName.Text.Trim()))
                {
                    MessageBox.Show("Please select an Object Name.");
                    return;
                }
                if (string.IsNullOrEmpty(cmbGrantee.Text.Trim()))
                {
                    MessageBox.Show("Please select a Grantee.");
                    return;
                }
                string privilege = cmbPrivilege.Text.Trim();
                string objectName = cmbObjectName.Text.Trim();
                string grantee = cmbGrantee.Text.Trim();
                string withGrant = chkWithGrantOption.Checked ? " WITH GRANT OPTION" : "";

                string query = "";
                if (privilege == "EXECUTE")
                    query = $"GRANT EXECUTE ON {objectName} TO {grantee}{withGrant}";
                else
                    query = $"GRANT {privilege} ON {objectName} TO {grantee}{withGrant}";

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Privilege granted successfully.");
                }
                // Refresh lại danh sách quyền
                btnViewPrivileges_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grant error: " + ex.Message);
            }
        }


        /// <summary>
        /// Xử lý thu hồi quyền (REVOKE) dựa trên dòng được chọn trong DataGridView.
        /// </summary>
        private void btnRevoke_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPrivileges.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a privilege from the list to revoke.");
                    return;
                }

                DataGridViewRow selectedRow = dgvPrivileges.SelectedRows[0];
                string grantee = selectedRow.Cells["GRANTEE"].Value?.ToString() ?? "";
                string privilege = selectedRow.Cells["PRIVILEGE"].Value?.ToString() ?? "";
                string objectName = selectedRow.Cells["OBJECT_NAME"].Value?.ToString() ?? "";
                string column = "";
                if (selectedRow.Cells["COLUMN_NAME"].Value != DBNull.Value)
                    column = selectedRow.Cells["COLUMN_NAME"].Value.ToString();

                if (string.IsNullOrEmpty(grantee) || string.IsNullOrEmpty(privilege) || string.IsNullOrEmpty(objectName))
                {
                    MessageBox.Show("Selected privilege entry is invalid.");
                    return;
                }

                string columnsClause = "";
                if ((privilege == "SELECT" || privilege == "UPDATE") && !string.IsNullOrEmpty(column))
                    columnsClause = $"({column})";

                string query = "";
                if (privilege == "EXECUTE")
                    query = $"REVOKE EXECUTE ON {objectName} FROM {grantee}";
                else
                    query = $"REVOKE {privilege}{columnsClause} ON {objectName} FROM {grantee}";

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Privilege revoked successfully.");
                }
                // Refresh lại danh sách quyền
                btnViewPrivileges_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Revoke error: " + ex.Message);
            }
        }

        /// <summary>
        /// Load và hiển thị danh sách các quyền đã cấp trong DataGridView.
        /// </summary>
        private void btnViewPrivileges_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"
                        SELECT GRANTEE, PRIVILEGE, TABLE_SCHEMA || '.' || TABLE_NAME AS OBJECT_NAME,
                               'TABLE/VIEW' AS OBJECT_TYPE, COLUMN_NAME, 
                               GRANTABLE AS GRANT_OPTION
                        FROM ALL_COL_PRIVS
                        UNION ALL
                        SELECT GRANTEE, PRIVILEGE, TABLE_SCHEMA || '.' || TABLE_NAME AS OBJECT_NAME,
                               'TABLE/VIEW' AS OBJECT_TYPE, NULL AS COLUMN_NAME,
                               GRANTABLE AS GRANT_OPTION
                        FROM ALL_TAB_PRIVS
                        ORDER BY GRANTEE, OBJECT_NAME, PRIVILEGE";

                OracleDataAdapter adapter = new OracleDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvPrivileges.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("View error: " + ex.Message);
            }
        }
    }
}
