using System.Text.Json;
using MediatR;
using TS.Application.Invoices.Commands.AddInvoices.Request;
using TS.Application.Services;
using TS.Domain.Entities;
using TS.Domain.Repositories.Customers;
using TS.Domain.Repositories.Invoices;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Invoices.Commands.AddInvoices
{
    public class AddInvoicesHandler(IInvoicesRepository invoicesRepository,
                                    ICustomersRepository customersRepository,
                                    IPlaysRepository playsRepository,
                                    IRabbitMQServices rabbitMQServices) : IRequestHandler<AddInvoicesRequest>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;
        private readonly ICustomersRepository _customersRepository = customersRepository;
        private readonly IPlaysRepository _playsRepository = playsRepository;
        private readonly IRabbitMQServices _rabbitMQServices = rabbitMQServices;

        public async Task Handle(AddInvoicesRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.GetAsync(request.CustomerId);
            var plays = await _playsRepository.GetAllAsync();

            if (customer == null || plays == null || !plays.Any())
                throw new Exception("Cliente ou peça não encontrado.");

            var addTo = new Invoice
            {
                CreationAt = DateTime.Now,
                CustomerId = request.CustomerId,
                Customer = customer,
                Seats = request.Seats,
                InvoicesItems = request.Performances.Select(res => new InvoicesItems
                {
                    Performance = new Performance
                    {
                        PlayId = res.PlayId,
                        Play = plays.FirstOrDefault(x => x.Id == res.PlayId)!,
                        Audience = res.Audience
                    }
                }).ToList()
            };

            await _invoicesRepository.CreateAsync(addTo);

            var messageQueue = new MessageQueue
            {
                TypeFile = (int)request.TypeFile,
                InvoiceId = addTo.Id
            };

            _rabbitMQServices.Publisher(messageQueue);
        }
    }
}