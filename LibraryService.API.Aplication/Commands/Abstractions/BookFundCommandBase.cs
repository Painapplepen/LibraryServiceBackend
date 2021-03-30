using LibraryService.API.Contracts.IncomingOutgoing.BookFund;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class BookFundCommandBase<TResponse> : IRequest<TResponse>
    {
        public BookFundDTO Entity { get; set; }
        public long Id { get; set; }

        protected BookFundCommandBase(long id, BookFundDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected BookFundCommandBase(BookFundDTO entity)
        {
            Entity = entity;
        }
    }
}
