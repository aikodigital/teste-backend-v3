using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersRefactoringKata.infra;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;

namespace TheatricalPlayersRefactoringKata.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheatricalPlayersRefactoringKataController : ControllerBase
    {
        private ApiDbContext _db;
        private readonly StatementPrinter _statementPrinter;

        public TheatricalPlayersRefactoringKataController(ApiDbContext db, StatementPrinter statementPrinter)
        {
            _db = db;
        }
    }
}
