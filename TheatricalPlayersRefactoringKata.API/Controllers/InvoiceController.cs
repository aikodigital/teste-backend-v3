using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
