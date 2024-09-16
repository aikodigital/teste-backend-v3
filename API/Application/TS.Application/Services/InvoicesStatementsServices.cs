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
                }

                response.Items.Add(new DatasItemsResponse
                {
                    AmountOwed = value,
                    Seats = 0,
                    EarnedCredits = Convert.ToDecimal(creditCustomer)
                });
            }

            return response;
        }

        public async Task TXT(long invoiceId)
        {
            var invoice = await Invoices(invoiceId);
        }

        public async Task XML(long invoiceId)
        {
            var invoice = await Invoices(invoiceId);
        }
    }

    public class DatasToPrintResponse
    {
        public string Customers { get; set; } = string.Empty;
        public List<DatasItemsResponse> Items { get; set; } = [];
    }

    public class DatasItemsResponse
    {
        public decimal AmountOwed { get; set; }
        public decimal Seats { get; set; }
        public decimal EarnedCredits { get; set; }
    }
}