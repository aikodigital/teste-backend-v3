using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Repositories;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

namespace TheatricalPlayersRefactoringKata.Service.Services;

public class StatementPrinterService : IStatementPrinterService
{
    private readonly ITypeGenreRepository _typeGenreRepository;

    public StatementPrinterService(ITypeGenreRepository typeGenreRepository)
    {
        _typeGenreRepository = typeGenreRepository;
    }

    public async Task<string> Print(Invoice invoice)
    {
        int totalAmount = 0;
        int volumeCredits = 0;
        string result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new("en-US");
       
        foreach (var perf in invoice.Performances)
        {
            Play play = perf.Play;
            int lines = play.Lines;
            if (lines < 1000) lines = 1000;
            if (lines > 4000) lines = 4000;

            TypeGenre typeGenre = perf.Play.TypeGenre;

            int thisAmount = await Calculate(play, perf, typeGenre, lines);

            volumeCredits += CalculateCredits(perf, typeGenre, volumeCredits);

            result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount) / 100, perf.Audience);
            totalAmount += thisAmount;
        }

        result += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount) / 100);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        return result;
    }

    public async Task<int> Calculate(Play play, Performance perf, TypeGenre genre, int lines)
    {
        var thisAmount = lines * perf.Play.TypeGenre.BasePriceMultiplier;
   
        switch (genre.Name)
        {   
            case "tragedy":
                thisAmount = CalculateTragedy(play, perf, genre, thisAmount);
                break;
            case "comedy":
                thisAmount = CalculateComedy(perf, genre, thisAmount);
                break;
            case "history":
                thisAmount = await CalculateHistorical(play, perf, thisAmount);
                break;
            default:
                throw new Exception("unknown type: " + genre.Name);
        }

        return thisAmount;
    }

    private static int CalculateCredits(Performance perf, TypeGenre genre, int volumeCredits)
    {
        volumeCredits = Math.Max(perf.Audience - 30, 0);
        if (genre.Name == "comedy") volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

        return volumeCredits;
    }

    private static int CalculateTragedy(Play play, Performance perf, TypeGenre genre, int thisAmount)
    {
        if (perf.Audience > 30)
        {
            thisAmount += genre.ExtraFeePerAudience.Value * (perf.Audience - genre.MaxAudience.Value);
        }

        return thisAmount;
    }

    private static int CalculateComedy(Performance perf, TypeGenre genre, int thisAmount)
    {
        thisAmount += genre.BaseFeePerAudience.Value * perf.Audience;
        if (perf.Audience > genre.MaxAudience.Value)
        {
            thisAmount += genre.BonusFee.Value + (genre.ExtraFeePerAudience.Value * (perf.Audience - genre.MaxAudience.Value));
        }

        return thisAmount;
    }

    private async Task<int> CalculateHistorical(Play play, Performance perf, int thisAmount)
    {
        var genreTragedy = (await _typeGenreRepository.GetByFilter(x => x.Name.Equals("tragedy"))).FirstOrDefault();
        var genreComedy = (await _typeGenreRepository.GetByFilter(x => x.Name.Equals("comedy"))).FirstOrDefault();

        var tragedyAmount = CalculateTragedy(play, perf, genreTragedy, thisAmount);
        var comedyAmount = CalculateComedy(perf, genreComedy, thisAmount);

        return tragedyAmount + comedyAmount;
    }
}