using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.BookFundQueries
{
    public class GetBookFundQuery : IRequest<BookFundDTO>
    {
        public long Id { get; }

        public GetBookFundQuery(long id)
        {
            Id = id;
        }
    }

    public class GetBookFundQueryHandler : IRequestHandler<GetBookFundQuery, BookFundDTO>
    {
        private readonly IBookFundService bookFundService;

        public GetBookFundQueryHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<BookFundDTO> Handle(GetBookFundQuery request, CancellationToken cancellationToken)
        {
            var bookFund = await bookFundService.GetAsync(request.Id);
            return MapToBookFundDTO(bookFund);
        }

        private BookFundDTO MapToBookFundDTO(BookFund bookFund)
        {
            return new BookFundDTO()
            {
                Amount = bookFund.Amount,
                Book =
                {
                    AmountPage = bookFund.Book.AmountPage,
                    Title = bookFund.Book.Title,
                    Year = bookFund.Book.Year,
                    Author = new AuthorDTO()
                    {
                        Name = bookFund.Book.Author.Name,
                        Surname = bookFund.Book.Author.Surname,
                        Patronymic = bookFund.Book.Author.Patronymic
                    },
                    Genre = new GenreDTO()
                    {
                        Name = bookFund.Book.Genre.Name
                    },
                    Publisher = new PublisherDTO()
                    {
                        Name = bookFund.Book.Publisher.Name
                    }
                },
                Library =
                {
                    Address = bookFund.Library.Address,
                    Name = bookFund.Library.Name,
                    Telephone = bookFund.Library.Telephone
                }
            };
        }
    }
}