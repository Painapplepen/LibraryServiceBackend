using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.BookFund;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.BookFundQueries
{
    public class SearchBookFundQuery : PagedSearchQuery<FoundBookFundDTO, BookFundSearchCondition>
    {
        public SearchBookFundQuery(BookFundSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchBookFundQueryHandler : IRequestHandler<SearchBookFundQuery, PagedResponse<FoundBookFundDTO>>
    {
        private readonly IBookFundViewService bookFundViewService;
        public SearchBookFundQueryHandler(IBookFundViewService bookFundViewService)
        {
            this.bookFundViewService = bookFundViewService;
        }

        public async Task<PagedResponse<FoundBookFundDTO>> Handle(SearchBookFundQuery request, CancellationToken cancellationToken)
        {
            BookFundSearchCondition searchCondition = new BookFundSearchCondition()
            {
                ISBN = GetFilterValues(request.SearchCondition.ISBN),
                BookTitle = GetFilterValues(request.SearchCondition.BookTitle),
                BookYear = request.SearchCondition.BookYear,
                BookAmountPage = request.SearchCondition.BookAmountPage,
                AuthorName = GetFilterValues(request.SearchCondition.AuthorName),
                AuthorSurname = GetFilterValues(request.SearchCondition.AuthorSurname),
                AuthorPatronymic = GetFilterValues(request.SearchCondition.AuthorPatronymic),
                LibraryName = GetFilterValues(request.SearchCondition.LibraryName),
                LibraryTelephone = GetFilterValues(request.SearchCondition.LibraryTelephone),
                LibraryAddress = GetFilterValues(request.SearchCondition.LibraryAddress),
                Amount = request.SearchCondition.Amount,
                Genre = GetFilterValues(request.SearchCondition.Genre),
                Publisher = GetFilterValues(request.SearchCondition.Publisher),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<BookFundView> foundBookFund = await bookFundViewService.FindAsync(searchCondition, sortProperty);
            FoundBookFundDTO[] mappedBookFund = foundBookFund.Select(MapToFoundBookFund).ToArray();
            var totalCount = await bookFundViewService.CountAsync(searchCondition);

            return new PagedResponse<FoundBookFundDTO>
            {
                Items = mappedBookFund,
                TotalCount = totalCount
            };
        }

        private FoundBookFundDTO MapToFoundBookFund(BookFundView bookFund)
        {
            return new FoundBookFundDTO
            {
                Id = bookFund.Id,
                ISBN = bookFund.ISBN,
                Amount = bookFund.Amount,
                BookAmountPage = bookFund.BookAmountPage,
                BookTitle = bookFund.BookTitle,
                BookYear = bookFund.BookYear,
                AuthorName = bookFund.AuthorName,
                AuthorSurname = bookFund.AuthorSurname,
                AuthorPatronymic = bookFund.AuthorPatronymic,
                Genre = bookFund.Genre,
                Publisher = bookFund.Publisher,
                LibraryAddress = bookFund.LibraryAddress,
                LibraryName = bookFund.LibraryName,
                LibraryTelephone = bookFund.LibraryTelephone

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
                return nameof(BookFund.Id);
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

            if (propertyName.Equals("library", StringComparison.InvariantCultureIgnoreCase))
            {
                return "LibraryName";
            }

            if (propertyName.Equals("telephone", StringComparison.InvariantCultureIgnoreCase))
            {
                return "LibraryTelephone";
            }

            if (propertyName.Equals("address", StringComparison.InvariantCultureIgnoreCase))
            {
                return "LibraryAddress";
            }

            if (propertyName.Equals("amount", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(BookFund.Amount);
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
                return "BookTitle";
            }

            if (propertyName.Equals("pages", StringComparison.InvariantCultureIgnoreCase))
            {
                return "BookAmountPage";
            }

            if (propertyName.Equals("year", StringComparison.InvariantCultureIgnoreCase))
            {
                return "BookYear";
            }

            return propertyName;
        }
    }
}
