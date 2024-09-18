using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IReportService
    {
        public Task<ActionResult> ReportByCustomer(List<string> customersNames);
    }
}
