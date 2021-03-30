using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.PublisherCommands
{
    public class UpdatePublisherCommand : PublisherCommandBase<PublisherDTO>
    {
        public UpdatePublisherCommand(long id, PublisherDTO update) : base(id, update) { }
    }

    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, PublisherDTO>
    {
        private readonly IPublisherService publisherService;

        public UpdatePublisherCommandHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<PublisherDTO> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await publisherService.GetAsync(request.Id);

            var publisherToUpdate = MapDTOToPublisher(request.Entity, publisher);

            var updatedPublisher = await publisherService.UpdateAsync(publisherToUpdate);

            return MapToPublisherDTO(updatedPublisher);
        }

        public Publisher MapDTOToPublisher(PublisherDTO publisherDTO, Publisher publisher)
        {
            publisher.Name = publisherDTO.Name;
            return publisher;
        }

        public PublisherDTO MapToPublisherDTO(Publisher publisher)
        {
            return new PublisherDTO()
            {
                Name = publisher.Name
            };
        }
    }
}
