using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using TextAnalysis.Web.Domain.Models;

namespace TextAnalysis.Web.Domain.Providers
{
    public class MongoMappingsProvider
    {
        public static void Register()
        {
            // BsonClassMap.RegisterClassMap<ResourceUrl>(cm => {
            //     cm.AutoMap();
            //     cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            // });
        }
    }
}