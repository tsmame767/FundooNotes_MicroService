using Microsoft.Data.SqlClient;
using System.Data;

namespace Notes.ContextDB
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectStr;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectStr = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(ConnectStr);
    }
}
