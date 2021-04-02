using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Application.Queries.GenreQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Genre;
using LibraryService.API.Contracts.Outgoing.Publisher;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.PublisherQueries
{
    public class SearchPublisherQuery : PagedSearchQuery<FoundPublisherDTO, PublisherSearchCondition>
    {
        public SearchPublisherQuery(PublisherSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchPublisherQueryHandler : IRequestHandler<SearchPublisherQuery, PagedResponse<FoundPublisherDTO>>
    {
        private readonly IPublisherService publisherService;

        public SearchPublisherQueryHandler(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        public async Task<PagedResponse<FoundPublisherDTO>> Handle(SearchPublisherQuery request, CancellationToken cancellationToken)
        {
            PublisherSearchCondition searchCondition = new PublisherSearchCondition()
            {
                Name = GetFilterValues(request.SearchCondition.Name),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Publisher> foundPublisher = await publisherService.FindAsync(searchCondition, sortProperty);
            FoundPublisherDTO[] mappedPublisher = foundPublisher.Select(MapToFoundPublisherDTO).ToArray();
            var totalCount = await publisherService.CountAsync(searchCondition);

            return new PagedResponse<FoundPublisherDTO>
            {
                Items = mappedPublisher,
                TotalCount = totalCount
            };
        }

        private FoundPublisherDTO MapToFoundPublisherDTO(Publisher publisher)
        {
            return new FoundPublisherDTO
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        private string[] GetFilterValues(ICollection<string> values)
        {
            return values == null
                       ? Array.Empty<string>()
                       : values.Select(v => v.Trim()).Distinct().ToArray();
        }

        protected string GetSortProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return nameof(Publisher.Id);
            }

            if (propertyName.Equals("Publsher", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Publisher.Name);
            }

            return propertyName;
        }
    }
}
