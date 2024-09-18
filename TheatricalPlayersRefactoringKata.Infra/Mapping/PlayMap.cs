using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Mapping
{
    public class PlayMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Play>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true); 
                map.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Name);
                map.MapMember(x => x.Lines);
                map.MapMember(x => x.Type);
            });
        }
    }
}
