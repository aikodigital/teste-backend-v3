using System.Globalization;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementService : IStatementService
{
    public StatementEntity Generate(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays)
    {
        var statement = new StatementEntity
        {
            Customer = invoice.Customer
        };

        var totalAmount = 0m;
        var volumeCredits = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var performanceAmount = performance.GetAmount(play);
            var performanceVolumeCredits = performance.GetVolumeCredits(play);

            var statementItem = new StatementItemEntity
            {
                Name = play.Name,
                AmountOwed = performanceAmount / 100,
                EarnedCredits = performanceVolumeCredits,
                Seats = performance.Audience
            };

            totalAmount += performanceAmount;
            volumeCredits += performanceVolumeCredits;

            statement.Items.Add(statementItem);
        }

        statement.AmountOwed = totalAmount / 100;
        statement.EarnedCredits = volumeCredits;

        return statement;
    }

    public string PrintText(StatementEntity statement)
    {
        var cultureInfo = new CultureInfo("en-US");
        var result = $"Statement for {statement.Customer}\n";

        foreach (var item in statement.Items)
        {
            result += string.Format(
                cultureInfo,
                "  {0}: {1:C} ({2} seats)\n",
                item.Name, item.AmountOwed, item.Seats
            );
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", statement.AmountOwed);
        result += $"You earned {statement.EarnedCredits} credits\n";

        return result;
    }

    public string PrintXml(StatementEntity statement)
    {
        var xmlSerializer = new XmlSerializer(statement.GetType());
        using var textWriter = new StringWriter();
        xmlSerializer.Serialize(textWriter, statement);
        
        return textWriter.ToString();
    }
}