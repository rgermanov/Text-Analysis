using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;
using TextAnalysis.Web.Models;
using TextAnalysis.Web.Models.Validators;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private readonly IResourcesRepository<ResourceUrl> _urlRepository;
        private readonly IUniqueIdentifierProvider _uniqueIdentifierProvider;

        public ArticlesController(
                IResourcesRepository<ResourceUrl> urlRepository,                
                IUniqueIdentifierProvider uniqueIdentifierProvider)
        {
            _urlRepository = urlRepository;            
            _uniqueIdentifierProvider = uniqueIdentifierProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {                        
            var links = _urlRepository.FilterBy(url => !string.IsNullOrWhiteSpace(url.Url)).ToList();

            return Ok(links);
        }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public IActionResult Create([FromBody]ArticleModel model)
        {
            var validator = new ArticleModelValidator();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var resourceUrl = new ResourceUrl()
            {
                Key = this.GenerateUrlKey(model.Url),
                Url = model.Url
            };

            _urlRepository.Add(resourceUrl);

            return Ok(resourceUrl);
        }

        private string GenerateUrlKey(string url)
        {
            var uniqueId = _uniqueIdentifierProvider.Generate(url.ToLower());

            return uniqueId;
        }
    }
}