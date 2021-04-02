using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.PublisherCommands
{
    public class AddPublisherCommand : PublisherCommandBase<long>
    {
        public AddPublisherCommand(PublisherDTO publisher) : base(publisher) { }
    }

    public class AddPublisherCommandHandler : IRequestHandler<AddPublisherCommand, long>
    {
        private readonly IPublisherService publisherService;

        public AddPublisherCommandHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<long> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = MapToPublisher(request.Entity);
            var insertedPublisher = await publisherService.InsertAsync(publisher);
            return insertedPublisher.Id;
        }

        private Publisher MapToPublisher(PublisherDTO publisher)
        {
            return new Publisher
            {
                Name = publisher.Name,
            };
        }
    }
}
