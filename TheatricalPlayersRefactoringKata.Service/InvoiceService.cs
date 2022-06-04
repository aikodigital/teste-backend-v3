using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Interface.Services;
using TheatricalPlayersRefactoringKata.Domain.Interface.UoW;
using TheatricalPlayersRefactoringKata.Domain.Model.Entity;

namespace TheatricalPlayersRefactoringKata.Domain.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICalculateService _calculateService;

        public InvoiceService(IUnitOfWork uow,
            ICalculateService calculateService)
        {
            _uow = uow;
            _calculateService = calculateService;
        }

        public async Task<Invoice> Invoice(long customerId, List<Performance> performances, List<Play> plays)
        {
            Invoice invoice = new Invoice();
            invoice.Id = 0;
            invoice.Active = true;
            invoice.CreationDate = DateTime.Now;
            invoice.LastModifiedDate = DateTime.Now;
            invoice.CustomerId = customerId;
            invoice.Performances = performances;

            foreach (Performance performance in performances)
            {
                Play play = plays.First(x => x.Id == performance.PlayId);
                performance.AmountOwed = _calculateService.CalculateValueByType(play.PlayType, performance.Audience, play.Lines);
                performance.EarnedCredits = _calculateService.CalculateCreditsByType(play.PlayType, performance.Audience);

                invoice.TotalAmount += performance.AmountOwed;
                invoice.TotalCredits += performance.EarnedCredits;
            }

            invoice = _uow.InvoiceRepository.Add(invoice);

            return invoice;
        }

        public string GenerateXML(Invoice invoice)
        {
            XElement statement = new XElement("Statement");
            CultureInfo culture = new CultureInfo("en-US");

            XElement customer = new XElement("Customer", invoice.Customer.Name);
            XElement amount = new XElement("AmountOwed", invoice.TotalAmount.ToString("N2", culture));
            XElement credits = new XElement("EarnedCredits", invoice.TotalCredits);
            XElement items = new XElement("Items");
            foreach (Performance performance in invoice.Performances)
            {
                XElement item = new XElement("Item",
                                             new XElement("AmountOwed", performance.AmountOwed.ToString("N2", culture)),
                                             new XElement("EarnedCredits", performance.EarnedCredits),
                                             new XElement("Seats", performance.Audience));
                items.Add(item);
            }

            statement.Add(customer);
            statement.Add(items);
            statement.Add(amount);
            statement.Add(credits);

            string result = statement.ToString();
            return result;
        }

        public string GenerateText(Invoice invoice)
        {
            StringBuilder text = new StringBuilder($"Statement for {invoice.Customer.Name}");
            CultureInfo culture = new CultureInfo("en-US");

            foreach (Performance performance in invoice.Performances)
            {
                text.AppendLine($"\n\t{performance.Play.Name}: ${performance.AmountOwed.ToString("N2", culture)} ({performance.EarnedCredits} seats)");
            }

            text.AppendLine($"\nAmount owed is ${invoice.TotalAmount.ToString("N2", culture)}");
            text.AppendLine($"\nYou earned ${invoice.TotalCredits} credits");

            string result = text.ToString();
            return result;
        }

        public string GenerateJson(Invoice invoice)
        {
            string json = JsonConvert.SerializeObject(invoice);
            return json;
        }
    }
}