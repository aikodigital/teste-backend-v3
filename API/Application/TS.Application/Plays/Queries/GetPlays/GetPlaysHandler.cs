using MediatR;
using TS.Application.Plays.Queries.GetPlays.Request;
using TS.Application.Plays.Queries.GetPlays.Response;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Plays.Queries.GetPlays
{
    public class GetPlaysHandler(IPlaysRepository playsRepository) : IRequestHandler<GetPlaysRequest, GetPlaysResponse>
    {
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task<GetPlaysResponse> Handle(GetPlaysRequest request, CancellationToken cancellationToken)
        {
            var plays = await _playsRepository.GetAsync(request.Id);
            var responses = new GetPlaysResponse
            {
                Id = plays!.Id,
                Name = plays.Name,
                Type = (Domain.Enums.ETypePLay)plays.Type,
                Lines = plays.Lines,
            };

            return responses;
        }
    }
}