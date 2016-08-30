using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Data;

namespace TextAnalysis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ResourcesContext _context;
        
        public HomeController(ResourcesContext context)
        {
            _context = context;    
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
