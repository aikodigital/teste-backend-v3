using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<DeleteInvoiceCommand>.Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Invoices
               .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Invoice), request.Id);
            }

            _context.Invoices.Remove(entity);

            //entity.DomainEvents.Add(new TodoItemDeletedEvent(entity)); TODO

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
