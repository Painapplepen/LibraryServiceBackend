using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services.Abstraction
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(long? id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> InsertAsync(TEntity newEntity);
        Task<TEntity> UpdateAsync(TEntity newEntity);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
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

        public async Task<TEntity> GetAsync(long? id, CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(new object[] {id}, cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet.ToListAsync(cancellationToken);
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

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await GetAsync(id, cancellationToken);

            if (entity != null)
            {
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}