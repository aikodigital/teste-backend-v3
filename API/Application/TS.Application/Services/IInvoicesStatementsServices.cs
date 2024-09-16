namespace TS.Application.Services
{
    public interface IInvoicesStatementsServices
    {
        Task XML(long invoiceId);
        Task TXT(long invoiceId);
    }
}