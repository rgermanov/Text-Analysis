using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using MongoDB.Driver;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Repositories
{
    public class MongoRepository<TEntity> : IResourcesRepository<TEntity> where TEntity : IEntity
    {
        // string connectionString = @"mongodb://text-analysis:LMelMAxJfacoTwjNDw6YBS2bpTvZeyKStOU4ffmwgrZ0yi91vxaToLAQTQr8Dbm4qQZvEW0qWpsp9AgpmCgIig==@text-analysis.documents.azure.com:10250/?ssl=true&sslverifycertificate=false";       
        private const string ConnectionString = @"mongodb://text-analysis:text-analysis123@ds145009.mlab.com:45009/text-analysis-dev";
        public void Add(TEntity entity)
        {
            var mongoClient = this.CreateMongoClient();

            var database = mongoClient.GetDatabase("text-analysis-dev");

            var collection = database.GetCollection<TEntity>(typeof(TEntity).Name);

            collection.InsertOne(entity);
        }

        public IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            var mongoClient = this.CreateMongoClient();

            var database = mongoClient.GetDatabase("text-analysis-dev");

            var collection = database.GetCollection<TEntity>(typeof(TEntity).Name);

            var items = collection.Find(filter);

            return items.ToEnumerable();
        }

        public IEnumerable<TEntity> Get(string resourceId)
        {
            throw new NotImplementedException();
        }

        private MongoClient CreateMongoClient()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(
                          new MongoUrl(ConnectionString)
                        );
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };

            var mongoClient = new MongoClient(settings);

            return mongoClient;
        }
    }
}