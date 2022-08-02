//Author: Anushka Sharma
//Date: August 1, 2022
//Purpose: The SQLHandler accepts the SQL query string, executes the query, and returns the JSON output as a string.
//and then returns the result. 

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace API_ETL
{
    public class SQLHandler
    {
        public static SqlDataReader reader;

        public static string getAllData(string connectionString, string query)
        {
            using (SqlConnection connection =
                   new SqlConnection(connectionString))
            {
                reader = null;
                var jsonResult = new StringBuilder();
                
                //System.Diagnostics.Debug.WriteLine("****Testing****");
                try
                {
                    // 2. Open the connection
                    connection.Open();

                    // 3. Pass the connection to a command object
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //
                    // 4. Use the connection and get query results
                    reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        jsonResult.Append("[]");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            jsonResult.Append(reader.GetValue(0).ToString());
                            System.Diagnostics.Debug.WriteLine("****"+ reader.GetValue(0).ToString() + "****");
                            //System.Diagnostics.Debug.WriteLine("****jsonResult: " + jsonResult + "****");
                        }
                    }

                } finally {
                    // close the reader
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                return jsonResult.ToString();
            }
        }
    }
}
