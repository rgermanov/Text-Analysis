using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using System.Threading.Tasks;

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
            
            var scripts = result.DocumentNode.Descendants("script");
            var styles = result.DocumentNode.Descendants("link");

            var nodesToGetText = result.DocumentNode.ChildNodes.Where(n => !scripts.Contains(n) && !styles.Contains(n));            

            return Ok(nodesToGetText.Select(n => n.InnerHtml));
        }
    }
}