using System.Globalization;
using System.Text;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class ExtractTextService : ExtractService, IExtractTextService
    {
        public ExtractTextService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override string GenerateExtract(Invoice invoice)
        {
            StringBuilder text = new StringBuilder($"Statement for {invoice.Customer.Name}");
            CultureInfo culture = new CultureInfo("en-US");

            foreach (Performance performance in invoice.Performances)
            {
                text.AppendLine($"\n\t{performance.Play.Name}: ${performance.AmountOwed.ToString("N2", culture)} ({performance.EarnedCredits} seats)");
            }

            text.AppendLine($"\nAmount owed is ${invoice.TotalAmount.ToString("N2", culture)}");
            text.AppendLine($"\nYou earned {invoice.TotalCredits} credits");

            string result = text.ToString();
            return result;
        }
    }
}