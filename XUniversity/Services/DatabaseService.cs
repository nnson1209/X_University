public class DatabaseService
{
    private readonly string _connectionString;
    private OracleConnection _connection;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Connect()
    {
        _connection = new OracleConnection(_connectionString);
        _connection.Open();
    }

    public void Disconnect()
    {
        if (_connection != null && _connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
    {
        using (OracleCommand cmd = new OracleCommand(query, _connection))
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param.Key, param.Value);
                }
            }

            DataTable dt = new DataTable();
            using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
            {
                adapter.Fill(dt);
            }
            return dt;
        }
    }

    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
    {
        using (OracleCommand cmd = new OracleCommand(query, _connection))
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param.Key, param.Value);
                }
            }
            return cmd.ExecuteNonQuery();
        }
    }
}