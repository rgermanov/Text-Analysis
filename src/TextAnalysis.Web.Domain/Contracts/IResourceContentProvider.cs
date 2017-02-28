using System;

namespace TextAnalysis.Web.Domain.Contracts
{
    public interface IResourceContentProvider
    {
        string Scrape(Uri url);
    }
}