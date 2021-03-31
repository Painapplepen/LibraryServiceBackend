using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
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

        public GetAllBookQueryHandler(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IReadOnlyCollection<FoundBookDTO>> Handle(GetAllBookQuery request,
            CancellationToken cancellationToken)
        {
            var books = await bookService.GetAllAsync(cancellationToken);

            return books.Select(MapToFoundBookDTO).ToArray();
        }

        private FoundBookDTO MapToFoundBookDTO(Book book)
        {
            //var author = authorService.Get(book.AuthorId);
            //var genre = genreService.Get(book.GenreId);
            //var publisher = publisherService.Get(book.PublisherId);
            return new FoundBookDTO
            {
                Id = book.Id,
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                //Author =
                //{
                //    Id = book.AuthorId,
                //    Name = author.Name,
                //    Surname = author.Surname,
                //    Patronymic = author.Patronymic
                //},
                //Genre =
                //{
                //    Id = book.GenreId,
                //    Name = genre.Name
                //},
                //Publisher =
                //{
                //    Id = book.PublisherId,
                //    Name = publisher.Name
                //}
            };
        }

    }
}