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
        private readonly IBookFundViewService bookFundViewService;
        public GetAllBookFundQueryHandler(IBookFundViewService bookFundViewService)
        {
            this.bookFundViewService = bookFundViewService;
        }

        public async Task<IReadOnlyCollection<FoundBookFundDTO>> Handle(GetAllBookFundQuery request,
            CancellationToken cancellationToken)
        {
            var bookFunds = await bookFundViewService.GetAllAsync(cancellationToken);

            return bookFunds.Select(MapToFoundBookFundDTO).ToArray();
        }

        private FoundBookFundDTO MapToFoundBookFundDTO(BookFundView bookFund)
        {
            return new FoundBookFundDTO
            {
                Id = bookFund.Id,
                Amount = bookFund.Amount,
                BookAmountPage = bookFund.BookAmountPage,
                BookTitle = bookFund.BookTitle,
                BookYear = bookFund.BookYear,
                AuthorName = bookFund.AuthorName,
                AuthorSurname = bookFund.AuthorSurname,
                AuthorPatronymic = bookFund.AuthorPatronymic,
                LibraryAddress = bookFund.LibraryAddress,
                LibraryName = bookFund.LibraryName,
                LibraryTelephone = bookFund.LibraryTelephone,
                Genre = bookFund.Genre,
                Publisher = bookFund.Publisher
            };
        }
    }
}