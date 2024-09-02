using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plays.Commands.UpdatePlay
{
    public class UpdatePlayCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Lines { get; set; }
        public Types Type { get; set; }
    }

    public class UpdatePlayCommandHandler : IRequestHandler<UpdatePlayCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePlayCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<UpdatePlayCommand>.Handle(UpdatePlayCommand request, CancellationToken cancellationToken)
        {
            var play = await _context.Plays
                .FindAsync(request.Id);

            if (play == null)
            {
                throw new NotFoundException(nameof(Play), request.Id);
            }

            play.Name = request.Name;
            play.Lines = request.Lines;
            play.Type = request.Type;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
