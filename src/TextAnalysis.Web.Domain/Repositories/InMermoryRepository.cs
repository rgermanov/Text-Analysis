using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Repositories
{
    public class InMemoryRepository<TEntity> : IResourcesRepository<TEntity> where TEntity : IEntity
    {
        private static readonly Dictionary<string, TEntity> _dataStorage = new Dictionary<string, TEntity>();

        public void Add(TEntity entity)
        {
            if (_dataStorage.Any(item => item.Key != entity.Key))
            {
                _dataStorage.Add(entity.Key, entity);
            }            
        }

        public IQueryable<TEntity> FilterBy(Func<TEntity, bool> filter)
        {
            var entities = _dataStorage.Select(item => item.Value).ToList();
            
            return entities.Where(filter).AsQueryable();
        }

        public IQueryable<TEntity> Get(string resourceId)
        {
            throw new NotImplementedException();
        }
    }
}
