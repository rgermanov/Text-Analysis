using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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

        public ArticlesController(
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
            // string connectionString = @"mongodb://text-analysis:LMelMAxJfacoTwjNDw6YBS2bpTvZeyKStOU4ffmwgrZ0yi91vxaToLAQTQr8Dbm4qQZvEW0qWpsp9AgpmCgIig==@text-analysis.documents.azure.com:10250/?ssl=true&sslverifycertificate=false";
            string connectionString = @"mongodb://text-analysis:text-analysis123@ds145009.mlab.com:45009/text-analysis-dev";
            
            MongoClientSettings settings = MongoClientSettings.FromUrl(
              new MongoUrl(connectionString)
            );
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            var mongoClient = new MongoClient(settings);

            var database = mongoClient.GetDatabase("web-pages");

            var collection = database.GetCollection<ResourceUrl>("urls");

            var items = collection.AsQueryable().ToList();

            return Ok(items);

            // var links = _urlRepository.FilterBy(item => true);

            // return Ok(links);
        }

        [HttpPost]
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