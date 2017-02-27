using System.Collections.Generic;

namespace TextAnalysis.Web.Models
{
    public class ArticleModel
    {
        public string Key { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> References { get; set; }
        public ArticleModel()
        {
            this.References = new Dictionary<string, string>();
        }
    }
}