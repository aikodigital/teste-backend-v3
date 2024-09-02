using Application.Common.Interfaces;
using Application.Invoices.EventHandlers;
using Domain.Entities;
using MediatR;

namespace Application.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public string? Customer { get; set; }
        public List<Performance> Performances { get; set; } = new List<Performance>();
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Invoice(request.Customer, request.Performances);

            entity.DomainEvents.Add(new InvoiceCreatedEvent(entity));

            _context.Invoices.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            return 1;
        }

        //TODO: implement fluentValidator
    }
}
