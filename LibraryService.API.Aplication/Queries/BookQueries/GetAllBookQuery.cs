using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.BookQueries
{
    public class GetAllBookQuery : IRequest<IReadOnlyCollection<FoundBookDTO>>
    {
        public GetAllBookQuery()
        {
        }
    }

    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, IReadOnlyCollection<FoundBookDTO>>
    {
        private readonly IBookService bookService;
        private readonly IPublisherService publisherService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        public GetAllBookQueryHandler(IBookService bookService,
            IPublisherService publisherService,
            IAuthorService authorService,
            IGenreService genreService)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
        }

        public async Task<IReadOnlyCollection<FoundBookDTO>> Handle(GetAllBookQuery request,
            CancellationToken cancellationToken)
        {
            var books = await bookService.GetAllAsync(cancellationToken);

            return books.Select(MapToFoundBookDTO).ToArray();
        }

        private FoundBookDTO MapToFoundBookDTO(Book book)
        {
            CancellationToken cancellationToken = default;
            var author = authorService.GetAsync(book.AuthorId, cancellationToken).Result;
            var genre = genreService.GetAsync(book.GenreId, cancellationToken).Result;
            var publisher = publisherService.GetAsync(book.PublisherId, cancellationToken).Result;
            return new FoundBookDTO
            {
                Id = book.Id,
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
            };
        }

    }
}