using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
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
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;
        public GetBookQueryHandler(IBookService bookService, 
                                    IAuthorService authorService, 
                                    IGenreService genreService, 
                                    IPublisherService publisherService)
        {
            this.bookService = bookService;
            this.publisherService = publisherService;
            this.genreService = genreService;
            this.authorService = authorService;
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
            var author = await authorService.GetAsync(book.AuthorId);
            var genre = await genreService.GetAsync(book.GenreId);
            var publisher = await genreService.GetAsync(book.PublisherId);
            return new BookDTO()
            {
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                Author = new AuthorDTO()
                {
                    Name = author.Name,
                    Surname = author.Surname,
                    Patronymic = author.Patronymic
                },
                Genre = new GenreDTO()
                {
                    Name = genre.Name
                },
                Publisher = new PublisherDTO()
                {
                    Name = publisher.Name
                }
            };
        }
    }
}