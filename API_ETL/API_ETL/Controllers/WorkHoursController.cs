//Author: Anushka Sharma
//Date: August 1, 2022
//Purpose: The WorkHoursController handles the the GET request (including optional parameters), forms the SQL query string, passes it to SQLHandler for the request to be completed,
//and then returns the result. 

using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;
using System.Text;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHoursController : Controller
    {

        //form connection string
        //send it to SQL class
        //have SQL Class return a SQLObject?
        //verify any parameters to avoid injection attacks
        //parse through the SQL Object returned according to params

        public string connectionString = 
              "Data Source=hoeldsqedm01;" + 
              "Initial Catalog=DataHub_OESSHE;" + 
              "Integrated Security=SSPI;";

        [HttpGet]
        /// <summary>The unique identifier</summary>
        public string getParameters([FromQuery] string? StartDate, string? EndDate, int? DepartmentID, int? PersonnelTypeID)
        {
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

            sqlRequest += "SELECT TOP 500 * FROM [DataHub_OESSHE].[IMPACT].[VW_PER_WORK_HOURS]";
            bool hasParam = false;
            
            //handles DepartmentID param if present
            if (DepartmentID != null)
            {
                sqlRequest += " WHERE DEPARTMENT_1_ID = " + DepartmentID;
                hasParam = true;
            }

            //handles DepartmentID param if present
            if (PersonnelTypeID != null)
            {
                if (hasParam)
                {
                    //already Where keyword
                    sqlRequest += (" AND PERSONNEL_TYPE_ID = " + PersonnelTypeID);
                } else { 
                //first Where keyword
                    sqlRequest += " WHERE PERSONNEL_TYPE_ID = " + PersonnelTypeID;
                    hasParam = true;
                }
            }

            if (StartDate != null)
            {
                //try parsing the string
                
                //System.Diagnostics.Debug.WriteLine("****StartDateFormatted: " + StartDateFormatted + "****");
                if (hasParam)
                {
                    //already Where keyword
                    sqlRequest += (" AND TIME_PERIOD > CAST(@StartDateFormatted AS DATETIME)");
                }
                else
                {
                    //first Where keyword
                    sqlRequest += " WHERE TIME_PERIOD > CAST(@StartDateFormatted AS DATETIME)";
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
                    sqlRequest += (" AND TIME_PERIOD < CAST(@EndDateFormatted AS DATETIME)");
                }
                else
                {
                    //first Where keyword
                    sqlRequest += " WHERE TIME_PERIOD < CAST(@EndDateFormatted AS DATETIME)";
                    hasParam = true;
                }
            }

            sqlRequest += " FOR JSON AUTO;";
            System.Diagnostics.Debug.WriteLine("****query: " + sqlRequest + "****");

            string result = SQLHandler.getAllData(connectionString, sqlRequest);
            return result;
            //return new WorkHours().get() + startYear + endYear + business + dept_exp_type + cutoff;
        }

    }
}
