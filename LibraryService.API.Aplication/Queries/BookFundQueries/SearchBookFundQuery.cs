﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.BookFund;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
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
        private readonly IBookFundService bookFundService;

        public SearchBookFundQueryHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<PagedResponse<FoundBookFundDTO>> Handle(SearchBookFundQuery request, CancellationToken cancellationToken)
        {
            BookFundSearchCondition searchCondition = new BookFundSearchCondition()
            {
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
            IReadOnlyCollection<BookFund> foundBookFund = await bookFundService.FindAsync(searchCondition, sortProperty);
            FoundBookFundDTO[] mappedBookFund = foundBookFund.Select(MapToFoundBookFund).ToArray();
            var totalCount = await bookFundService.CountAsync(searchCondition);

            return new PagedResponse<FoundBookFundDTO>
            {
                Items = mappedBookFund,
                TotalCount = totalCount
            };
        }
        // Check it.
        public FoundBookFundDTO MapToFoundBookFund(BookFund bookFund)
        {
            return new FoundBookFundDTO
            {
                Id = bookFund.Id,
                Amount = bookFund.Amount,
                Book =
                {
                    Id = bookFund.BookId,
                    AmountPage = bookFund.Book.AmountPage,
                    Title = bookFund.Book.Title,
                    Year = bookFund.Book.Year,
                    Author = 
                    {
                        Id = bookFund.Book.AuthorId,
                        Name = bookFund.Book.Author.Name,
                        Surname = bookFund.Book.Author.Surname,
                        Patronymic = bookFund.Book.Author.Patronymic
                    },
                    Genre =
                    {
                        Id = bookFund.Book.GenreId,
                        Name = bookFund.Book.Genre.Name
                    }
                },
                Library =
                {
                    Id = bookFund.LibraryId,
                    Address = bookFund.Library.Address,
                    Name = bookFund.Library.Name,
                    Telephone = bookFund.Library.Telephone
                }
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

            if (propertyName.Equals("libraryName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Name);
            }

            if (propertyName.Equals("libraryTelephone", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Telephone);
            }

            if (propertyName.Equals("libraryAddress", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Address);
            }

            if (propertyName.Equals("amount", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(BookFund.Amount);
            }

            if (propertyName.Equals("genre", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Genre.Name);
            }

            if (propertyName.Equals("publisher", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Publisher.Name);
            }

            if (propertyName.Equals("bookTitle", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Title);
            }

            if (propertyName.Equals("bookAmountPage", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Year);
            }

            if (propertyName.Equals("bookYear", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Year);
            }

            return propertyName;
        }
    }
}
