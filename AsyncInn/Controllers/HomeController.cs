using Microsoft.AspNetCore.Mvc;

namespace AsyncInn.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Connects the Controller to Views
        /// </summary>
        /// <returns>Routing to Views</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}