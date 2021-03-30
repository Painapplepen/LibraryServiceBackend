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
    public interface IBookService : IBaseService<Book>
    {
        Task<IReadOnlyCollection<Book>> FindAsync(BookSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(BookSearchCondition searchCondition);
    }
    public class BookService : BaseService<Book>, IBookService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Book>> FindAsync(BookSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Book> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public async Task<long> CountAsync(BookSearchCondition searchCondition)
        {
            IQueryable<Book> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Book> BuildFindQuery(BookSearchCondition searchCondition)
        {
            IQueryable<Book> query = dbContext.Books;

            if (searchCondition.Title.Any())
            {
                foreach (var title in searchCondition.Title)
                {
                    var upperTitle = title.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Title != null && x.Title.ToUpper().Contains(upperTitle));
                }
            }

            if (searchCondition.AuthorName.Any())
            {
                foreach (var authorName in searchCondition.AuthorName)
                {
                    var upperAuthorName = authorName.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Author.Name != null && x.Author.Name.ToUpper().Contains(upperAuthorName));
                }
            }

            if (searchCondition.AuthorPatronymic.Any())
            {
                foreach (var authorPatronymic in searchCondition.AuthorPatronymic)
                {
                    var upperAuthorPatronymic = authorPatronymic.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Author.Patronymic != null && x.Author.Patronymic.ToUpper().Contains(upperAuthorPatronymic));
                }
            }

            if (searchCondition.AuthorSurname.Any())
            {
                foreach (var authorSurname in searchCondition.AuthorSurname)
                {
                    var upperAuthorSurname = authorSurname.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Author.Surname != null && x.Author.Surname.ToUpper().Contains(upperAuthorSurname));
                }
            }

            if (searchCondition.Year.Any())
            {
                foreach (var year in searchCondition.Year)
                {
                    query = query.Where(x => x.Year == year);
                }
            }

            if (searchCondition.AmountPage.Any())
            {
                foreach (var amountPage in searchCondition.AmountPage)
                {
                    query = query.Where(x => x.AmountPage == amountPage);
                }
            }

            if (searchCondition.Genre.Any())
            {
                foreach (var genre in searchCondition.Genre)
                {
                    var upperGenre = genre.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Genre.Name != null && x.Genre.Name.ToUpper().Contains(upperGenre));
                }
            }

            if (searchCondition.Publisher.Any())
            {
                foreach (var publisher in searchCondition.Publisher)
                {
                    var upperPublisher = publisher.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Publisher.Name != null && x.Publisher.Name.ToUpper().Contains(upperPublisher));
                }
            }

            return query;
        }
    }
}