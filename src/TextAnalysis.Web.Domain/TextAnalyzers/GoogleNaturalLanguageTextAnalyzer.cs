using System;
using Google.Cloud.Language.V1;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;

namespace TextAnalysis.Web.Domain.TextAnalyzers
{
    public class GoogleNaturalLanguageTextAnalyzer : ITextAnalyzer
    {
        public TextAnalyzerResult AlayzeHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                throw new ArgumentException(@html);
            }

            var client = LanguageServiceClient.Create();

            var documentToAnalyzer = new Document
            {
                Content = html,
                Type = Document.Types.Type.Html
            };

            var result = client.AnalyzeEntities(documentToAnalyzer);

            return new TextAnalyzerResult
            {
                Language = result.Language,

            };
        }
    }
}