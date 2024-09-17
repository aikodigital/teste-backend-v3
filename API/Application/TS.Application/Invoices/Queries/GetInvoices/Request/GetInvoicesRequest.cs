using MediatR;
using TS.Application.Invoices.Queries.GetInvoices.Response;

namespace TS.Application.Invoices.Queries.GetInvoices.Request
{
    public class GetInvoicesRequest : IRequest<GetInvoicesResponse>
    {
        public long Id { get; set; }
    }
}