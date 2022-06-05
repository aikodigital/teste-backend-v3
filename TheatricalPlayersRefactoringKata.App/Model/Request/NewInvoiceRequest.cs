using TheatricalPlayersRefactoringKata.App.ViewModel;

namespace TheatricalPlayersRefactoringKata.App.Model.Request
{
    public class NewInvoiceRequest
    {
        public long CustomerId { get; set; }
        public List<PerformanceViewModel> PerformanceViewModels { get; set; }
    }
}