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
        private readonly IBookViewService bookViewService;
        public GetAllBookQueryHandler(IBookViewService bookViewService)
        {
            this.bookViewService = bookViewService;
        }

        public async Task<IReadOnlyCollection<FoundBookDTO>> Handle(GetAllBookQuery request,
            CancellationToken cancellationToken)
        {
            var books = await bookViewService.GetAllAsync(cancellationToken);

            return books.Select(MapToFoundBookDTO).ToArray();
        }

        private FoundBookDTO MapToFoundBookDTO(BookView book)
        {
            return new FoundBookDTO
            {
                Id = book.Id,
                ISBN = book.ISBN,
                AmountPage = book.AmountPage,
                Title = book.Title,
                Year = book.Year,
                AuthorName = book.AuthorName,
                AuthorSurname = book.AuthorSurname,
                AuthorPatronymic = book.AuthorPatronymic,
                Genre = book.Genre,
                Publisher = book.Publisher

            };
        }

    }
}