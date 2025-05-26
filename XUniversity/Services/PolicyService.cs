public class PolicyService
{
    private readonly DatabaseService _dbService;
    private readonly string _currentUser;

    public PolicyService(DatabaseService dbService, string currentUser)
    {
        _dbService = dbService;
        _currentUser = currentUser;
    }

    // NHANVIEN Policy
    public DataTable GetNhanVienData()
    {
        string query = "SELECT * FROM NHANVIEN";
        return _dbService.ExecuteQuery(query);
    }

    public bool UpdateNhanVienPhone(string manv, string newPhone)
    {
        string query = "UPDATE NHANVIEN SET DT = :dt WHERE MANLD = :manv";
        var parameters = new Dictionary<string, object>
        {
            { ":dt", newPhone },
            { ":manv", manv }
        };
        return _dbService.ExecuteNonQuery(query, parameters) > 0;
    }

    // MOMON Policy
    public DataTable GetMoMonData()
    {
        string query = "SELECT * FROM MOMON";
        return _dbService.ExecuteQuery(query);
    }

    public bool AddMoMon(string mahp, string magv, int hk, int nam)
    {
        string query = "INSERT INTO MOMON (MAHP, MAGV, HK, NAM) VALUES (:mahp, :magv, :hk, :nam)";
        var parameters = new Dictionary<string, object>
        {
            { ":mahp", mahp },
            { ":magv", magv },
            { ":hk", hk },
            { ":nam", nam }
        };
        return _dbService.ExecuteNonQuery(query, parameters) > 0;
    }

    // SINHVIEN Policy
    public DataTable GetSinhVienData()
    {
        string query = "SELECT * FROM SINHVIEN";
        return _dbService.ExecuteQuery(query);
    }

    public bool UpdateSinhVienInfo(string masv, string dchi, string dt)
    {
        string query = "UPDATE SINHVIEN SET DCHI = :dchi, DT = :dt WHERE MASV = :masv";
        var parameters = new Dictionary<string, object>
        {
            { ":dchi", dchi },
            { ":dt", dt },
            { ":masv", masv }
        };
        return _dbService.ExecuteNonQuery(query, parameters) > 0;
    }

    // DANGKY Policy
    public DataTable GetDangKyData()
    {
        string query = "SELECT * FROM DANGKY";
        return _dbService.ExecuteQuery(query);
    }

    public bool UpdateDiem(string masv, int mamm, decimal diemth, decimal diemqt, decimal diemck, decimal diemtk)
    {
        string query = "UPDATE DANGKY SET DIEMTH = :diemth, DIEMQT = :diemqt, " +
                      "DIEMCK = :diemck, DIEMTK = :diemtk " +
                      "WHERE MASV = :masv AND MAMM = :mamm";
        var parameters = new Dictionary<string, object>
        {
            { ":diemth", diemth },
            { ":diemqt", diemqt },
            { ":diemck", diemck },
            { ":diemtk", diemtk },
            { ":masv", masv },
            { ":mamm", mamm }
        };
        return _dbService.ExecuteNonQuery(query, parameters) > 0;
    }
}