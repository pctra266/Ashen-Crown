using System.Diagnostics;
using AshenCrown.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AshenCrown.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new Player() { Name = "Chosen One"});
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [HttpPost]
        [Route("trapro")]
        public IActionResult Arc1([FromForm]int x)
        {
            return Content("the value: "+ x);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
