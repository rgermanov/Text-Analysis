using System;
using System.Linq;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Repositories
{
    public class ResourcesRepository<TEntity> : IResourcesRepository<TEntity> where TEntity : IEntity
    {
        void IResourcesRepository<TEntity>.Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IResourcesRepository<TEntity>.FilterBy(Func<TEntity, bool> filter)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IResourcesRepository<TEntity>.Get(string resourceId)
        {
            throw new NotImplementedException();
        }
    }

}