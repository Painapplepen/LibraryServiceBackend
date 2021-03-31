using System;
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
        private readonly IBookService bookService;
        private readonly IPublisherService publisherService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly ILibraryService libraryService;
        public SearchBookFundQueryHandler(IBookFundService bookFundService,
                                        IPublisherService publisherService,
                                        IAuthorService authorService,
                                        IGenreService genreService,
                                        IBookService bookService,
                                        ILibraryService libraryService)
        {
            this.bookFundService = bookFundService;
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
            this.libraryService = libraryService;
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

        public FoundBookFundDTO MapToFoundBookFund(BookFund bookFund)
        {
            //var library = libraryService.Get(bookFund.LibraryId);
            //var book = bookService.Get(bookFund.BookId) ;
            //var author = authorService.Get(book.AuthorId);
            //var genre = genreService.Get(book.GenreId);
            //var publisher = publisherService.Get(book.PublisherId);
            return new FoundBookFundDTO
            {
                Id = bookFund.Id,
                Amount = bookFund.Amount,
                //Book =
                //{
                //    Id = bookFund.BookId,
                //    AmountPage = book.AmountPage,
                //    Title = book.Title,
                //    Year = book.Year,
                //    Author =
                //    {
                //        Id = book.AuthorId,
                //        Name = author.Name,
                //        Surname = author.Surname,
                //        Patronymic = author.Patronymic
                //    },
                //    Genre =
                //    {
                //        Id = book.GenreId,
                //        Name = genre.Name
                //    },
                //    Publisher =
                //    {
                //        Id = book.PublisherId,
                //        Name = publisher.Name
                //    }
                //},
                //Library =
                //{
                //    Id = bookFund.LibraryId,
                //    Address = library.Address,
                //    Name = library.Name,
                //    Telephone = library.Telephone
                //}
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
