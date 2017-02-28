using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Repositories
{
    public class MongoRepository<TEntity> : IResourcesRepository<TEntity> where TEntity : IKeyEntity
    {        
        private MongoClient client;

        public MongoRepository(MongoClient client)
        {
            this.client = client;
        }

        public void Add(TEntity entity)
        {
            var collection = this.GetCollection();            

            collection.InsertOne(entity);
        }

        public IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            var collection = this.GetCollection();

            var items = collection.AsQueryable<TEntity>().Where(filter.Compile());

            return items.AsEnumerable();
        }

        public IEnumerable<TEntity> Get(string key)
        {
            var collection = this.GetCollection();

            var entities = collection.AsQueryable<TEntity>().Where(e => e.Key == key);

            return entities;
        }

        private IMongoCollection<TEntity> GetCollection()
        {            
            var database = this.client.GetDatabase("text-analysis-dev");

            var collection = database.GetCollection<TEntity>(typeof(TEntity).Name);

            return collection;
        }
    }
}