//Author: Anushka Sharma
//Date: August 2, 2022
//Purpose: The ProcessSafetyController handles the the GET request (including optional parameters), forms the SQL query string, passes it to SQLHandler for the request to be completed,
//and then returns the result, all for the Process Safety table.

using Microsoft.AspNetCore.Mvc;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessSafetyController : Controller
    {
        public string connectionString =
              "Data Source=hoeldsqedm01;" +
              "Initial Catalog=DataHub_OESSHE;" +
              "Integrated Security=SSPI;";

        [HttpGet]
        //Tip: mark the optional parameters with a "?"
        public string getParameters([FromQuery] string? StartDate, string? EndDate, int? DepartmentID)
        {

            //declares relevant variables in SQL
            string sqlRequest = "";
            if (StartDate != null)
            {
                DateTime StartDateFormatted;
                DateTime.TryParse(StartDate, out StartDateFormatted);

                sqlRequest += "DECLARE @StartDateFormatted VARCHAR(30) = '" + StartDateFormatted + "';";
            }

            if (EndDate != null)
            {
                DateTime EndDateFormatted;
                DateTime.TryParse(EndDate, out EndDateFormatted);

                sqlRequest += "DECLARE @EndDateFormatted VARCHAR(30) = '" + EndDateFormatted + "';";
            }

            //starts the SQL request
            sqlRequest += "SELECT TOP 500 * FROM [DataHub_OESSHE].[IMPACT].[VW_PER_PROCESS_SAFETY]";
            bool hasParam = false;

            //handles DepartmentID param if present
            if (DepartmentID != null)
            {
                sqlRequest += " WHERE DEPARTMENT_1_ID = " + DepartmentID;
                hasParam = true;
            }

            if (StartDate != null)
            {
                //try parsing the string

                //System.Diagnostics.Debug.WriteLine("****StartDateFormatted: " + StartDateFormatted + "****");
                if (hasParam)
                {
                    //already Where keyword
                    sqlRequest += (" AND OCCURRED_DATE > CAST(@StartDateFormatted AS DATETIME)");
                }
                else
                {
                    //first Where keyword
                    sqlRequest += " WHERE OCCURRED_DATE > CAST(@StartDateFormatted AS DATETIME)";
                    hasParam = true;
                }
            }

            if (EndDate != null)
            {
                //try parsing the string

                //System.Diagnostics.Debug.WriteLine("****StartDateFormatted: " + StartDateFormatted + "****");
                if (hasParam)
                {
                    //already Where keyword
                    sqlRequest += (" AND OCCURRED_DATE < CAST(@EndDateFormatted AS DATETIME)");
                }
                else
                {
                    //first Where keyword
                    sqlRequest += " WHERE OCCURRED_DATE < CAST(@EndDateFormatted AS DATETIME)";
                    hasParam = true;
                }
            }

            sqlRequest += " FOR JSON AUTO;";
            System.Diagnostics.Debug.WriteLine("****query: " + sqlRequest + "****");

            string result = SQLHandler.getAllData(connectionString, sqlRequest);
            return result;

        }
    }
}
