using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Genre;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.GenreQueries
{
    public class SearchGenreQuery : PagedSearchQuery<FoundGenreDTO, GenreSearchCondition>
    {
        public SearchGenreQuery(GenreSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchAuthorQueryHandler : IRequestHandler<SearchGenreQuery, PagedResponse<FoundGenreDTO>>
    {
        private readonly IGenreService genreService;

        public SearchAuthorQueryHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<PagedResponse<FoundGenreDTO>> Handle(SearchGenreQuery request, CancellationToken cancellationToken)
        {
            GenreSearchCondition searchCondition = new GenreSearchCondition()
            {
                Name = GetFilterValues(request.SearchCondition.Name),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Genre> foundGenre = await genreService.FindAsync(searchCondition, sortProperty);
            FoundGenreDTO[] mappedGenre = foundGenre.Select(MapToFoundGenre).ToArray();
            var totalCount = await genreService.CountAsync(searchCondition);

            return new PagedResponse<FoundGenreDTO>
            {
                Items = mappedGenre,
                TotalCount = totalCount
            };
        }

        public FoundGenreDTO MapToFoundGenre(Genre genre)
        {
            return new FoundGenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
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
                return nameof(Genre.Id);
            }

            if (propertyName.Equals("Genre", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Genre.Name);
            }

            return propertyName;
        }
    }
}
