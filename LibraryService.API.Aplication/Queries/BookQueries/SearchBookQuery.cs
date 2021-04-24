using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.BookQueries
{
    public class SearchBookQuery : PagedSearchQuery<FoundBookDTO, BookSearchCondition>
    {
        public SearchBookQuery(BookSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchBookQueryHandler : IRequestHandler<SearchBookQuery, PagedResponse<FoundBookDTO>>
    {
        private readonly IBookViewService bookViewService;
        public SearchBookQueryHandler(IBookViewService bookViewService)
        {
            this.bookViewService = bookViewService;
        }

        public async Task<PagedResponse<FoundBookDTO>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            BookSearchCondition searchCondition = new BookSearchCondition()
            {
                ISBN = GetFilterValues(request.SearchCondition.ISBN),
                Title = GetFilterValues(request.SearchCondition.Title),
                Year = request.SearchCondition.Year,
                AmountPage = request.SearchCondition.AmountPage,
                AuthorName = GetFilterValues(request.SearchCondition.AuthorName),
                AuthorSurname = GetFilterValues(request.SearchCondition.AuthorSurname),
                AuthorPatronymic = GetFilterValues(request.SearchCondition.AuthorPatronymic),
                Genre = GetFilterValues(request.SearchCondition.Genre),
                Publisher = GetFilterValues(request.SearchCondition.Publisher),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<BookView> foundBook = await bookViewService.FindAsync(searchCondition, sortProperty);
            FoundBookDTO[] mappedAuthor = foundBook.Select(MapToFoundBookDTO).ToArray();
            var totalCount = await bookViewService.CountAsync(searchCondition);

            return new PagedResponse<FoundBookDTO>
            {
                Items = mappedAuthor,
                TotalCount = totalCount
            };
        }

        private FoundBookDTO MapToFoundBookDTO(BookView book)
        {
            return new FoundBookDTO
            {
                Id = book.Id,
                ISBN = book.ISBN,
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                AuthorName = book.AuthorName,
                AuthorSurname = book.AuthorSurname,
                AuthorPatronymic = book.AuthorPatronymic,
                Genre = book.Genre,
                Publisher = book.Publisher
            };
        }
        
        private string[] GetFilterValues(ICollection<string> values)
        {
            return values == null
                       ? Array.Empty<string>()
                       : values.Select(v => v.Trim()).Distinct().ToArray();
        }

        protected string GetSortProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return nameof(Book.Id);
            }

            if (propertyName.Equals("isbn", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.ISBN);
            }

            if (propertyName.Equals("authorName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Name);
            }

            if (propertyName.Equals("authorSurName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Surname);
            }

            if (propertyName.Equals("authorPatronymic", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Patronymic);
            }

            if (propertyName.Equals("genre", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Genre";
            }

            if (propertyName.Equals("publisher", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Publisher";
            }

            if (propertyName.Equals("Title", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Title);
            }

            if (propertyName.Equals("AmountPage", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.AmountPage);
            }

            if (propertyName.Equals("Year", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Year);
            }

            return propertyName;
        }
    }
}
