using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccessLayer
{
    public interface ISyncRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        TEntity Add(TEntity createEntity);

        TEntity Update(TEntity updateEntity);

        void Remove(TEntity deleteEntity);

        void Remove(TKey key);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate);

        TEntity GetById(TKey key);

    }
}
