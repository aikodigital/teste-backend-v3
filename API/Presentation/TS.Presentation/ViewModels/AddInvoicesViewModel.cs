using TS.Domain.Enums;

namespace TS.Presentation.ViewModels
{
    public class AddInvoiceViewModel
    {
        public ETypeFile TypeFile { get; set; }
        public long CustomerId { get; set; }
        public decimal Seats { get; set; }
        public virtual IEnumerable<AddInvoicePerformancesViewModel> Performances { get; set; } = [];
    }

    public class AddInvoicePerformancesViewModel
    {
        public long PlayId { get; set; }
        public int Audience { get; set; }
    }
}