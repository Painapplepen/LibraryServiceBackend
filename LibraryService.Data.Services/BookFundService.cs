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
    public interface IBookFundService : IBaseService<BookFund>
    {
        Task<IReadOnlyCollection<BookFund>> FindAsync(BookFundSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(BookFundSearchCondition searchCondition);
        Task<bool> ExistsAsync(long id);
    }
    public class BookFundService : BaseService<BookFund>, IBookFundService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookFundService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<BookFund>> FindAsync(BookFundSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<BookFund> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public Task<bool> ExistsAsync(long id)
        {
            return dbContext.BookFunds.AnyAsync(entity => entity.Id == id);
        }


        public async Task<long> CountAsync(BookFundSearchCondition searchCondition)
        {
            IQueryable<BookFund> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<BookFund> BuildFindQuery(BookFundSearchCondition searchCondition)
        {
            IQueryable<BookFund> query = dbContext.BookFunds;

            if (searchCondition.BookTitle.Any())
            {
                foreach (var bookTitle in searchCondition.BookTitle)
                {
                    var upperBookTitle = bookTitle.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Title != null && x.Book.Title.ToUpper().Contains(upperBookTitle));
                }
            }

            if (searchCondition.LibraryName.Any())
            {
                foreach (var libraryName in searchCondition.LibraryName)
                {
                    var upperLibraryName = libraryName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Library.Name != null && x.Library.Name.ToUpper().Contains(upperLibraryName));
                }
            }

            if (searchCondition.AuthorName.Any())
            {
                foreach (var authorName in searchCondition.AuthorName)
                {
                    var upperAuthorName = authorName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Author.Name != null && x.Book.Author.Name.ToUpper().Contains(upperAuthorName));
                }
            }

            if (searchCondition.AuthorPatronymic.Any())
            {
                foreach (var authorPatronymic in searchCondition.AuthorPatronymic)
                {
                    var upperAuthorPatronymic = authorPatronymic.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Author.Patronymic != null && x.Book.Author.Patronymic.ToUpper().Contains(upperAuthorPatronymic));
                }
            }

            if (searchCondition.AuthorSurname.Any())
            {
                foreach (var authorSurname in searchCondition.AuthorSurname)
                {
                    var upperAuthorSurname = authorSurname.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Author.Surname != null && x.Book.Author.Surname.ToUpper().Contains(upperAuthorSurname));
                }
            }

            if (searchCondition.BookYear.Any())
            {
                foreach (var bookYear in searchCondition.BookYear)
                {
                    query = query.Where(x => x.Book.Year == bookYear);
                }
            }

            if (searchCondition.BookAmountPage.Any())
            {
                foreach (var bookAmountPage in searchCondition.BookAmountPage)
                {
                    query = query.Where(x => x.Book.AmountPage == bookAmountPage);
                }
            }

            if (searchCondition.Genre.Any())
            {
                foreach (var genre in searchCondition.Genre)
                {
                    var upperGenre = genre.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Genre.Name != null && x.Book.Genre.Name.ToUpper().Contains(upperGenre));
                }
            }

            if (searchCondition.Publisher.Any())
            {
                foreach (var publisher in searchCondition.Publisher)
                {
                    var upperPublisher = publisher.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Book.Publisher.Name != null && x.Book.Publisher.Name.ToUpper().Contains(upperPublisher));
                }
            }

            if (searchCondition.LibraryAddress.Any())
            {
                foreach (var libraryAddress in searchCondition.LibraryAddress)
                {
                    var upperLibraryAddress = libraryAddress.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Library.Address != null && x.Library.Address.ToUpper().Contains(upperLibraryAddress));
                }
            }

            if (searchCondition.LibraryTelephone.Any())
            {
                foreach (var libraryTelephone in searchCondition.LibraryTelephone)
                {
                    var upperLibraryTelephone = libraryTelephone.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Library.Telephone != null && x.Library.Telephone.ToUpper().Contains(upperLibraryTelephone));
                }
            }

            if (searchCondition.Amount.Any())
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