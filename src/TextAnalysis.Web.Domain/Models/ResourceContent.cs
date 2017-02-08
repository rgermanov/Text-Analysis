using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Models
{
    public class ResourceContent : IEntity
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}