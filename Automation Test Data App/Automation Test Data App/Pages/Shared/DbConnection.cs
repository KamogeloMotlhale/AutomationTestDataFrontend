using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Shared
{
    public static class DbConnection
    {
        static private String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
        static private SqlDataReader? sqlDataReader;
        static private SqlConnection connection = new SqlConnection(connectionString);

        private static SqlCommand openDb(String sql)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            return command;
        }
        public static SqlDataReader readDataFromDB(String sql)
        {
            var cmd = openDb(sql);
            sqlDataReader = cmd.ExecuteReader();
            return sqlDataReader;

        }
        public static void removeCreateUpdateDataOnDB(String sql)
        {
            var cmd = openDb(sql);
            cmd.ExecuteNonQuery();
        }

        public static void closeDbConnection()
        {
            connection.Close();
        }

    }
}
