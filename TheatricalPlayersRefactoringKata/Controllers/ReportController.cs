using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Interfaces;

namespace TheatricalPlayersRefactoringKata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("ReportByCustomer")]
        public Task<ActionResult> ReportByCustomer(List<string> customersNames)
        {
            try
            {
                return _reportService.ReportByCustomer(customersNames);
            }
            catch
            {
                throw;
            }
        }
    }
}
