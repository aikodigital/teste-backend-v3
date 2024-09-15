using MediatR;
using TS.Application.Invoices.Commands.AddInvoices.Request;
using TS.Application.Services;
using TS.Domain.Entities;
using TS.Domain.Repositories.Customers;
using TS.Domain.Repositories.Invoices;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Invoices.Commands.AddInvoices
{
    public class AddInvoicesHandler(IInvoicesRepository invoicesRepository, ICustomersRepository customersRepository, IPlaysRepository playsRepository, IRabbitMQServices rabbitMQServices) : IRequestHandler<AddInvoicesRequest>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;
        private readonly ICustomersRepository _customersRepository = customersRepository;
        private readonly IPlaysRepository _playsRepository = playsRepository;
        private readonly IRabbitMQServices _rabbitMQServices = rabbitMQServices;

        public async Task Handle(AddInvoicesRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.GetAsync(request.CustomerId);
            var play = await _playsRepository.GetAsync(request.PlayId);

            if (customer != null && play != null)
            {
                var addTo = new Invoice
                {
                    CreationAt = DateTime.Now,
                    CustomerId = request.CustomerId,
                    PlayId = request.PlayId,
                    LoyaltyCredit = request.LoyaltyCredit,
                    Customer = customer,
                    Play = play
                };

                await _invoicesRepository.CreateAsync(addTo);

                var message = new
                {
                    InvoiceId = addTo.Id
                };

                _rabbitMQServices.Publisher(message.ToString()!);
            }
        }
    }
}