using System;
using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IResourcesRepository<ResourceUrl> _urlRepository;
        private readonly IResourcesRepository<ResourceContent> _contentRepository;        
        private readonly IUniqueIdentifierProvider _uniqueIdentifierProvider;
        
        public ArticlesController (
                IResourcesRepository<ResourceUrl> urlRepository,
                IResourcesRepository<ResourceContent> contentRepository,
                IUniqueIdentifierProvider uniqueIdentifierProvider)
        {
            _urlRepository = urlRepository;
            _contentRepository = contentRepository;
            _uniqueIdentifierProvider = uniqueIdentifierProvider;
        }

        [HttpGet]
        public IActionResult Get() 
        {            
            return Ok(new { articleUrl = "https://google.com"});
        }

        [HttpPost]
        public IActionResult Post(string url)
        {
            /*
                1. Generate unique key from the url.
                2. Insert the url into a table.
                3. 

            */
            var resourceUrl = new ResourceUrl() 
            {
                Key = this.GenerateUrlKey(url),
                Url = url
            };

            _urlRepository.Add(resourceUrl);

            //TODO: Fetch the resource from Generic Provider
            // and return the title and the content
            // var resourceContent = new ResourceContent
            // {
            //     Key = resourceUrl.Key,
            //     //Title =     
            // };

            // _contentRepository.Add(resourceContent);

            return Ok(resourceUrl);
        }
        
        private string GenerateUrlKey(string url)
        {            
            var uniqueId = _uniqueIdentifierProvider.Generate(url.ToLower());
            
            return uniqueId;
        }
    }
}