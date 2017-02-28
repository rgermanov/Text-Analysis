using System.Collections.Generic;
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
           
            var articleModels = AutoMapper.Mapper.Map<List<ResourceUrl>, List<ArticleModel>>(links);
            articleModels.ForEach(articleModel =>
            {
                articleModel.References.Add("scrape", Url.Link("ArticleScrapeGet", new { key = articleModel.Key }));
                articleModel.References.Add("read", Url.Link("ArticleReadGet", new { key = articleModel.Key }));                
            });

            return Ok(articleModels);
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

            return Ok(AutoMapper.Mapper.Map<ArticleModel>(resourceUrl));
        }

        private string GenerateUrlKey(string url)
        {
            var uniqueId = _uniqueIdentifierProvider.Generate(url.ToLower());

            return uniqueId;
        }
    }
}