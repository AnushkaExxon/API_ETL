//Author: Anushka Sharma
//Date: August 1, 2022
//Purpose: The HierarchyController handles the the GET request (including optional parameters), forms the SQL query string, passes it to SQLHandler for the request to be completed,
//and then returns the result, all for the Hierarchy table.

using Microsoft.AspNetCore.Mvc;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HierarchyController : Controller
    {
        public string connectionString =
              "Data Source=hoeldsqedm01;" +
              "Initial Catalog=DataHub_OESSHE;" +
              "Integrated Security=SSPI;";
        [HttpGet]
        /// <summary>The unique identifier</summary>
        public string getParameters([FromQuery] int? DepartmentID)
        {
            string sqlRequest = "";

            sqlRequest += "SELECT TOP 500 * FROM [DataHub_OESSHE].[IMPACT].[VW_PER_HIERARCHY]";
            if (DepartmentID != null)
            {
                sqlRequest += " WHERE DEPARTMENT_1_ID = " + DepartmentID;
                
            }
            sqlRequest += " FOR JSON AUTO;";
            System.Diagnostics.Debug.WriteLine("****query: " + sqlRequest + "****");

            string result = SQLHandler.getAllData(connectionString, sqlRequest);
            return result;
        }
    }

    
}
