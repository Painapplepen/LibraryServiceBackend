using LibraryService.API.Contracts.IncomingOutgoing.Book;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class BookCommandBase<TResponse> : IRequest<TResponse>
    {
        public BookDTO Entity { get; set; }
        public long Id { get; set; }

        protected BookCommandBase(long id, BookDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected BookCommandBase(BookDTO entity)
        {
            Entity = entity;
        }
    }
}
