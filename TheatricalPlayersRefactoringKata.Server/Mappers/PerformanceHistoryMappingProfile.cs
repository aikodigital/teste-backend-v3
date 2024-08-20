using AutoMapper;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.PerformanceHistory;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.PerformanceHistory;

namespace TheatricalPlayersRefactoringKata.Server.Mappers
{
    public class PerformanceHistoryMappingProfile : Profile
    {
        public PerformanceHistoryMappingProfile()
        {
            // <PerformanceHistoryEntity to PerformanceHistoryDTO>
            CreateMap<PerformanceHistoryEntity, PerformanceHistoryDTO>();

            // <Performance to PerformanceHistoryEntity>
            CreateMap<Performance, PerformanceHistoryEntity>()
                .ConstructUsing(source => new PerformanceHistoryEntity
                {
                    PlayId = source.PlayId,
                    Audience = source.Audience,
                    AmountOwed = source.Results != null ? source.Results.AmountOwed : 0,
                    EarnedCredits = source.Results != null ? source.Results.EarnedCredits : 0
                });
        }
    }
}