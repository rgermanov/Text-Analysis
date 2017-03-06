using TextAnalysis.Web.Domain.Models;

namespace TextAnalysis.Web.Domain.Contracts
{
    public interface ITextAnalyzer
    {
        TextAnalyzerResult AlayzeHtml(string html);
    }
}
