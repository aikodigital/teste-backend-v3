using MediatR;
using TS.Application.Plays.Queries.GetAllPlays.Request;
using TS.Application.Plays.Queries.GetAllPlays.Response;
using TS.Domain.Repositories.Plays;

namespace TS.Application.Plays.Queries.GetAllPlays
{
    public class GetAllPlaysHandler(IPlaysRepository playsRepository) : IRequestHandler<GetAllPlaysRequest, IEnumerable<GetAllPlaysResponse>>
    {
        private readonly IPlaysRepository _playsRepository = playsRepository;

        public async Task<IEnumerable<GetAllPlaysResponse>> Handle(GetAllPlaysRequest request, CancellationToken cancellationToken)
        {
            var plays = await _playsRepository.GetAllAsync();
            var responses = plays.Where(c => string.IsNullOrWhiteSpace(request.Term) ||
                                          c.Id.ToString().Trim().Contains(request.Term, StringComparison.CurrentCultureIgnoreCase)).Select(res => new GetAllPlaysResponse
                                          {
                                              Id = res.Id,
                                              Name = res.Name,
                                              Type = (Domain.Enums.ETypePLay)res.Type,
                                              Lines = res.Lines
                                          }).ToList();
            return responses;
        }
    }
}