using MySqlConnector;
using System.Data;

namespace BE_tasteal.Persistence.Context
{
    public class ConnectionManager
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;

        public ConnectionManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                _connection = new MySqlConnection(connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
