﻿using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using MediatR;

namespace LibraryService.API.Application.Commands.Abstractions
{
    public class PublisherCommandBase<TResponse> : IRequest<TResponse>
    {
        public PublisherFoundDTO Entity { get; set; }
        public long Id { get; set; }

        protected PublisherCommandBase(long id, PublisherFoundDTO entity)
        {
            Id = id;
            Entity = entity;
        }

        protected PublisherCommandBase(PublisherFoundDTO entity)
        {
            Entity = entity;
        }
    }
}

