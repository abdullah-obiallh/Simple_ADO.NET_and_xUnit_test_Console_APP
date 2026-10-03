using System;
using System.Data;
using System.Data.SqlClient;


namespace ContactsDataAccessLayer
{
    public static class RetriveFromDataBase
    {
     public static bool GetContactInfoByID(int ID,ref string FirstName,ref string LastName,
         ref string Address,ref string Email,ref string Phone,ref string ImagePath,ref DateTime DateOfBirth,ref int CountryID) {
            bool found = false;
            SqlConnection connection = new SqlConnection(dbConnection.connectionString);
            string query = @"select * from Contacts Where Contacts.ContactID = @ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);


            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
             
                if (reader.Read()) {
                    found = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    if (reader["ImagePath"] !=  DBNull.Value) {
                    ImagePath = (string)reader["ImagePath"];
                    }
                    else {
                        ImagePath="";
                    }
                }
                else {
                    found = false;
                    Console.WriteLine("Id Not Found: "+ ID);
                }
                reader.Close();
            }
            catch (Exception ex) {
                Console.WriteLine("Error Occurred ContactsDataAccessLayer.GetContactInfoByID" + ex.Message);
            }finally {
                connection.Close();
            }

            return found;
        }   
        public static int InsertContact( string firstName,  string lastName,
          string address,  string email,  string phone, DateTime dateOfBirth,  int countryID, string imagePath = "") {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(dbConnection.connectionString);
            string query = @"Insert Into Contacts(FirstName,LastName,Email,Phone,Address,DateOfBirth,ImagePath,CountryID)
                            Values(@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth,@ImagePath,@CountryID);
                            Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@CountryID", countryID);
            command.Parameters.AddWithValue("@Address", address);
            if (imagePath != "") {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            else {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

            try {
                connection.Open();
                object result = command.ExecuteScalar();
                if (int.TryParse(result.ToString(),out int parseValue)) {
                    ContactID = parseValue;
                    Console.WriteLine("Parse Successfully and Read Successfully");
                }
                else {
                    Console.WriteLine("Somthing occurred");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error Occurred: "+ ex.Message);
            }
            finally {
                connection.Close();
            }
            return ContactID;
        }
        public static bool UpdateContact(int contactID,string firstName, string lastName,
          string address, string email, string phone, DateTime dateOfBirth, int countryID, string imagePath = "") {

            int effectedRow = 0;
            bool isUpdated = false;

            SqlConnection connection =new SqlConnection(dbConnection.connectionString);
            
            string query = @"Update Contacts
                            set
                            FirstName=@firstname,
                            LastName=@lastname,
                            Email=@email,
                            Phone=@phone,
                            Address=@address,
                            DateOfBirth=@dateOfBirth,
                            CountryID=@countryID,
                            ImagePath=@imagePath
                            Where ContactID=@contactID";

            SqlCommand command =new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", contactID);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@CountryID", countryID);
            command.Parameters.AddWithValue("@Address", address);
            if (imagePath != "") {
                command.Parameters.AddWithValue("@ImagePath", imagePath);
            }
            else {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }


            try {
                connection.Open();
                 effectedRow = command.ExecuteNonQuery();
                if (effectedRow>0) {
                    isUpdated=true;
                    Console.WriteLine("Row effected: "+ effectedRow);
                }
                else {
                    isUpdated = false;
                    Console.WriteLine("Somthing Happen: "+ effectedRow);
                }

            } catch (Exception ex) {

                Console.WriteLine("Something Occurred on UpdateFun :\n" + ex.Message);
            }
            finally {
                connection.Close();
            }
            return isUpdated;
        }
        public static bool DeleteContactFromDatabase(int contactID) {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(dbConnection.connectionString);

            string query = @"Delete Contacts where ContactID=@contactID";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@contactID", contactID);
            try {
                connection.Open();
                rowAffected= command.ExecuteNonQuery();
                if (rowAffected>0) {
                    Console.WriteLine("Delete Successfully Row Affected: "+rowAffected);

                }
                else { 
                    Console.WriteLine("Somthing Happen..: Row Affected"+rowAffected);
                }

            }
            catch (Exception ex) {
                Console.WriteLine("exception in delete fun: "+ex.Message);
            }
            finally {
                connection.Close();
            }


            return (rowAffected>0) ;
        }
        public static DataTable GetAllContact() {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(dbConnection.connectionString);
            string query = "Select * from Contacts";

            SqlCommand command = new SqlCommand(query,connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) {
                    dt.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex) {
                Console.WriteLine("in GetAllContact..:"+ex.Message);
            }
            finally {
                connection.Close();
            }
            return  dt ;
        }
        public static bool IsExistContact(int contactID) {
            bool isFound= false;

            SqlConnection connection=new SqlConnection(dbConnection.connectionString);

            string query = "select found = 1 from Contacts where ContactID=@contactID";

            SqlCommand command = new SqlCommand(query,connection);

            command.Parameters.AddWithValue("@contactID", contactID);

            try {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null) {
                    isFound= true;
                    Console.WriteLine("Found :"+contactID);
                }
                else {
                    isFound= false;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Is ExcistContact : ..."+ ex.Message);
            }finally { 
                connection.Close(); 
            }

            return isFound;
        }

        public static int GetCountryIDByName(string countryName) {
            int countryNumber = -1;
            SqlConnection connection = new SqlConnection(dbConnection.connectionString);

            string query = "Select CountryID from Countries Where Countries.CountryName=@countryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@countryName", countryName);
            try {
                connection.Open();
                object result = command.ExecuteScalar();
                if ((result!=null)?int.TryParse(result.ToString(), out int resultFromParse):false) {
                    countryNumber = resultFromParse;
                    Console.WriteLine("Country Found And Parsed Successfully");
                }
                else {
                    Console.WriteLine("Error Occurred Durring The Parse Process");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("in GetCountryIDByNameFun :" + ex.Message);
            }finally { 
                connection.Close(); 
            }

            return countryNumber;
        }
    }
}
