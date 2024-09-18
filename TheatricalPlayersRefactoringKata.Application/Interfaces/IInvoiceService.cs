using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Application.Models;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface IInvoiceService
    {
        public Task<ActionResult> Create(InvoiceModel invoice);
        public Task<ActionResult> GetAllByCustomer(string customerName);
        public Task<ActionResult> GetAllByPlay(string playId);
        public Task<ActionResult> Delete(string id);
    }
}
