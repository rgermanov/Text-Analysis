using Microsoft.AspNetCore.Mvc;
using TextAnalysis.Web.Domain.Contracts;
using TextAnalysis.Web.Domain.Models;
using TextAnalysis.Web.Models;
using System.Linq;

namespace TextAnalysis.Web.Controllers
{
    [Route("api/articles/read", Name = "ArticleRead")]
    public class ArticleReadController : Controller
    {
        private readonly IResourcesRepository<ResourceContent> _contentRepository;

        public ArticleReadController(IResourcesRepository<ResourceContent> contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [HttpGet]
        [Route("{key}", Name = "ArticleReadGet")]
        public IActionResult Get(string key)
        {
            var resources = _contentRepository.Get(key);

            if (resources == null)
            {
                return NotFound();
            }

            var resourceContent = resources.OrderByDescending(item => item.Id);
            
            if (!resourceContent.Any())
            {
                return NotFound();
            }
            
            return Ok(AutoMapper.Mapper.Map<ArticleContentModel>(resourceContent.First()));
        }
    }
}