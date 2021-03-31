using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Application.Queries.AuthorQueries;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.API.Contracts.Outgoing.Library;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.LibraryQueries
{
    public class SearchLibraryQuery : PagedSearchQuery<FoundLibraryDTO, LibrarySearchCondition>
    {
        public SearchLibraryQuery(LibrarySearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchLibraryQueryHandler : IRequestHandler<SearchLibraryQuery, PagedResponse<FoundLibraryDTO>>
    {
        private readonly ILibraryService libraryService;

        public SearchLibraryQueryHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<PagedResponse<FoundLibraryDTO>> Handle(SearchLibraryQuery request, CancellationToken cancellationToken)
        {
            LibrarySearchCondition searchCondition = new LibrarySearchCondition()
            {
                Name = GetFilterValues(request.SearchCondition.Name),
                Address = GetFilterValues(request.SearchCondition.Address),
                Telephone = GetFilterValues(request.SearchCondition.Telephone),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Library> foundLibrary = await libraryService.FindAsync(searchCondition, sortProperty);
            FoundLibraryDTO[] mappedLibrary = foundLibrary.Select(MapToFoundLibraryDTO).ToArray();
            var totalCount = await libraryService.CountAsync(searchCondition);

            return new PagedResponse<FoundLibraryDTO>
            {
                Items = mappedLibrary,
                TotalCount = totalCount
            };
        }

        private FoundLibraryDTO MapToFoundLibraryDTO(Library library)
        {
            return new FoundLibraryDTO
            {
                Id = library.Id,
                Address = library.Address,
                Name = library.Name,
                Telephone = library.Telephone
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
                return nameof(Library.Id);
            }

            if (propertyName.Equals("libraryName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Name);
            }

            if (propertyName.Equals("libraryTelephone", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Telephone);
            }

            if (propertyName.Equals("libraryAddress", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Library.Address);
            }

            return propertyName;
        }
    }
}
