using TP.Domain.Entities;

public interface IStatementFormatter
{
    string FormatStatement(Invoice invoice, Dictionary<string, Play> plays, decimal totalAmount, int volumeCredits);
}