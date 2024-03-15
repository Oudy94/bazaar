using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSandwichMakersHardwareStoreSolution.Helpers
{
    public class DatabaseHelper
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=xxxxxx;User Id=xxxxxx;Password=xxxxxx;"; //HERE ADD YOUR OWN CONNECTION STRING!!
        private SqlConnection connection;

        public DatabaseHelper()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error opening database connection: {ex.Message}");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error closing database connection: {ex.Message}");
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM [dbo].[employee] WHERE email = '{email}' AND password = '{password}'";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex) 
            {
                throw new Exception($"Error authenticating user: {ex.Message}", ex);
            }
        }
    }
}
