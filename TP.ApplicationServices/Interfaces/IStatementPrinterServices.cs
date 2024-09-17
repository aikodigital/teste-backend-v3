using TP.Domain.Entities;

namespace TP.ApplicationServices.Interfaces
{
    public interface IStatementPrinterServices
    {
        string Print(Invoice invoice, Dictionary<string, Play> plays, string format);
    }
}
