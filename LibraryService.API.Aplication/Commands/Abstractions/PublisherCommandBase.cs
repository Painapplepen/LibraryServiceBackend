using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class PublisherCommandBase<TResponse> : IRequest<TResponse>
    {
        public PublisherDTO Entity { get; set; }
        public long Id { get; set; }

        protected PublisherCommandBase(long id, PublisherDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected PublisherCommandBase(PublisherDTO entity)
        {
            Entity = entity;
        }
    }
}

