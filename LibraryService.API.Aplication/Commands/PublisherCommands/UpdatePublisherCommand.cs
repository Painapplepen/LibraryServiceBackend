using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.PublisherCommands
{
    public class UpdatePublisherCommand : PublisherCommandBase<Response>
    {
        public UpdatePublisherCommand(long id, PublisherDTO update) : base(id, update) { }
    }

    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Response>
    {
        private readonly IPublisherService publisherService;

        public UpdatePublisherCommandHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<Response> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await publisherService.GetAsync(request.Id.Value, cancellationToken);

            if (publisher == null)
            {
                return Response.Error;
            }

            var publisherToUpdate = MapDTOToPublisher(request.Entity, publisher);

            var updatedPublisher = await publisherService.UpdateAsync(publisherToUpdate);

            if (updatedPublisher == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        public Publisher MapDTOToPublisher(PublisherDTO publisherDTO, Publisher publisher)
        {
            publisher.Name = publisherDTO.Name;
            return publisher;
        }
    }
}
