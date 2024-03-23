using Microsoft.Data.SqlClient;
using System.Data;

namespace Collaborator.Context
{
    public class ContextDB
    {
        private readonly IConfiguration configuration;
        private readonly string ConnectStr;

        public ContextDB(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectStr = configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(ConnectStr);

    }
}
