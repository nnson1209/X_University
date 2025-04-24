using System.Data;
using XUniversity.DAL;

namespace XUniversity.BLL
{
    public class UserRoleBLL
    {
        private readonly UserRoleDAL dal = new UserRoleDAL();
        public DataTable LoadUsers() => dal.GetUsers();
        public DataTable LoadRoles() => dal.GetRoles();
        public void CreateUser(string u, string p) => dal.CreateUser(u, p);
        public void AlterUser(string u, string np) => dal.AlterUser(u, np);
        public void DropUser(string u) => dal.DropUser(u);
        public void CreateRole(string r) => dal.CreateRole(r);
        public void DropRole(string r) => dal.DropRole(r);
    }
}