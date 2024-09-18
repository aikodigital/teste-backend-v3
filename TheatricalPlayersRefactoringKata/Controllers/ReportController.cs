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

        /// <summary>
        /// Endpoint for generating reports from multiple Customers, 
        /// each report will be saved in the Report folder in the root directory
        /// </summary>
        /// <param name="customersNames">List of Customers Names</param>
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
