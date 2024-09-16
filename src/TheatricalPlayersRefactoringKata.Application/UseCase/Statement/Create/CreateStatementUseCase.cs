using TheatricalPlayersRefactoringKata.Entities;
using TheatricalPlayersRefactoringKata.Services.PlayAmount;
using TheatricalPlayersRefactoringKata.Services.PlayVolumeCredits;

namespace TheatricalPlayersRefactoringKata.Application.UseCase.Statement.Create;

public class CreateStatementUseCase : ICreateStatementUseCase
{
    private readonly IPlayAmountService _playAmountService;
    private readonly IPlayVolumeCreditsService _playVolumeCreditsService;

    public CreateStatementUseCase(IPlayAmountService playAmountService, IPlayVolumeCreditsService playVolumeCreditsService)
    {
        _playAmountService = playAmountService;
        _playVolumeCreditsService = playVolumeCreditsService;
    }

    public CreateStatementOutput Execute(CreateStatementInput input)
    {
        var invoice = input.Invoice;
        var plays = input.Plays;

        var statement = new StatementEntity(
            customer: invoice.Customer
        );

        var totalAmount = 0m;
        var totalVolumeCredits = 0;

        foreach (var performance in invoice.Performances)
        {
            var play = plays[performance.PlayId].ToEntity();
            var playAmount = _playAmountService.GetAmount(
                play: play,
                audience: performance.Audience
            );
            var playVolumeCredits = _playVolumeCreditsService.GetVolumeCredits(
                play: play,
                audience: performance.Audience
            );

            statement.AddItem(
                name: play.Name,
                amountOwed: playAmount / 100,
                earnedCredits: playVolumeCredits,
                seats: performance.Audience
            );

            totalAmount += playAmount;
            totalVolumeCredits += playVolumeCredits;
        }

        statement.Close(
            amountOwed: totalAmount / 100,
            earnedCredits: totalVolumeCredits
        );

        return new CreateStatementOutput
        {
            Statement = statement,
        };
    }
}