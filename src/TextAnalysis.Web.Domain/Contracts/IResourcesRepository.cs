using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace TextAnalysis.Web.Domain.Contracts
{
    public interface IResourcesRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> Get(string resourceId);

        IEnumerable<TEntity> FilterBy(Expression<Func<TEntity, bool>> filter);

        void Add(TEntity entity);
    }
}