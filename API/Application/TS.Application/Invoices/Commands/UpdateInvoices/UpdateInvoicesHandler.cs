using MediatR;
using TS.Application.Invoices.Commands.UpdateInvoices.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Invoices;

namespace TS.Application.Invoices.Commands.UpdateInvoices
{
    public class UpdateInvoicesHandler(IInvoicesRepository invoicesRepository) : IRequestHandler<UpdateInvoicesRequest>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;

        public async Task Handle(UpdateInvoicesRequest request, CancellationToken cancellationToken)
        {
            var updateTo = new Invoice
            {
                Id = request.Id,
                CreationAt = request.CreationAt,
                CustomerId = request.CustomerId,
                PlayId = request.PlayId,
                LoyaltyCredit = request.LoyaltyCredit
            };

            await _invoicesRepository.UpdateAsync(updateTo);
        }
    }
}