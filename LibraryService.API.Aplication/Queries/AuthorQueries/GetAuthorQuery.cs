using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.AuthorQueries
{
    public class GetAuthorQuery : IRequest<AuthorDTO>
    {
        public long Id { get; }

        public GetAuthorQuery(long id)
        {
            Id = id;
        }
    }

    public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, AuthorDTO>
    {
        private readonly IAuthorService authorService;

        public GetAuthorQueryHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<AuthorDTO> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await authorService.GetAsync(request.Id);

            return MapToAuthorDTO(author);
        }

        public AuthorDTO MapToAuthorDTO(Author author)
        {
            return new AuthorDTO()
            {
                Surname = author.Surname,
                Name = author.Name,
                Patronymic = author.Patronymic
            };
        }
    }
}