using Microsoft.AspNetCore.Mvc;

namespace API_ETL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
