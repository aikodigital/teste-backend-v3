using AutoMapper;

namespace TheatricalPlayersRefactoringKata.Server.Mappers.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapWithPostProcessing<TSource, TDestination>(this IMapper mapper, TSource source, Action<TDestination> postProcessor)
        {
            TDestination destination = mapper.Map<TDestination>(source);
            postProcessor(destination);

            return destination;
        }
    }

}