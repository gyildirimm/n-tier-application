using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccessLayer.EntityFramework
{
    public class EfGenericRepository<TEntity, TKey, TContext> : 
            IGenericRepository<TEntity, TKey, TContext>
        where TEntity : class, IEntity<TKey>, new()
        where TKey : IEquatable<TKey>
        where TContext : DbContext
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfGenericRepository(TContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Add(TEntity createEntity)
        {
            _dbSet.Add(createEntity);

            return createEntity;
        }

        public async Task<TEntity> AddAsync(TEntity createEntity)
        {
            await _dbSet.AddAsync(createEntity);

            return createEntity;
        }

        public void Remove(TEntity deleteEntity)
        {
            _dbSet.Remove(deleteEntity);
        }

        public void Remove(TKey key)
        {
            TEntity entity = GetById(key);

            Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking().AsQueryable();

            if(predicate != null)
                entities = entities.Where(predicate);

            return entities.AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking().AsQueryable();

            if (predicate != null)
                entities = entities.Where(predicate);

            return await entities.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity GetById(TKey key)
        {
            var entity = _dbSet.FirstOrDefault(w => w.Id.Equals(key));

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public async Task<TEntity> GetByIdAsync(TKey key)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(w => w.Id.Equals(key));

            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity;
        }

        public TEntity Update(TEntity updateEntity)
        {
            _context.Entry(updateEntity).State = EntityState.Modified;

            return updateEntity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>>? predicate)
        {
            IQueryable<TEntity> entities = _dbSet.AsNoTracking().AsQueryable();

            if(predicate != null)
                entities = entities.Where(predicate);

            return entities;
        }
    }
}
