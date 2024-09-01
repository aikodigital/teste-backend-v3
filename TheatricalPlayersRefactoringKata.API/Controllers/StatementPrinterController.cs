using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    public class StatementPrinterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
