using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.BookQueries
{
    public class GetBookQuery : IRequest<BookDTO>
    {
        public long Id { get; }

        public GetBookQuery(long id)
        {
            Id = id;
        }
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, BookDTO>
    {
        private readonly IBookService bookService;
        public GetBookQueryHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<BookDTO> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = await bookService.GetAsync(request.Id);

            if (book == null)
            {
                return null;
            }

            return await MapToBookDTO(book);
        }

        public async Task<BookDTO> MapToBookDTO(Book book)
        {
            return new BookDTO()
            {
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                PublisherId = book.PublisherId,
                GenreId = book.GenreId,
                AuthorId = book.AuthorId
            };
        }
    }
}