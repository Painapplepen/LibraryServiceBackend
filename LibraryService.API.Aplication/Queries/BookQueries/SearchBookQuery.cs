﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
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
        private readonly IBookService bookService;

        public SearchBookQueryHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<PagedResponse<FoundBookDTO>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            BookSearchCondition searchCondition = new BookSearchCondition()
            {
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
            IReadOnlyCollection<Book> foundBook = await bookService.FindAsync(searchCondition, sortProperty);
            FoundBookDTO[] mappedAuthor = foundBook.Select(MapToFoundBook).ToArray();
            var totalCount = await bookService.CountAsync(searchCondition);

            return new PagedResponse<FoundBookDTO>
            {
                Items = mappedAuthor,
                TotalCount = totalCount
            };
        }
        // Check it.
        public FoundBookDTO MapToFoundBook(Book book)
        {
            return new FoundBookDTO
            {
                Id = book.Id,
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                Author =
                {
                    Id = book.AuthorId,
                    Name = book.Author.Name,
                    Surname = book.Author.Surname,
                    Patronymic = book.Author.Patronymic
                },
                Genre =
                {
                    Id = book.GenreId,
                    Name = book.Genre.Name
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
                return nameof(Book.Id);
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
                return nameof(Genre.Name);
            }

            if (propertyName.Equals("publisher", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Publisher.Name);
            }

            if (propertyName.Equals("Title", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Title);
            }

            if (propertyName.Equals("AmountPage", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Year);
            }

            if (propertyName.Equals("Year", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Book.Year);
            }

            return propertyName;
        }
    }
}