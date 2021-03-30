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

    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class
    {
        private LibraryServiceDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        protected BaseService(LibraryServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity newEntity)
        {
            await dbSet.AddAsync(newEntity);
            await dbContext.SaveChangesAsync();
            return newEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity newEntity)
        {
            if (dbContext.Entry(newEntity).State == EntityState.Detached)
            {
                dbSet.Attach(newEntity);
            }

            dbContext.ChangeTracker.DetectChanges();
            await dbContext.SaveChangesAsync();
            return newEntity;
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
    }
}