using MySql.Data.MySqlClient;
using System.Configuration;

namespace vKitchenv
{
    public static class DbHelper
    {
        public static MySqlConnection GetConnection()
        {
            //NE DIRATI NISTA, samo u App.config podesiti parametre
            var cs = ConfigurationManager.ConnectionStrings["vKitchenvConn"].ConnectionString;
            var conn = new MySqlConnection(cs);
            conn.Open();
            return conn;
        }
    }
}
