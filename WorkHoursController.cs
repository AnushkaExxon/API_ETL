using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkHoursController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

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
        public string getParameters([FromQuery] DateTime? StartYear, int? EndYear, int? DepartmentID, int? PersonnelTypeID)
        {
            string sqlRequest = "SELECT * FROM [DataHub_OESSHE].[IMPACT].[VW_PER_WORK_HOURS]";
            bool hasParam = false;
            if (DepartmentID != null)
            {
                sqlRequest += " WHERE DEPARTMENT_1_ID = " + DepartmentID;
                hasParam = true;
            }
            if (PersonnelTypeID != null)
            {
                if (hasParam)
                {
                    //already Where keyword
                    sqlRequest += (" AND PERSONNEL_TYPE_ID = " + PersonnelTypeID);
                }
                //first Where keyword
                sqlRequest += " WHERE PERSONNEL_TYPE_ID = " + PersonnelTypeID;
                hasParam = true;
            }
            
            sqlRequest += " FOR JSON AUTO";
            string result = SQLHandler.getAllData(connectionString, sqlRequest);
            return result;
            //return new WorkHours().get() + startYear + endYear + business + dept_exp_type + cutoff;
        }

    }
}
