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
                invoice.Amount += _calculateService.CalculateValueByType(play.PlayType, performance.Audience);
                invoice.Credits += _calculateService.CalculateCreditsByType(play.PlayType, performance.Audience);
            }

            invoice = _uow.InvoiceRepository.Add(invoice);

            return invoice;
        }
    }
}