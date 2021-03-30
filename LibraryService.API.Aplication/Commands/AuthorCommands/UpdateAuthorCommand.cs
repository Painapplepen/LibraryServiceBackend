using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.AuthorCommands
{
    public class UpdateAuthorCommand : AuthorCommandBase<AuthorDTO>
    {
        public UpdateAuthorCommand(long id, AuthorDTO update) : base(id, update) { }
    }

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorDTO>
    {
        private readonly IAuthorService authorService;

        public UpdateAuthorCommandHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<AuthorDTO> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await authorService.GetAsync(request.Id);

            var authorToUpdate = MapDTOToAuthor(request.Entity, author);

            var updatedAuthor = await authorService.UpdateAsync(authorToUpdate);

            return MapToAuthorDTO(updatedAuthor);
        }

        public Author MapDTOToAuthor(AuthorDTO authorDTO, Author author)
        {
            author.Surname = authorDTO.Surname;
            author.Name = authorDTO.Name;
            author.Patronymic = authorDTO.Patronymic;
            return author;
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