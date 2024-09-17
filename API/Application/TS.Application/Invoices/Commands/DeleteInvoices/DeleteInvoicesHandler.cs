using MediatR;
using TS.Application.Invoices.Commands.DeleteInvoices.Request;
using TS.Domain.Repositories.Invoices;

namespace TS.Application.Invoices.Commands.DeleteInvoices
{
    public class DeleteInvoicesHandler(IInvoicesRepository invoicesRepository) : IRequestHandler<DeleteInvoicesRequest>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;

        public async Task Handle(DeleteInvoicesRequest request, CancellationToken cancellationToken)
        {
            await _invoicesRepository.DeleteAsync(request.Id);
        }
    }
}