using System.Collections.Generic;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Services.PlayTypeCalculators;

namespace TheatricalPlayersRefactoringKata.Application.UseCases;

public class GenerateStatementUseCase 
{
    private readonly IPlayRepository _playRepository;
    private readonly ExtractService _extractService;

    public GenerateStatementUseCase(IPlayRepository playRepository, ExtractService extractService)
    {
        _playRepository = playRepository;
        _extractService = extractService;
    }

    public StatementResult GenerateExtractValues(Invoice invoice)
    {
        decimal totalAmount = 0;
        int totalVolumeCredits = 0;
        var lines = new List<StatementLine>();

        foreach (var performance in invoice.Performances)
        {
            var play = _playRepository.GetPlayById(performance.PlayId);
            performance.Play = play;
            decimal thisAmount = CalculatePerformanceAmount(performance);

            var calculator = PlayTypeCalculatorFactory.GetCalculator(play.Type);

            if (calculator is HistoryCalculator historyCalculator)
            {
                thisAmount = historyCalculator.CalculateAmount(performance, thisAmount);
            }
            else
            {
                thisAmount += calculator.CalculateAmount(performance);
            }

            var volumeCredits = calculator.CalculateVolumeCredits(performance);

            totalAmount += thisAmount;
            totalVolumeCredits += volumeCredits;

            lines.Add(new StatementLine
            {
                PlayName = play.Name,
                Audience = performance.Audience,
                Amount = thisAmount,
                VolumeCredits = volumeCredits
            });
        }

        var extract = new StatementResult
        {
            Customer = invoice.Customer,
            Lines = lines,
            TotalAmount = totalAmount,
            TotalVolumeCredits = totalVolumeCredits
        };

        return extract;
    }

    public decimal CalculatePerformanceAmount(Performance performance)
    {
        int lines = performance.Play.Lines;
        if (lines < 1000) lines = 1000;
        if (lines > 4000) lines = 4000;
        return lines * 10;
    }
}

