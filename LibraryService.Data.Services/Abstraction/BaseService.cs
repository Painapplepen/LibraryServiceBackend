using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long id);
        Task<TEntity> InsertAsync(TEntity newEntity);
        Task<TEntity> UpdateAsync(TEntity newEntity);
        Task DeleteAsync(long id);
    }

    public abstract class BaseService<TEntity> : IBaseService<TEntity>, IDisposable
        where TEntity : class
    {
        private LibraryServiceDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        protected BaseService(LibraryServiceDbContext dbContext)
        {
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity newEntity)
        {
            var addEntity = await dbSet.AddAsync(newEntity);
            await dbContext.SaveChangesAsync();
            return addEntity.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity newEntity)
        {
            var updateEntity = dbSet.Update(newEntity);
            await dbContext.SaveChangesAsync();
            return updateEntity.Entity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.DisposeAsync();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}