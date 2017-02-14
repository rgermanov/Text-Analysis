using System;
using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Data;
using TextAnalysis.Web.Models;

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

        public IActionResult Tools()
        {
            var viewModel = new ToolsViewModel
            {
                BookmarkletScript = this.LoadBookmarkletScript()
            };

            return View(viewModel);
        }

        private string LoadBookmarkletScript()
        {
            var script = System.IO.File.ReadAllText("Scripts/bookmarklet.js");

            return script.Replace("{{@domain}}", string.Format(@"{0}://{1}", this.HttpContext.Request.Scheme, this.HttpContext.Request.Host.ToString()));
        }
    }
}
