using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Configuration;

namespace XUniversity.DAL
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["OracleDbContext"].ConnectionString;

        public static OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }

        public static bool TestConnection(out string error)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    error = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public static DataTable ExecuteTable(string query)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new OracleCommand(query, conn))
                {
                    var dt = new DataTable();
                    using (var adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        public static int ExecuteNonQuery(string query)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new OracleCommand(query, conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string query)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new OracleCommand(query, conn))
                {
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}