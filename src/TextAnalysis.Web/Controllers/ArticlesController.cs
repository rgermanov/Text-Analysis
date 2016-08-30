using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Data;

namespace TextAnalysis.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ResourcesContext _context;
        
        public ArticlesController (ResourcesContext context)
        {
          _context = context;
        }

        [HttpPost]
        public IActionResult Post(string url)
        {
            /*
                1. Generate unique key from the url.
                2. Insert the url into a table.
                3. 

            */

            return Ok();
        }
    }
}