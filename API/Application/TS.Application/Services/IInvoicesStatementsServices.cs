using TS.Domain.Enums;

namespace TS.Application.Services
{
    public interface IInvoicesStatementsServices
    {
        Task GenerateFile(ETypeFile typeFile,
                          long invoiceId);
    }
}