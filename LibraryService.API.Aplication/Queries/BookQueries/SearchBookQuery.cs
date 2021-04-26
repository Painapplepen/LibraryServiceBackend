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

            if (propertyName.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
                return "AuthorName";
            }

            if (propertyName.Equals("surname", StringComparison.InvariantCultureIgnoreCase))
            {
                return "AuthorSurname";
            }

            if (propertyName.Equals("patronymic", StringComparison.InvariantCultureIgnoreCase))
            {
                return "AuthorPatronymic";
            }

            if (propertyName.Equals("genre", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Genre";
            }

            if (propertyName.Equals("publisher", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Publisher";
            }

            if (propertyName.Equals("title", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Title";
            }

            if (propertyName.Equals("pages", StringComparison.InvariantCultureIgnoreCase))
            {
                return "AmountPage";
            }

            if (propertyName.Equals("year", StringComparison.InvariantCultureIgnoreCase))
            {
                return "Year";
            }

            return propertyName;
        }
    }
}
