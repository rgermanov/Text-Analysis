using System;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceFetch : IEntity
    {
        public string Id { get; set; }
        public string Key { get; set; }

        public DateTime FetchDate { get; set; }
    }
}