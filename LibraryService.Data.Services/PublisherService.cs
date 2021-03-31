using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface IPublisherService : IBaseService<Publisher>
    {
        Task<IReadOnlyCollection<Publisher>> FindAsync(PublisherSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(PublisherSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id);
    }
    public class PublisherService : BaseService<Publisher>, IPublisherService
    {
        private readonly LibraryServiceDbContext dbContext;

        public PublisherService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id)
        {
            return dbContext.Publishers.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IReadOnlyCollection<Publisher>> FindAsync(PublisherSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Publisher> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public async Task<long> CountAsync(PublisherSearchCondition searchCondition)
        {
            IQueryable<Publisher> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Publisher> BuildFindQuery(PublisherSearchCondition searchCondition)
        {
            IQueryable<Publisher> query = dbContext.Publishers;

            if (searchCondition.Name.Any())
            {
                foreach (var name in searchCondition.Name)
                {
                    var upperName = name.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Name != null && x.Name.ToUpper().Contains(upperName));
                }
            }

            return query;
        }
    }
}
