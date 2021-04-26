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
    public interface IBookFundViewService : IBaseService<BookFundView>
    {
        Task<IReadOnlyCollection<BookFundView>> FindAsync(BookFundSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(BookFundSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    }

    public class BookFundViewService : BaseService<BookFundView>, IBookFundViewService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookFundViewService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<BookFundView>> FindAsync(BookFundSearchCondition searchCondition,
            string sortProperty)
        {
            IQueryable<BookFundView> query = BuildFindQuery(searchCondition);

            query = searchCondition.SortDirection == "asc"
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return dbContext.BookFunds.AnyAsync(entity => entity.Id == id, cancellationToken);
        }


        public async Task<long> CountAsync(BookFundSearchCondition searchCondition)
        {
            IQueryable<BookFundView> query = BuildFindQuery(searchCondition);

            var count = await query.LongCountAsync();

            return count % 10 == 0 ? count / 10 : count / 10 + 1;
        }

        private IQueryable<BookFundView> BuildFindQuery(BookFundSearchCondition searchCondition)
        {
            IQueryable<BookFundView> query = dbContext.BookFundViews;

            if (searchCondition.BookTitle.Any())
            {
                foreach (var bookTitle in searchCondition.BookTitle)
                {
                    var upperBookTitle = bookTitle.ToUpper().Trim();
                    query = query.Where(x =>
                        x.BookTitle != null && x.BookTitle.ToUpper().Contains(upperBookTitle));
                }
            }

            if (searchCondition.LibraryName.Any())
            {
                foreach (var libraryName in searchCondition.LibraryName)
                {
                    var upperLibraryName = libraryName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.LibraryName != null && x.LibraryName.ToUpper().Contains(upperLibraryName));
                }
            }

            if (searchCondition.ISBN.Any())
            {
                foreach (var bookTitle in searchCondition.ISBN)
                {
                    var upperBookTitle = bookTitle.ToUpper().Trim();
                    query = query.Where(x =>
                        x.ISBN != null && x.ISBN.ToUpper().Contains(upperBookTitle));
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

            if (searchCondition.BookYear != null)
            {
                foreach (var bookYear in searchCondition.BookYear)
                {
                    query = query.Where(x => x.BookYear == bookYear);
                }
            }

            if (searchCondition.BookAmountPage != null)
            {
                foreach (var bookAmountPage in searchCondition.BookAmountPage)
                {
                    query = query.Where(x => x.BookAmountPage == bookAmountPage);
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

            if (searchCondition.LibraryAddress.Any())
            {
                foreach (var libraryAddress in searchCondition.LibraryAddress)
                {
                    var upperLibraryAddress = libraryAddress.ToUpper().Trim();
                    query = query.Where(x =>
                        x.LibraryAddress != null && x.LibraryAddress.ToUpper().Contains(upperLibraryAddress));
                }
            }

            if (searchCondition.LibraryTelephone.Any())
            {
                foreach (var libraryTelephone in searchCondition.LibraryTelephone)
                {
                    var upperLibraryTelephone = libraryTelephone.ToUpper().Trim();
                    query = query.Where(x =>
                        x.LibraryTelephone != null &&
                        x.LibraryTelephone.ToUpper().Contains(upperLibraryTelephone));
                }
            }

            if (searchCondition.Amount != null)
            {
                foreach (var amount in searchCondition.Amount)
                {
                    query = query.Where(x => x.Amount == amount);
                }
            }

            return query;
        }
    }

}