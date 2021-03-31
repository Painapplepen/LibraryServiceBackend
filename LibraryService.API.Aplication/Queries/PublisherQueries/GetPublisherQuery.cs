using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Publisher;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.PublisherQueries
{
    public class GetPublisherQuery : IRequest<PublisherDTO>
    {
        public long Id { get; }

        public GetPublisherQuery(long id)
        {
            Id = id;
        }
    }

    public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, PublisherDTO>
    {
        private readonly IPublisherService publisherService;

        public GetPublisherQueryHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<PublisherDTO> Handle(GetPublisherQuery request, CancellationToken cancellationToken)
        {
            var publisher = await publisherService.GetAsync(request.Id, cancellationToken);

            if (publisher == null)
            {
                return null;
            }

            return MapToPublisherDTO(publisher);
        }

        private PublisherDTO MapToPublisherDTO(Publisher publisher)
        {
            return new PublisherDTO()
            {
                Name = publisher.Name
            };
        }
    }
}