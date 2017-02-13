using MongoDB.Bson;

namespace TextAnalysis.Web.Domain.Contracts 
{
    public interface IEntity
    {
        ObjectId Id { get; set; }
    }
}