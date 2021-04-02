using LibraryService.API.Contracts.IncomingOutgoing.Author;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class AuthorCommandBase<TResponse> : IRequest<TResponse>
    {
        public AuthorDTO Entity { get; set; }
        public long? Id { get; set; }

        protected AuthorCommandBase(long id, AuthorDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected AuthorCommandBase(AuthorDTO entity)
        {
            Entity = entity;
        }
    }
}