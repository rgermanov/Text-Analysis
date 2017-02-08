using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceUrl : IEntity
    {
        public string Key { get; set; }

        public string Url { get; set; }
    }
}