using Newtonsoft.Json;
using System.Linq;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Models.Dtos;
using Formatting = Newtonsoft.Json.Formatting;

namespace TheatricalPlayersRefactoringKata.Application.Adapters;

public class JsonFormatterAdapter : IFormatterAdapter
{
    public string Format(Statement statement)
    {
        // TODO: AJUSTAR CRIAÇÃO DO DTO
        var statementDto = new StatementDto
        {
            Customer = statement.Customer,
            TotalAmountOwed = statement.TotalAmountOwed,
            TotalEarnedCredits = statement.TotalEarnedCredits,
            Items = statement.Items.Select(item => new StatementItemDto
            {
                AmountOwed = item.AmountOwed,
                EarnedCredits = item.EarnedCredits,
                Seats = item.Seats
            }).ToList()
        };

        return JsonConvert.SerializeObject(statementDto, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
        });
    }
}
