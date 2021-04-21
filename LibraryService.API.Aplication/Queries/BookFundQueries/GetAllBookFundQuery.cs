using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Queries.AuthorQueries;
using LibraryService.API.Contracts.Outgoing.BookFund;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.BookFundQueries
{
    public class GetAllBookFundQuery : IRequest<IReadOnlyCollection<FoundBookFundDTO>>
    {
        public GetAllBookFundQuery()
        {
        }
    }

    public class GetAllBookFundQueryHandler : IRequestHandler<GetAllBookFundQuery, IReadOnlyCollection<FoundBookFundDTO>>
    {
        private readonly IBookFundService bookFundService;
        private readonly IBookService bookService;
        private readonly IPublisherService publisherService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly ILibraryService libraryService;
        public GetAllBookFundQueryHandler(IBookFundService bookFundService,
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

        public async Task<IReadOnlyCollection<FoundBookFundDTO>> Handle(GetAllBookFundQuery request,
            CancellationToken cancellationToken)
        {
            var bookFunds = await bookFundService.GetAllAsync(cancellationToken);

            return bookFunds.Select(MapToFoundBookFundDTO).ToArray();
        }

        private FoundBookFundDTO MapToFoundBookFundDTO(BookFund bookFund)
        {
            CancellationToken cancellationToken = default;
            var library = libraryService.GetAsync(bookFund.LibraryId, cancellationToken).Result;
            var book = bookService.GetAsync(bookFund.BookId, cancellationToken).Result;
            var author = authorService.GetAsync(book.AuthorId, cancellationToken).Result;
            var genre = genreService.GetAsync(book.GenreId, cancellationToken).Result;
            var publisher = publisherService.GetAsync(book.PublisherId, cancellationToken).Result;
            return new FoundBookFundDTO
            {
                Id = bookFund.Id,
                Amount = bookFund.Amount,
                Book =
                {
                    Id = bookFund.BookId,
                    AmountPage = book.AmountPage,
                    Title = book.Title,
                    Year = book.Year,
                    Author =
                    {
                        Id = book.AuthorId,
                        Name = author.Name,
                        Surname = author.Surname,
                        Patronymic = author.Patronymic
                    },
                    Genre =
                    {
                        Id = book.GenreId,
                        Name = genre.Name
                    },
                    Publisher =
                    {
                        Id = book.PublisherId,
                        Name = publisher.Name
                    }
                },
                Library =
                {
                    Id = bookFund.LibraryId,
                    Address = library.Address,
                    Name = library.Name,
                    Telephone = library.Telephone
                }
            }; ;
        }
    }
}