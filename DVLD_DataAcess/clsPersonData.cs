using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAcess
{
    public class clsPersonData
    {
        public static bool FindPerson(string ColumName, object Value, ref int personID, ref string nationalNo, ref string phone, ref int nationalityCountryID, ref string firstName, ref string secondName,
                                                        ref string thirdName, ref string lastName, ref DateTime dateOfBirth, ref string gendor, ref string address, ref string email, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $"SELECT * FROM People WHERE {ColumName} = @Value";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Value", Value);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personID = reader["PersonID"] != DBNull.Value ? (int)reader["PersonID"] : -1;
                    nationalNo = reader["NationalNo"] != DBNull.Value ? (string)reader["NationalNo"] : "";
                    phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : "";
                    nationalityCountryID = reader["NationalityCountryID"] != DBNull.Value ? (int)reader["NationalityCountryID"] : -1;

                    firstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : "";
                    secondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                    thirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    lastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";

                    dateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue;
                    gendor = reader["Gendor"] != DBNull.Value ? ((int)reader["Gendor"] == 0 ? "Male" : "Female")  : "";
                    address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : "";
                    email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }

                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool FindPerson( int PersonID, ref string nationalNo, ref string phone, ref int nationalityCountryID, ref string firstName, ref string secondName,
                                                       ref string thirdName, ref string lastName, ref DateTime dateOfBirth, ref string gendor, ref string address, ref string email, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $"SELECT * FROM People WHERE PersonID= @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    nationalNo = reader["NationalNo"] != DBNull.Value ? (string)reader["NationalNo"] : "";
                    phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : "";
                    nationalityCountryID = reader["NationalityCountryID"] != DBNull.Value ? (int)reader["NationalityCountryID"] : -1;

                    firstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : "";
                    secondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                    thirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    lastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";

                    dateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue;
                    gendor = reader["Gendor"] != DBNull.Value ? (Convert.ToInt32(reader["Gendor"]) == 0 ? "Male" : "Female") : "";
                    address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : "";
                    email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }

                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

              public static bool FindPerson(string NationalNo, ref int PersonID,ref string phone, ref int nationalityCountryID, ref string firstName, ref string secondName,
                                                       ref string thirdName, ref string lastName, ref DateTime dateOfBirth, ref string gendor, ref string address, ref string email, ref string imagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $"SELECT * FROM People WHERE NationalNo= @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID = reader["PersonID"] != DBNull.Value ? (int)reader["PersonID"] : -1;
                    phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : "";
                    nationalityCountryID = reader["NationalityCountryID"] != DBNull.Value ? (int)reader["NationalityCountryID"] : -1;

                    firstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : "";
                    secondName = reader["SecondName"] != DBNull.Value ? (string)reader["SecondName"] : "";
                    thirdName = reader["ThirdName"] != DBNull.Value ? (string)reader["ThirdName"] : "";
                    lastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : "";

                    dateOfBirth = reader["DateOfBirth"] != DBNull.Value ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue;
                    gendor = reader["Gendor"] != DBNull.Value ? (Convert.ToInt32(reader["Gendor"]) == 0 ? "Male" : "Female") : "";
                    address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : "";
                    email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : "";
                    imagePath = reader["ImagePath"] != DBNull.Value ? (string)reader["ImagePath"] : "";
                }

                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewPerson(string NationalNo, string Phone, int NationalityCountryID, string FirstName, string SecondName,
                                                               string ThirdName, string LastName, DateTime DateOfBirth, string Gendor, string Address, string Email, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People (NationalNo, Phone, NationalityCountryID, FirstName, SecondName, ThirdName, LastName, 
                                                                                        DateOfBirth, Gendor, Address, Email, ImagePath)
                             VALUES (@NationalNo, @Phone, @NationalityCountryID, @FirstName, @SecondName, @ThirdName, @LastName,
                                                                               @DateOfBirth, @Gendor, @Address, @Email, @ImagePath);
                                                                                      SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor == "Male" ? 0 : 1);
            command.Parameters.AddWithValue("@Address", Address);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            if (!string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            finally
            {
                connection.Close();
            }


            return PersonID;
        
        }

        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete People 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
    
        public static bool UpdatePerson(int PersonID, string NationalNo, string Phone, int NationalityCountryID, string FirstName, string SecondName,
                                                              string ThirdName, string LastName, DateTime DateOfBirth, string Gendor, string Address, string Email, string ImagePath)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  People  
                           SET NationalNo = @NationalNo,
                           Phone = @Phone,
                           NationalityCountryID = @NationalityCountryID,
                           FirstName = @FirstName,
                           SecondName = @SecondName,
                           ThirdName = @ThirdName,
                           LastName = @LastName,
                           DateOfBirth = @DateOfBirth,
                           Gendor = @Gendor,
                           Address = @Address,
                           Email = @Email,
                           ImagePath = @ImagePath
                           where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor == "Male" ? 0 : 1);
            command.Parameters.AddWithValue("@Address", Address);

            if (ImagePath != "" && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            if (Email != "" && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);


            if (ThirdName != "" && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static DataTable FilterBy(string ColumName, object Value)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@" SELECT PersonID,NationalNo,   Phone,NationalityCountryID,FirstName,SecondName,ThirdName,LastName,DateOfBirth,
                                                           CASE WHEN Gendor = 0 THEN 'Male' ELSE 'Female' END AS Gendor,Address,Email,ImagePath
                                                            FROM People  WHERE {ColumName} LIKE @Value";
            SqlCommand command = new SqlCommand(query, sqlConnection);

            // 💡 الحل: تحويل Value إلى نص (string) أولاً
            command.Parameters.AddWithValue("@Value", "%" + Value + "%");

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                // ... (معالجة الخطأ)
            }
            finally
            {
                sqlConnection.Close();
            }
            return dt;
        }
        public static DataTable GetAllPerson()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection= new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =
          @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth,   
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as Gendor ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY PersonID";

            ;
            SqlCommand command = new SqlCommand(query, sqlConnection);
            try
            {
                sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return dt;
        }
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool IsPersonExist(int PersonID )
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}
