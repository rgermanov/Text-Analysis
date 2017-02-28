using MongoDB.Bson;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceUrl : IKeyEntity
    {
        public ObjectId Id { get; set; }
        
        public string Key { get; set; }

        public string Url { get; set; }
    }
}