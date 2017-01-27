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
        public IActionResult Post(ArticleModel model)
        {
            var validator = new ArticleModelValidator();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid) {
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