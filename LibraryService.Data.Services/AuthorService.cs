using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface IAuthorService : IBaseService<Author>
    {
        Task<IReadOnlyCollection<Author>> FindAsync(AuthorSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(AuthorSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
    }
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        private readonly LibraryServiceDbContext dbContext;

        public AuthorService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return dbContext.Authors.AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Author>> FindAsync(AuthorSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Author> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<long> CountAsync(AuthorSearchCondition searchCondition)
        {
            IQueryable<Author> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Author> BuildFindQuery(AuthorSearchCondition searchCondition)
        {
            IQueryable<Author> query = dbContext.Authors;

            if (searchCondition.Name.Any())
            {
                foreach (var authorName in searchCondition.Name)
                {
                    var upperAuthorName = authorName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Name != null && x.Name.ToUpper().Contains(upperAuthorName));
                }
            }


            if (searchCondition.Surname.Any())
            {
                foreach (var authorSurname in searchCondition.Surname)
                {
                    var upperAuthorSurname = authorSurname.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Surname != null && x.Surname.ToUpper().Contains(upperAuthorSurname));
                }
            }

            if (searchCondition.Patronymic.Any())
            {
                foreach (var authorPatronymic in searchCondition.Patronymic)
                {
                    var upperAuthorPatronymic = authorPatronymic.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Patronymic != null && x.Patronymic.ToUpper().Contains(upperAuthorPatronymic));
                }
            }

            return query;
        }
    }
}