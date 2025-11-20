using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAcess
{
    public class clsApplicationTypesData
    {

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"   select* from ApplicationTypes";
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

        public static bool FindApp(int ApplicationTypeID,ref float ApplicationFees, ref string ApplicationTypeTitle)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID= @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicationFees =Convert.ToSingle( reader["ApplicationFees"]);
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];

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


        static public bool UpdateApplicationType(int ApplicationTypeID, float ApplicationFees,  string ApplicationTypeTitle)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  ApplicationTypes  
                           SET ApplicationFees = @ApplicationFees,
                           ApplicationTypeTitle = @ApplicationTypeTitle
                           where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);


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
