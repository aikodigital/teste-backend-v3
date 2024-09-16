using System.Globalization;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services.PlayAmount;
using TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Services;

public class StatementService : IStatementService
{
    private readonly IPlayAmountService _playAmountService;
    private readonly IPlayVolumeCreditsService _playVolumeCreditsService;

    public StatementService(IPlayAmountService playAmountService, IPlayVolumeCreditsService playVolumeCreditsService)
    {
        _playAmountService = playAmountService;
        _playVolumeCreditsService = playVolumeCreditsService;
    }

    public StatementEntity Create(InvoiceEntity invoice, Dictionary<string, PlayEntity> plays)
    {
        var statement = new StatementEntity
        {
            Customer = invoice.Customer,
        };

        var totalAmount = 0m;
        var totalVolumeCredits = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId];
            var playAmount = _playAmountService.GetAmount(
                play: play,
                audience: performance.Audience
            );
            var playVolumeCredits = _playVolumeCreditsService.GetVolumeCredits(
                play: play,
                audience: performance.Audience
            );

            var statementItem = new StatementItemEntity
            {
                Name = play.Name,
                AmountOwed = playAmount / 100,
                EarnedCredits = playVolumeCredits,
                Seats = performance.Audience
            };

            totalAmount += playAmount;
            totalVolumeCredits += playVolumeCredits;

            statement.Items.Add(statementItem);
        }

        statement.AmountOwed = totalAmount / 100;
        statement.EarnedCredits = totalVolumeCredits;

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