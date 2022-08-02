using Microsoft.AspNetCore.Mvc;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnplannedEventController : Controller
    {
        public string connectionString =
             "Data Source=hoeldsqedm01;" +
             "Initial Catalog=DataHub_OESSHE;" +
             "Integrated Security=SSPI;";

        [HttpGet]
        /// <summary>The unique identifier</summary>
        public string getParameters([FromQuery] string? StartDate, string? EndDate)
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

            sqlRequest += "SELECT TOP 200 * FROM [DataHub_OESSHE].[IMPACT].[VW_PER_UNPLANNED_EVENT]";
            bool hasParam = false;


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
            //return new WorkHours().get() + startYear + endYear + business + dept_exp_type + cutoff;
        }

    }
}
