using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IRepositories;
using TheatricalPlayersRefactoringKata.Domain.Utils;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories {
    public class StatementPrinterRepositoryImpl : IStatementPrinterRepository {


        // adicionar context do banco de dados para persistência dos dados no banco

        private readonly Dictionary<Enum, IGenreStrategy> _genres;
        private readonly CultureInfo _cultureInfo;

        public StatementPrinterRepositoryImpl(Dictionary<Enum, IGenreStrategy> genres, CultureInfo cultureInfo) {
            _genres = genres ?? throw new ArgumentNullException(nameof(genres));
            _cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
        }

        public string Print(Invoice invoice, Dictionary<string, Play> plays) {

            if (invoice == null) {
                throw new ArgumentNullException(nameof(invoice));
            }

            if (plays == null) {
                throw new ArgumentNullException(nameof(plays));
            }

            double totalAmount = 0;
            double volumeCredits = 0;
            string result = string.Format("Statement for {0}\n", invoice.Customer);

            foreach (var perf in invoice.Performances) {
                Play play = plays[perf.PlayId];

                if (!plays.TryGetValue(perf.PlayId, out play)) {
                    throw new KeyNotFoundException($"Play with ID {perf.PlayId} not found.");
                }

                if (!_genres.TryGetValue(play.Type, out var genre)) {
                    throw new KeyNotFoundException($"Genre strategy for {play.Type} not found.");
                }

                double thisAmount = PlayCalculationUtils.CalculatePlayLines(perf, play);

                thisAmount = genre.CalculatePlayAmount(perf);
                volumeCredits += genre.CalculatePlayCredits(perf);

                // print line for this order
                result += string.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
                totalAmount += thisAmount;
            }
            result += string.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += string.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

    }
}
