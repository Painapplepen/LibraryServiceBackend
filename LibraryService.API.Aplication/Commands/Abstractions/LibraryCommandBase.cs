using LibraryService.API.Contracts.IncomingOutgoing.Library;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class LibraryCommandBase<TResponse> : IRequest<TResponse>
    {
        public LibraryDTO Entity { get; set; }
        public long? Id { get; set; }

        protected LibraryCommandBase(long id, LibraryDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected LibraryCommandBase(LibraryDTO entity)
        {
            Entity = entity;
        }
    }
}
