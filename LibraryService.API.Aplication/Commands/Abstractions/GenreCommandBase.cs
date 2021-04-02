using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class GenreCommandBase<TResponse> : IRequest<TResponse>
    {
        public GenreDTO Entity { get; set; }
        public long? Id { get; set; }

        protected GenreCommandBase(long id, GenreDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected GenreCommandBase(GenreDTO entity)
        {
            Entity = entity;
        }
    }
}
