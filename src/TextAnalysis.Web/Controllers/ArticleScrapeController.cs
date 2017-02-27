using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/articles/scrape")]
    public class ArticlesScrapeController : Controller
    {
        [HttpGet]
        public IActionResult Get(string url)
        {
            var webGet = new HtmlWeb();
            var documentTask = webGet.LoadFromWebAsync(url);

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
            else if(node.ChildNodes.Any(n => n.Name == "br"))
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