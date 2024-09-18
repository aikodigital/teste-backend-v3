using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Infra.Mapping
{
    internal class InvoiceMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Invoice>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdProperty(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetSerializer(new StringSerializer(BsonType.ObjectId));
                map.MapIdMember(x => x.Id);
                map.MapMember(x => x.Customer);
                map.MapMember(x => x.Performances);

                BsonClassMap.RegisterClassMap<Performance>(map =>
                {
                    map.AutoMap();
                    map.SetIgnoreExtraElements(true);
                    map.MapMember(x => x.PlayId);
                    map.MapMember(x => x.Audience);
                });
            });
        }
    }
}
