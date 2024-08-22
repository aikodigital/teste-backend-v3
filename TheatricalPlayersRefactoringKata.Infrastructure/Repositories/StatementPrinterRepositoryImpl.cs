using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Enums;
using TheatricalPlayersRefactoringKata.Domain.Utils;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories {
    public class StatementPrinterRepositoryImpl : IStatementPrinterRepository {


        // adicionar context do banco de dados para persistência dos dados no banco

        private readonly Dictionary<Enum, IGenreStrategy> _genres;
        private readonly CultureInfo _cultureInfo;

        public StatementPrinterRepositoryImpl(Dictionary<Enum, IGenreStrategy> genres, CultureInfo cultureInfo) {
            _genres = genres ?? [];
            _cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
        }

        public Result<string> Print(Invoice invoice, Dictionary<string, Play> plays) {

            if (invoice == null) {
                return Result<string>.Failure(Error.Validation("Invoice data is null", ErrorType.Validation.ToString()));
            }

            if (plays == null && plays!.Count == 0) {
                return Result<string>.Failure(Error.Validation("Plays data is null", ErrorType.Validation.ToString()));
            }

            double totalAmount = 0;
            double volumeCredits = 0;
            string result = string.Format("Statement for {0}\n", invoice.Customer);

            foreach (var perf in invoice.Performances) {
                Play play = plays[perf.PlayId];

                if (!plays.TryGetValue(perf.PlayId, out play!)) {
                    Result<string>.Failure(Error.NotFound($"Play with ID {perf.PlayId} not found.", ErrorType.NotFound.ToString()));
                }

                if (!_genres.TryGetValue(play.Type, out var genre)) {
                    Result<string>.Failure(Error.NotFound($"Genre strategy for {play.Type} not found.", ErrorType.NotFound.ToString()));
                }

                double thisAmount = PlayCalculationUtils.CalculatePlayLines(perf, play);

                thisAmount = genre!.CalculatePlayAmount(perf, thisAmount);
                volumeCredits += genre.CalculatePlayCredits(perf);

                // print line for this order
                result += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += string.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += string.Format("You earned {0} credits\n", volumeCredits);
            return Result<string>.Success(result);
            ;
        }

    }
}
