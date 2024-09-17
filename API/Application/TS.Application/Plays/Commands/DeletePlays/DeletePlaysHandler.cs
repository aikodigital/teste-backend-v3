using MediatR;
using TS.Application.Plays.Commands.DeletePlays.Request;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Plays.Commands.DeletePlays
{
    public class DeletePlaysHandler(IPlaysRepository playsRepository) : IRequestHandler<DeletePlaysRequest>
    {
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task Handle(DeletePlaysRequest request, CancellationToken cancellationToken)
        {
            await _playsRepository.DeleteAsync(request.Id);
        }
    }
}