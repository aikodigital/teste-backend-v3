using MediatR;
using TS.Application.Invoices.Queries.GetInvoices.Request;
using TS.Application.Invoices.Queries.GetInvoices.Response;
using TS.Domain.Repositories.Invoices;

namespace TS.Application.Invoices.Queries.GetInvoices
{
    public class GetInvoicesHandler(IInvoicesRepository invoicesRepository) : IRequestHandler<GetInvoicesRequest, GetInvoicesResponse>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;

        public async Task<GetInvoicesResponse> Handle(GetInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoicesRepository.GetAsync(request.Id);
            var responses = new GetInvoicesResponse
            {
                Id = invoices!.Id,
                CreationAt = invoices.CreationAt,
                CustomerId = invoices.CustomerId,
            };

            return responses;
        }
    }
}