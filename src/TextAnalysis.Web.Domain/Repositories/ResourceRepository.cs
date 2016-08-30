using System;
using System.Linq;
using System.Linq.Expressions;
using TextAnalysis.Web.Domain.Contracts;

namespace TextAnalysis.Web.Domain.Repositories
{
    public class ResourcesRepository<TEntity> : IResourcesRepository<TEntity>
    {
        void IResourcesRepository<TEntity>.Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IResourcesRepository<TEntity>.FilterBy(Expression<Func<bool, TEntity>> filter)
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IResourcesRepository<TEntity>.Get(string resourceId)
        {
            throw new NotImplementedException();
        }
    }

}