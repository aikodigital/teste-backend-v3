using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    public class PerformanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
