using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Outgoing.Publisher;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.PublisherQueries
{
    public class GetAllPublisherQuery : IRequest<IReadOnlyCollection<FoundPublisherDTO>>
    {
        public GetAllPublisherQuery()
        {
        }
    }

    public class GetAllLibraryQueryHandler : IRequestHandler<GetAllPublisherQuery, IReadOnlyCollection<FoundPublisherDTO>>
    {
        private readonly IPublisherService publisherService;

        public GetAllLibraryQueryHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<IReadOnlyCollection<FoundPublisherDTO>> Handle(GetAllPublisherQuery request,
            CancellationToken cancellationToken)
        {
            var publishers = await publisherService.GetAllAsync(cancellationToken);

            return publishers.Select(MapToFoundPublisherDTO).ToArray();
        }

        private FoundPublisherDTO MapToFoundPublisherDTO(Publisher publisher)
        {
            return new FoundPublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }
    }
}