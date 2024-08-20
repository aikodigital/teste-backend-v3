using AutoMapper;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Performance;

namespace TheatricalPlayersRefactoringKata.Server.Mappers
{
    public class PerformanceMappingProfile : Profile
    {
        public PerformanceMappingProfile()
        {
            // <PerformanceDTO to Performance>
            CreateMap<PerformanceDTO, Performance>();
        }
    }
}