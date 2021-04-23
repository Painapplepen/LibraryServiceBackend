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
    public interface IBookViewService : IBaseService<BookView>
    {
        Task<IReadOnlyCollection<BookView>> FindAsync(BookSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(BookSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    }
    public class BookViewService : BaseService<BookView>, IBookViewService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookViewService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return dbContext.Books.AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<BookView>> FindAsync(BookSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<BookView> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<long> CountAsync(BookSearchCondition searchCondition)
        {
            IQueryable<BookView> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<BookView> BuildFindQuery(BookSearchCondition searchCondition)
        {
            IQueryable<BookView> query = dbContext.BookViews;

            if (searchCondition.Title.Any())
            {
                foreach (var bookTitle in searchCondition.Title)
                {
                    var upperBookTitle = bookTitle.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Title != null && x.Title.ToUpper().Contains(upperBookTitle));
                }
            }

            if (searchCondition.AuthorName.Any())
            {
                foreach (var authorName in searchCondition.AuthorName)
                {
                    var upperAuthorName = authorName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.AuthorName != null && x.AuthorName.ToUpper().Contains(upperAuthorName));
                }
            }

            if (searchCondition.AuthorPatronymic.Any())
            {
                foreach (var authorPatronymic in searchCondition.AuthorPatronymic)
                {
                    var upperAuthorPatronymic = authorPatronymic.ToUpper().Trim();
                    query = query.Where(x =>
                        x.AuthorPatronymic != null &&
                        x.AuthorPatronymic.ToUpper().Contains(upperAuthorPatronymic));
                }
            }

            if (searchCondition.AuthorSurname.Any())
            {
                foreach (var authorSurname in searchCondition.AuthorSurname)
                {
                    var upperAuthorSurname = authorSurname.ToUpper().Trim();
                    query = query.Where(x =>
                        x.AuthorSurname != null &&
                        x.AuthorSurname.ToUpper().Contains(upperAuthorSurname));
                }
            }

            if (searchCondition.Year.Any())
            {
                foreach (var bookYear in searchCondition.Year)
                {
                    query = query.Where(x => x.Year == bookYear);
                }
            }

            if (searchCondition.AmountPage.Any())
            {
                foreach (var bookAmountPage in searchCondition.AmountPage)
                {
                    query = query.Where(x => x.AmountPage == bookAmountPage);
                }
            }

            if (searchCondition.Genre.Any())
            {
                foreach (var genre in searchCondition.Genre)
                {
                    var upperGenre = genre.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Genre != null && x.Genre.ToUpper().Contains(upperGenre));
                }
            }

            if (searchCondition.Publisher.Any())
            {
                foreach (var publisher in searchCondition.Publisher)
                {
                    var upperPublisher = publisher.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Publisher != null && x.Publisher.ToUpper().Contains(upperPublisher));
                }
            }

            return query;
        }
    }
}