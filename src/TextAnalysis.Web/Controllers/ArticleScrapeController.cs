using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Collections.Generic;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;
using System;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/articles/scrape", Name="ArticleScrape")]
    public class ArticlesScrapeController : Controller
    {
        private readonly IResourcesRepository<ResourceUrl> _urlRepository;

        private readonly IResourcesRepository<ResourceContent> _contentRepository;

        public ArticlesScrapeController(
                IResourcesRepository<ResourceUrl> urlRepository,
                IResourcesRepository<ResourceContent> contentRepository)
        {
            _urlRepository = urlRepository;
            _contentRepository = contentRepository; 
        }

        [HttpGet]
        public IActionResult Get(string key)
        {
            var resourceUrl = _urlRepository.FilterBy(item => item.Key.Equals(key)).FirstOrDefault();
            if (resourceUrl == null)
            {
                throw new ArgumentException("Resource Url doesn't not exist.");
            }

            var webGet = new HtmlWeb();
            var documentTask = webGet.LoadFromWebAsync(resourceUrl.Url);

            Task.WaitAll(documentTask);

            var result = documentTask.Result;

            var body = result.DocumentNode.Descendants("body").FirstOrDefault();

            var nodesToGetText = body.ChildNodes.SelectMany(node => this.IncludeNode(node));

            return Ok(nodesToGetText.Select(node => node.InnerHtml));
        }

        private IEnumerable<HtmlNode> IncludeNode(HtmlNode node)
        {
            var result = new List<HtmlNode>();

            result.AddRange(node.ChildNodes.SelectMany(n => this.Recursive(n)));

            return result;
        }

        private IEnumerable<HtmlNode> Recursive(HtmlNode node)
        {
            var result = new List<HtmlNode>();

            if (node.ChildNodes.Any(n => n.Name == "p"))
            {
                if (IncludeParagraphNode(node))
                {
                    result.Add(node);
                }
            }
            else if (node.ChildNodes.Any(n => n.Name == "br"))
            {
                if (IncludeTextNode(node))
                {
                    result.Add(node);
                }
            }
            else
            {
                result.AddRange(node.ChildNodes.SelectMany(n => this.Recursive(n)));
            }

            return result;
        }

        private bool IncludeParagraphNode(HtmlNode node)
        {
            if (node.Descendants("p").Any())
            {
                if (node.Descendants("p").First().InnerText.Split(' ').Count() > 20)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IncludeTextNode(HtmlNode node)
        {
            if (node.ChildNodes.Where(cNode => cNode.Name == "br").Count() > 5)
            {
                if (node.InnerText.Split(' ').Count() > 20)
                {
                    return true;
                }
            }

            return false;
        }

    }
}