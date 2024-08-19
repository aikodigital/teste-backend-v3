using AutoMapper;

using TheatricalPlayersRefactoringKata.Modules;
using TheatricalPlayersRefactoringKata.Server.Database.DTOs.Play;
using TheatricalPlayersRefactoringKata.Server.Database.Repositories.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Server.Mappers
{
    public class PlayTypeResolver : IValueResolver<PlayEntity, Play, AbstractPlayType>
    {
        public AbstractPlayType Resolve(PlayEntity source, Play destination, AbstractPlayType destMember, ResolutionContext context)
        {
            return AbstractPlayType.FromString(source.Type);
        }
    }

    public class PlayMappingProfile : Profile
    {
        public PlayMappingProfile()
        {
            // <PlayDTO to PlayEntity>
            CreateMap<PlayDTO, PlayEntity>()
                .ForMember(destination => destination.Type, options => options.MapFrom(source => source.Type.ToString()));

            // <PlayEntity to Play>
            CreateMap<PlayEntity, Play>()
                .ForMember(destination => destination.Type, options => options.MapFrom<PlayTypeResolver>())
                .ConstructUsing(source => new Play(
                    source.Name,
                    source.Lines,
                    AbstractPlayType.FromString(source.Type)));
        }
    }
}