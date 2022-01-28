using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable
        where TContext : DbContext
    {
        Task<int> SaveChangesAsync();

        int SaveChanges();

        void Commit();

        Task CommitAsync();

        void BeginTransaction();

        Task BeginTransactionAsync();

        void Rollback();

        Task RollbackAsync();

    }
}
