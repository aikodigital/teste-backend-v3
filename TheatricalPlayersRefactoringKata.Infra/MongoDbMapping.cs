using TheatricalPlayersRefactoringKata.Infra.Mapping;

namespace TheatricalPlayersRefactoringKata.Infra
{
    public class MongoDbMapping
    {
        public static void Configure()
        {
            PlayMap.Configure();
        }
    }
}
