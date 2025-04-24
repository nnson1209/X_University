using System.Data;
using XUniversity.DAL;

namespace XUniversity.BLL
{
    public class PermissionBLL
    {
        private readonly PermissionDAL dal = new PermissionDAL();
        public void Grant(string priv, string obj, string to, bool wgo) => dal.GrantPrivilege(priv, obj, to, wgo);
        public void Revoke(string priv, string obj, string to) => dal.RevokePrivilege(priv, obj, to);
        public DataTable ObjPrivs(string g) => dal.GetObjectPrivs(g);
        public DataTable ColPrivs(string g) => dal.GetColumnPrivs(g);
        public DataTable SysPrivs(string g) => dal.GetSysPrivs(g);
        public DataTable RoleGrants(string g) => dal.GetRoleGrants(g);
    }
}