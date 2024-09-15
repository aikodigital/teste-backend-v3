using MediatR;
using TS.Application.Invoices.Queries.GetAllInvoices.Response;

namespace TS.Application.Invoices.Queries.GetAllInvoices.Request
{
    public class GetAllInvoicesRequest : IRequest<IEnumerable<GetAllInvoicesResponse>>
    {
        public string? Term { get; set; }
    }
}