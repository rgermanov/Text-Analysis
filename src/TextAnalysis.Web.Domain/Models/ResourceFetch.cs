using System;
using MongoDB.Bson;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceFetch : IKeyEntity
    {
        public ObjectId Id { get; set; }
        public string Key { get; set; }

        public DateTime FetchDate { get; set; }
    }
}