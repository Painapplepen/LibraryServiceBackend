using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Outgoing.Author;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.AuthorQueries
{
    public class GetAllAuthorQuery : IRequest<IReadOnlyCollection<FoundAuthorDTO>>
    {
        public GetAllAuthorQuery() { }
    }

    public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, IReadOnlyCollection<FoundAuthorDTO>>
    {
        private readonly IAuthorService authorService;

        public GetAllAuthorQueryHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<IReadOnlyCollection<FoundAuthorDTO>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
        {
            var authors = await authorService.GetAllAsync(cancellationToken);

            return authors.Select(MapToFoundAuthorDTO).ToArray();
        }

        private FoundAuthorDTO MapToFoundAuthorDTO(Author author)
        {
            return new FoundAuthorDTO
            {
                Id = author.Id,
                Surname = author.Surname,
                Name = author.Name,
                Patronymic = author.Patronymic
            };
        }
    }
}