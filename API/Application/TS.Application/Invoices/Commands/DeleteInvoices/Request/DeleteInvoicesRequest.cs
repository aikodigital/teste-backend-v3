using MediatR;

namespace TS.Application.Invoices.Commands.DeleteInvoices.Request
{
    public class DeleteInvoicesRequest : IRequest
    {
        public long Id { get; set; }
    }
}