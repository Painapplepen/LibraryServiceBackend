using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task<TEntity> InsertAsync(TEntity newEntity);
        Task<TEntity> UpdateAsync(long id, TEntity newEntity);
        Task<bool> DeleteAsync(long id);
    }

    public abstract class BaseService<TContext, TEntity> : IBaseService<TEntity>, IDisposable
        where TContext : DbContext
        where TEntity : class
    {
        private TContext DbContext { get; }
        private readonly DbSet<TEntity> dbSet;

        protected BaseService(TContext dbContext)
        {
            dbSet = DbContext.Set<TEntity>();
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity newEntity)
        {
            await dbSet.AddAsync(newEntity);
            Save();
            return await dbSet.FindAsync(newEntity);

        }

        public async Task<TEntity> UpdateAsync(long id, TEntity newEntity)
        {
            throw new System.NotImplementedException();
            //var result = await dbSet.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = dbSet.Remove(await dbSet.FindAsync(id));
            
            if (result != null)
            {
                Save();
                return true;
            }

            return false;
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
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