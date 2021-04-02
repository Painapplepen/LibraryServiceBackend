using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using LibraryService.Data.Domain.Models;
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
            var bookFund = await bookFundService.GetAsync(request.Id, cancellationToken);

            if (bookFund == null)
            {
                return null;
            }

            return MapToBookFundDTO(bookFund);
        }

        private BookFundDTO MapToBookFundDTO(BookFund bookFund)
        {
            return new BookFundDTO()
            {
                Amount = bookFund.Amount,
                BookId = bookFund.BookId.Value,
                LibraryId = bookFund.LibraryId.Value
            };
        }
    }
}