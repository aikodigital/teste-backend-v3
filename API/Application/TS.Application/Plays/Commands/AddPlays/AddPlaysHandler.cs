using MediatR;
using TS.Application.Plays.Commands.AddPlays.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Plays.Commands.AddPlays
{
    public class AddPlaysHandler(IPlaysRepository playsRepository) : IRequestHandler<AddPlaysRequest>
    {
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task Handle(AddPlaysRequest request, CancellationToken cancellationToken)
        {
            var addTo = new Play
            {
                Id = request.Id,
                Name = request.Name,
                Type = (int)request.Type,
                Lines = request.Lines
            };

            await _playsRepository.CreateAsync(addTo);
        }
    }
}