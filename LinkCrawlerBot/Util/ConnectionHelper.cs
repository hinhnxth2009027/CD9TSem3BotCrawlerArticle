using System.Data;
using System.Data.SqlClient;

namespace LinkCrawlerBot.Util
{
    public class ConnectionHelper
    {
        private const string ConnectionString = 
            // @"Server=tcp:nuocdenchandbserver.database.windows.net,1433;Initial Catalog=Nuocdenchan_db;Persist Security Info=False;User ID=admincd9t;Password=Coder9tuoi@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=nuocdenchan;Integrated Security=True";
        
        private static SqlConnection _sqlConnection;

        public static SqlConnection GetConnection()
        {
            if (_sqlConnection == null || _sqlConnection.State == ConnectionState.Closed)
            {
                _sqlConnection = new SqlConnection(
                    string.Format(ConnectionString));
            }

            return _sqlConnection;
        }
    }
}