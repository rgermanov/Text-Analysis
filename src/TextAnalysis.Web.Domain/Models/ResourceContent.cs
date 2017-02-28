using MongoDB.Bson;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceContent : IKeyEntity
    {
        public ObjectId Id { get; set; }
        public string Key { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}