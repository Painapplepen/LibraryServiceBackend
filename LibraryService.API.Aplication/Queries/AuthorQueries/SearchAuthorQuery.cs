using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.AuthorQueries
{
    public class SearchAuthorQuery : PagedSearchQuery<FoundAuthorDTO, AuthorSearchCondition>
    {
        public SearchAuthorQuery(AuthorSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchAuthorQueryHandler : IRequestHandler<SearchAuthorQuery, PagedResponse<FoundAuthorDTO>>
    {
        private readonly IAuthorService authorService;

        public SearchAuthorQueryHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<PagedResponse<FoundAuthorDTO>> Handle(SearchAuthorQuery request, CancellationToken cancellationToken)
        {
            AuthorSearchCondition searchCondition = new AuthorSearchCondition()
            {
                Name = GetFilterValues(request.SearchCondition.Name),
                Surname = GetFilterValues(request.SearchCondition.Surname),
                Patronymic = GetFilterValues(request.SearchCondition.Patronymic),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Author> foundAuthor = await authorService.FindAsync(searchCondition, sortProperty);
            FoundAuthorDTO[] mappedAuthor = foundAuthor.Select(MapToFoundAuthor).ToArray();
            var totalCount = await authorService.CountAsync(searchCondition);

            return new PagedResponse<FoundAuthorDTO>
            {
                Items = mappedAuthor,
                TotalCount = totalCount
            };
        }

        public FoundAuthorDTO MapToFoundAuthor(Author author)
        {
            return new FoundAuthorDTO
            {
                Id = author.Id,
                Surname = author.Surname,
                Name = author.Name,
                Patronymic = author.Patronymic
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
                return nameof(Author.Id);
            }

            if (propertyName.Equals("authorName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Name);
            }

            if (propertyName.Equals("authorSurName", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Surname);
            }

            if (propertyName.Equals("authorPatronymic", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Author.Patronymic);
            }

            return propertyName;
        }
    }
}