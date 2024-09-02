using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Plays.Commands.CreatePlay
{
    public class CreatePlayCommand : IRequest<int>
    {
        public string Name { get; set; }
        public Types Type { get; set; }
        public int Lines { get; set; }
    }

    public class CreatePlayCommandHandler : IRequestHandler<CreatePlayCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePlayCommand request, CancellationToken cancellationToken)
        {
            var entity = new Play(request.Name, request.Lines, request.Type);

            _context.Plays.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
