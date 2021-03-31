using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Queries.AuthorQueries;
using LibraryService.API.Contracts.Outgoing.BookFund;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
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

        public GetAllBookFundQueryHandler(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;
        }

        public async Task<IReadOnlyCollection<FoundBookFundDTO>> Handle(GetAllBookFundQuery request,
            CancellationToken cancellationToken)
        {
            var bookFunds = await bookFundService.GetAllAsync(cancellationToken);

            return bookFunds.Select(MapToFoundBookFundDTO).ToArray();
        }

        private FoundBookFundDTO MapToFoundBookFundDTO(BookFund bookFund)
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
    }
}