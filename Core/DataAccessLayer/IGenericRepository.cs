using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccessLayer
{
    public interface IGenericRepository<TEntity, TKey, TContext> : 
            IAsyncRepository<TEntity, TKey>,
            ISyncRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>? predicate);
    }
}
