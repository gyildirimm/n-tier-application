using Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {

        private readonly TContext _context;
        private bool disposed = false;
        private IDbContextTransaction transaction;

        public UnitOfWork(TContext context)
        {
            //Database.SetInitializer<TContext>(null);
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }

        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await transaction.CommitAsync();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public async Task RollbackAsync()
        {
            await transaction.RollbackAsync();
        }

        public void Rollback()
        {
            transaction?.Rollback();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
