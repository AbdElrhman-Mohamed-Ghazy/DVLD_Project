using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAcess
{
    public class clsTestTypesData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"   select* from TestTypes";
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

        public static bool FindTest(int TestTypeID, ref decimal TestTypeFees, ref string TestTypeTitle, ref string TestTypeDescription)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $"SELECT * FROM TestTypes WHERE TestTypeID= @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    TestTypeFees = (decimal)reader["TestTypeFees"];
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];

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


        static public bool UpdateTestType(int TestTypeID,  decimal TestTypeFees,  string TestTypeTitle,  string TestTypeDescription)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestTypes  
                           SET TestTypeFees = @TestTypeFees,
                           TestTypeTitle = @TestTypeTitle,
                            TestTypeDescription=@TestTypeDescription      
                           where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle); 
           command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);



            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

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


            return (rowsAffected > 0);

        }


    }
}
