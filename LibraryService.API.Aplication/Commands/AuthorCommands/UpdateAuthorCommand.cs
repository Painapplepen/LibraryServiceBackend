using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.AuthorCommands
{
    public class UpdateAuthorCommand : AuthorCommandBase<Response>
    {
        public UpdateAuthorCommand(long id, AuthorDTO update) : base(id, update) { }
    }

    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Response>
    {
        private readonly IAuthorService authorService;

        public UpdateAuthorCommandHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<Response> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await authorService.GetAsync(request.Id);

            if (author == null)
            {
                return Response.Error;
            }

            var authorToUpdate = MapDTOToAuthor(request.Entity, author);

            var updatedAuthor = await authorService.UpdateAsync(authorToUpdate);

            if (updatedAuthor == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        public Author MapDTOToAuthor(AuthorDTO authorDTO, Author author)
        {
            author.Surname = authorDTO.Surname;
            author.Name = authorDTO.Name;
            author.Patronymic = authorDTO.Patronymic;
            return author;
        }

    }
}