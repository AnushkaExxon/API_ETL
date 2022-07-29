using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace API_ETL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //private static void CreateCommand(string queryString, string connectionString)
        //{
        //    using (SqlConnection connection = new SqlConnection(
        //               connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(queryString, connection);
        //        command.Connection.Open();
        //        command.ExecuteNonQuery();
        //    }
        //}

        //[HttpGet(Name = "getname")]
        //public string getname()
        //{
        //    return "anushka";
        //}
    }
}
/* Questions: how does the HttpGet command work?
 * Why doesn't GetName() work?
 * How do you debug effectively if it just sends general error? I've tried breakpoints, but they don't seem to help
 * 
 * 
[HttpGet(Name = "GetDate")]
public IEnumerable<WeatherForecast> GetDate()
{
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
}
*/
