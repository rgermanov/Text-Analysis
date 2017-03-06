using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;
using System;
using TextAnalysis.Web.Models;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/articles/scrape", Name="ArticleScrape")]
    public class ArticlesScrapeController : Controller
    {
        private readonly IResourcesRepository<ResourceUrl> _urlRepository;
        private readonly IResourcesRepository<ResourceContent> _contentRepository;
        private readonly IResourceContentProvider _resourceContentProvider;
        private readonly ITextAnalyzer _textAnalyzer;
        

        public ArticlesScrapeController(
                IResourcesRepository<ResourceUrl> urlRepository,
                IResourcesRepository<ResourceContent> contentRepository,
                IResourceContentProvider resourceContentProvider,
                ITextAnalyzer textAnalyzer)
        {
            _urlRepository = urlRepository;
            _contentRepository = contentRepository; 
            _resourceContentProvider = resourceContentProvider;
            _textAnalyzer = textAnalyzer;
        }

        [HttpGet]
        [Route("{key}", Name="ArticleScrapeGet")]
        public IActionResult Get(string key)
        {
            var resourceUrl = _urlRepository.FilterBy(item => item.Key.Equals(key)).FirstOrDefault();
            if (resourceUrl == null)
            {
                throw new ArgumentException("Resource Url doesn't not exist.");
            }

            var contentHtml = _resourceContentProvider.Scrape(new Uri(resourceUrl.Url));

            var resourceContent = new ResourceContent()
            {
                Key = key,
                Text = contentHtml
            };

            _contentRepository.Add(resourceContent);

            var analysisResult = _textAnalyzer.AlayzeHtml(resourceContent.Text);

            return Ok(new { Content = AutoMapper.Mapper.Map<ArticleContentModel>(resourceContent), Analysis = analysisResult });
        }
    }    
}