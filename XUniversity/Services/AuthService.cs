public class AuthService
{
    private readonly DatabaseService _dbService;
    private string _currentUser;
    private string _currentRole;

    public AuthService(DatabaseService dbService)
    {
        _dbService = dbService;
    }

    public bool Login(string username, string password)
    {
        string query = "SELECT VAITRO FROM NHANVIEN WHERE MANLD = :username";
        var parameters = new Dictionary<string, object>
        {
            { ":username", username }
        };

        var result = _dbService.ExecuteQuery(query, parameters);
        if (result.Rows.Count > 0)
        {
            _currentUser = username;
            _currentRole = result.Rows[0]["VAITRO"].ToString();
            return true;
        }
        return false;
    }

    public bool HasPermission(string permission)
    {
        switch (_currentRole)
        {
            case "NVCB":
                return permission == "VIEW_SELF" || permission == "EDIT_SELF_DT";
            case "TRGDV":
                return permission == "VIEW_DEPT" || permission == "VIEW_MOMON_DEPT";
            case "NV TCHC":
                return true; // Có toàn quyền
            case "GV":
                return permission == "VIEW_MOMON_SELF" || permission == "VIEW_SV_DEPT";
            case "NV PĐT":
                return permission == "MANAGE_MOMON" || permission == "MANAGE_DANGKY";
            case "NV PKT":
                return permission == "MANAGE_DIEM";
            case "NV PCTSV":
                return permission == "MANAGE_SV";
            default:
                return false;
        }
    }
}