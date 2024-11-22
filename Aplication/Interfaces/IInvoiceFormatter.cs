using Aplication.DTO;

namespace Aplication.Interfaces
{
    public interface IInvoiceFormatter
    {
        string Format(InvoiceDto invoice, int valorTotal, int valorCreditos, 
            IEnumerable<PerformanceResult> performances);
    }

}
