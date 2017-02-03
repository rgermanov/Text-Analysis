using System;
using System.Linq;
using System.Linq.Expressions;

namespace TextAnalysis.Web.Domain.Contracts
{
    public interface IResourcesRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Get(string resourceId);

        IQueryable<TEntity> FilterBy(Func<TEntity, bool> filter);

        void Add(TEntity entity);
    }
}