using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System;

namespace OracleAdminTool.DAL
{
    public static class DatabaseHelper
    {
        // Đọc connection string từ App.config
        public static readonly string ConnString =
            ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using (var conn = new OracleConnection(ConnString))
                {
                    conn.Open();
                    errorMessage = null;
                    return conn.State == ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        // Trả về dạng bảng cho truy vấn SELECT
        public static DataTable ExecuteTable(string sql, params OracleParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (OracleConnection conn = new OracleConnection(ConnString))
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // Thực thi INSERT/UPDATE/DELETE hoặc DDL
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            int result;
            using (OracleConnection conn = new OracleConnection(ConnString))
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public static OracleConnection GetOpenConnection()
        {
            OracleConnection conn = new OracleConnection(ConnString);
            conn.Open();
            return conn;
        }
    }
}