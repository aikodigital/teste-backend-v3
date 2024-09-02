using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plays.Commands.DeletePlay
{
    public class DeletePlayCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeletePlayCommandHandler : IRequestHandler<DeletePlayCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeletePlayCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeletePlayCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Plays.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Play), request.Id);
            }

            _context.Plays.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
