using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface IGenreService : IBaseService<Genre>
    {
        Task<IReadOnlyCollection<Genre>> FindAsync(GenreSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(GenreSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id);
    }
    public class GenreService : BaseService<Genre>, IGenreService
    {
        private readonly LibraryServiceDbContext dbContext;

        public GenreService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id)
        {
            return dbContext.Genres.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IReadOnlyCollection<Genre>> FindAsync(GenreSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Genre> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public async Task<long> CountAsync(GenreSearchCondition searchCondition)
        {
            IQueryable<Genre> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Genre> BuildFindQuery(GenreSearchCondition searchCondition)
        {
            IQueryable<Genre> query = dbContext.Genres;

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
