using XUniversity.DAL;
using System.Data;

namespace XUniversity.DAL
{
    public class PermissionDAL
    {
        public void GrantPrivilege(string priv, string obj, string to, bool wgo)
        {
            var opt = wgo ? " WITH GRANT OPTION" : string.Empty;
            DatabaseHelper.ExecuteNonQuery($"GRANT {priv} ON {obj} TO {to}{opt}");
        }
        public void RevokePrivilege(string priv, string obj, string grantee) =>
            DatabaseHelper.ExecuteNonQuery($"REVOKE {priv} ON {obj} FROM {grantee}");

        public DataTable GetObjectPrivs(string g) =>
            DatabaseHelper.ExecuteTable($"SELECT OWNER,TABLE_NAME,PRIVILEGE,GRANTABLE FROM DBA_TAB_PRIVS WHERE GRANTEE='{g}'");
        public DataTable GetColumnPrivs(string g) =>
            DatabaseHelper.ExecuteTable($"SELECT TABLE_NAME,COLUMN_NAME,PRIVILEGE,GRANTABLE FROM DBA_COL_PRIVS WHERE GRANTEE='{g}'");
        public DataTable GetSysPrivs(string g) =>
            DatabaseHelper.ExecuteTable($"SELECT PRIVILEGE,ADMIN_OPTION FROM DBA_SYS_PRIVS WHERE GRANTEE='{g}'");
        public DataTable GetRoleGrants(string g) =>
            DatabaseHelper.ExecuteTable($"SELECT GRANTED_ROLE,ADMIN_OPTION FROM DBA_ROLE_PRIVS WHERE GRANTEE='{g}'");
    }
}