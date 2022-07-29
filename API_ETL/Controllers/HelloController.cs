using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ETL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        /// <summary>
        /// returns simple hello message
        /// </summary>
        /// <param name="suffix"></param>
        /// <returns></returns>
        [HttpGet]
        public string getName([FromQuery] string? suffix = "")
        {
            return new Hello().get() + suffix;
        }
        [HttpGet("{name}")]
        public string getNameFromURL(string name)
        {
            return name;
        }
    }
}
