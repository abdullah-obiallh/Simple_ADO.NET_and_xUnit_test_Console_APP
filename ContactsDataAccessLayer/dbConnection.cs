using System;

using System.Data.SqlClient;

namespace ContactsDataAccessLayer {
    public static class dbConnection {
       public static readonly string connectionString = "Server=.;database=ContactsDB;User ID=sa;Password=11223344";
        public static bool TryConnection() {
            SqlConnection conn = new SqlConnection(connectionString);
            try {
                conn.Open();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }finally { 
                conn.Close(); 
            }
        }
    }
}
