using System;
using System.Linq;
using System.Linq.Expressions;

namespace TextAnalysis.Web.Domain.Contracts 
{
    public interface IResourcesRepository<TEntity> 
    {        
        IQueryable<TEntity> Get(string resourceId);

        IQueryable<TEntity> FilterBy(Expression<Func<bool, TEntity>> filter);

        void Add(TEntity entity);        
    }
}