using System.Xml;
using System.Xml.Linq;
using TS.Domain.Enums;
using TS.Domain.Repositories.Customers;
using TS.Domain.Repositories.Invoices;
using TS.Domain.Repositories.Performances;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Services
{
    public class InvoicesStatementsServices(IInvoicesRepository invoicesRepository,
                                            ICustomersRepository customersRepository,
                                            IPerformancesRepository performancesRepository,
                                            IPlaysRepository playsRepository) : IInvoicesStatementsServices
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;
        private readonly ICustomersRepository _customersRepository = customersRepository;
        private readonly IPerformancesRepository _performancesRepository = performancesRepository;
        private readonly IPlaysRepository _playsRepository = playsRepository;

        private async Task<DatasToPrintResponse?> Invoices(long invoiceId)
        {
            var response = new DatasToPrintResponse();
            var invoices = await _invoicesRepository.GetAsync(invoiceId);
            if (invoices == null)
                return null;

            var customers = await _customersRepository.GetAsync(invoices.CustomerId);
            if (customers == null)
                return null;

            var performances = await _performancesRepository.GetAllAsync();
            var plays = await _playsRepository.GetAllAsync();

            response.Customers = customers.Name;

            foreach (var invoiceItem in invoices.InvoicesItems)
            {
                var performance = performances.FirstOrDefault(p => p.Id == invoiceItem.Id);
                if (performance == null)
                    continue;

                var play = plays.FirstOrDefault(p => p.Id == performance.PlayId);
                if (performance == null)
                    continue;

                var baseValue = play!.Lines / 10;
                var value = 0;
                var diff = 0;
                var creditCustomer = 0.0;

                if (performance.Audience > 30)
                    creditCustomer = 1;

                switch ((ETypePLay)play!.Type)
                {
                    case ETypePLay.Comedy:
                        baseValue += performance.Audience * 3;
                        if (performance.Audience > 20)
                        {
                            value = baseValue + 100;
                            diff = performance.Audience - 20;
                            value += diff * 5;
                        }

                        if (performance.Audience > 30)
                            creditCustomer += Math.Floor(performance.Audience * 0.2);


                        break;
                    case ETypePLay.Tragedy:
                        if (performance.Audience <= 30)
                            value = baseValue;
                        else
                        {
                            diff = performance.Audience - 30;
                            value = (diff * 10) + baseValue;
                        }
                        break;
                    case ETypePLay.History:
                        baseValue += performance.Audience * 3;
                        if (performance.Audience > 20)
                        {
                            value = baseValue + 100;
                            diff = performance.Audience - 20;
                            value += diff * 5;
                        }

                        baseValue = play!.Lines / 10;
                        var valueHistory = value;

                        if (performance.Audience <= 30)
                            value = baseValue;
                        else
                        {
                            diff = performance.Audience - 30;
                            value = (diff * 10) + baseValue;
                        }
                        value += valueHistory;
                        break;
                    default:
                        break;
                }

                response.Items.Add(new DatasItemsResponse
                {
                    PerformanceName = play.Name.Trim(),
                    AmountOwed = value,
                    Seats = 0,
                    EarnedCredits = Convert.ToDecimal(creditCustomer)
                });
            }

            response.TotalAmoutOwed = response.Items.Sum(x => x.AmountOwed);
            response.TotalEarnedCredits = response.Items.Sum(x => x.EarnedCredits);

            return response;
        }

        private async Task TXT(long invoiceId)
        {
            var invoice = await Invoices(invoiceId);

            if (invoice == null)
                return;

            var lines = new List<string>
            {
                $"Statement for {invoice.Customers}"
            };

            foreach (var item in invoice.Items)
                lines.Add($" {item.PerformanceName}: ${item.AmountOwed} ({item.Seats} seats)");

            lines.Add($"Amount owed is ${invoice.TotalAmoutOwed}");
            lines.Add($"You earned {invoice.TotalEarnedCredits} credits");

            var fileName = Path.Combine(GetPath(), $"{DateTime.Now:ddMMyyyyHHmmss}.received.txt");
            using StreamWriter writer = new(fileName);

            foreach (string line in lines)
                writer.WriteLine(line);
        }

        private async Task XML(long invoiceId)
        {
            var invoice = await Invoices(invoiceId);

            if (invoice == null)
                return;

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            var xml = new XDocument(new XElement("Statement",
                                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                                    new XElement("Items"),
                                        invoice!.Items.Select(p => new XElement("Item",
                                                                  new XElement("AmountOwed", p.AmountOwed),
                                                                  new XElement("EarnedCredits", p.EarnedCredits),
                                                                  new XElement("Seats", p.Seats))),
                                    new XElement("AmountOwed", invoice.TotalAmoutOwed),
                                    new XElement("EarnedCredits", invoice.TotalEarnedCredits)));



            var fileName = Path.Combine(GetPath(), $"{DateTime.Now:ddMMyyyyHHmmss}.received.xml");
            xml.Save(fileName);
        }

        private static string GetPath()
        {
            var path = Path.Combine("C:/", "Receiveds");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }

        public async Task GenerateFile(ETypeFile typeFile,
                                       long invoiceId)
        {
            switch (typeFile)
            {
                case ETypeFile.TXT:
                    await TXT(invoiceId);
                    break;
                case ETypeFile.XML:
                    await XML(invoiceId);
                    break;
                default:
                    break;
            }
        }
    }

    public class DatasToPrintResponse
    {
        public string Customers { get; set; } = string.Empty;
        public List<DatasItemsResponse> Items { get; set; } = [];
        public decimal TotalAmoutOwed { get; set; } = 0;
        public decimal TotalEarnedCredits { get; set; } = 0;
    }

    public class DatasItemsResponse
    {
        public string PerformanceName { get; set; } = string.Empty;
        public decimal AmountOwed { get; set; }
        public decimal Seats { get; set; }
        public decimal EarnedCredits { get; set; }
    }
}