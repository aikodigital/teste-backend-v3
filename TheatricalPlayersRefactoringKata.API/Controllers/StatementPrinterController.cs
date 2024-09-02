using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    public class StatementPrinterController : Controller
    {
        public StatementPrinterController()
        {
            
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }
    }
}
