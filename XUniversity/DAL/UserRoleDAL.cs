using OracleAdminTool.DAL;
using System.Data;

namespace XUniversity.DAL
{
    public class UserRoleDAL
    {
        public void CreateUser(string u, string p) =>
            DatabaseHelper.ExecuteNonQuery($"CREATE USER {u} IDENTIFIED BY {p}");
        public void AlterUser(string u, string np) =>
            DatabaseHelper.ExecuteNonQuery($"ALTER USER {u} IDENTIFIED BY {np}");
        public void DropUser(string u) =>
            DatabaseHelper.ExecuteNonQuery($"DROP USER {u} CASCADE");
        public DataTable GetUsers() =>
            DatabaseHelper.ExecuteTable("SELECT USERNAME, ACCOUNT_STATUS FROM DBA_USERS ORDER BY USERNAME");
        public void CreateRole(string r) =>
            DatabaseHelper.ExecuteNonQuery($"CREATE ROLE {r}");
        public void DropRole(string r) =>
            DatabaseHelper.ExecuteNonQuery($"DROP ROLE {r}");
        public DataTable GetRoles() =>
            DatabaseHelper.ExecuteTable("SELECT ROLE, PASSWORD_REQUIRED FROM DBA_ROLES ORDER BY ROLE");
    }
}