using System.Globalization;
using System.Linq;
using TheatricalPlayersRefactoringKata.Dtos;
using TheatricalPlayersRefactoringKata.Infra;

namespace TheatricalPlayersRefactoringKata.Tickets;

public class StatementPrinter
{
    private readonly CultureInfo _cultureInfo;

    public StatementPrinter()
    {
        _cultureInfo = new CultureInfo("en-US");
    }

    public string Print(StatementDto statementDto)
    {
        var result = string.Format("Statement for {0}\n", statementDto.Customer);

        statementDto.Items.ToList().ForEach(i =>
            result += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", i.Name, i.AmountOwed, i.Seats)
        );

        result += string.Format(_cultureInfo, "Amount owed is {0:C}\n", statementDto.AmountOwed);
        result += string.Format("You earned {0} credits\n", statementDto.EarnedCredits);

        return result;
    }

    public string PrintXml(StatementDto statementDto)
    {
        return XmlHelper.GetXmlDeserializedObject(statementDto);
    }
}
