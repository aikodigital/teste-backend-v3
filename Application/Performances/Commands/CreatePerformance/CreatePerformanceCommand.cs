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

namespace Application.Performances.Commands.CreatePerformance
{
    public class CreatePerformanceCommand : IRequest<int>
    {
        public string PlayId;
        public int Audience;
        public int NumberOfLines;
    }

    public class CreatePerformanceCommandHandler : IRequestHandler<CreatePerformanceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreatePerformanceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreatePerformanceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Performance(request.PlayId, request.Audience);

            var play = await _context.Plays
                .FindAsync(request.PlayId);

            if (play == null)
            {
                throw new NotFoundException(nameof(Play), request.PlayId);
            }
            
            var value = CalculateValue(request.NumberOfLines, request.Audience, play.Type);
            var credits = CalculateCredits(request.Audience, play.Type);

            entity.Value = value;
            entity.Credits = credits;

            _context.Performances.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public decimal CalculateValue(int numLines, int audience, Types type)
        {
            int adjustedLines = Math.Clamp(numLines, 1000, 4000);
            var value = adjustedLines / 10m;
            switch (type)
            {
                case Types.Tragedy:
                    if (audience > 30)
                    {
                        value += (audience - 30) * 10m;
                    }
                    break;

                case Types.Comedy:
                    value += audience * 3m;
                    if (audience > 20)
                    {
                        value += 100m + (audience - 20) * 5m;
                    }
                    break;
                case Types.History:
                    var tragedyValue = CalculateValue(numLines, audience, Types.Tragedy);
                    var comedyValue = CalculateValue(numLines, audience, Types.Comedy);

                    value = tragedyValue + comedyValue;
                    break;
            }

            return value;
        }

        public int CalculateCredits(int audience, Types type)
        {
            int credits = 0;

            if (audience > 30)
            {
                credits += audience - 30;
            }

            if (type == Types.Comedy)
            {
                credits += (int)Math.Floor(audience / 5m);
            }

            return credits;
        }
    }
}
