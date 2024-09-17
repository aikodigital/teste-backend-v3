using MediatR;
using TS.Application.Invoices.Queries.GetAllInvoices.Request;
using TS.Application.Invoices.Queries.GetAllInvoices.Response;
using TS.Domain.Repositories.Invoices;

namespace TS.Application.Invoices.Queries.GetAllInvoices
{
    public class GetAllInvoicesHandler(IInvoicesRepository invoicesRepository) : IRequestHandler<GetAllInvoicesRequest, IEnumerable<GetAllInvoicesResponse>>
    {
        private readonly IInvoicesRepository _invoicesRepository = invoicesRepository;

        public async Task<IEnumerable<GetAllInvoicesResponse>> Handle(GetAllInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoicesRepository.GetAllAsync();
            var responses = invoices.Where(c => string.IsNullOrWhiteSpace(request.Term) ||
                                          c.Id.ToString().Trim().Contains(request.Term, StringComparison.CurrentCultureIgnoreCase)).Select(res => new GetAllInvoicesResponse
                                          {
                                              Id = res.Id,
                                              CreationAt = res.CreationAt,
                                              CustomerId = res.CustomerId
                                          }).ToList();
            return responses;
        }
    }
}