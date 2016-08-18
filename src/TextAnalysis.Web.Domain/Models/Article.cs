using System;

namespace TextAnalysis.Web.Domain.Models
{
    public class Article 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}