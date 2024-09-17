using MediatR;
using TS.Application.Plays.Commands.UpdatePlays.Request;
using TS.Domain.Entities;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Plays.Commands.UpdatePlays
{
    public class UpdatePlaysHandler(IPlaysRepository playsRepository) : IRequestHandler<UpdatePlaysRequest>
    {
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task Handle(UpdatePlaysRequest request, CancellationToken cancellationToken)
        {
            var updateTo = new Play
            {
                Id = request.Id,
                Name = request.Name,
                Type = (int)request.Type,
                Lines = request.Lines
            };

            await _playsRepository.UpdateAsync(updateTo);
        }
    }
}