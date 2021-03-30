using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.IncomingOutgoing.Author;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.AuthorCommands
{
    public class AddAuthorCommand : AuthorCommandBase<long>
    {
        public AddAuthorCommand(AuthorDTO author) : base(author) { }
    }

    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, long>
    {
        private readonly IAuthorService authorService;

        public AddAuthorCommandHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<long> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = MapToAuthor(request.Entity);
            var insertedAuthor = await authorService.InsertAsync(author);
            return insertedAuthor.Id;
        }

        private Author MapToAuthor(AuthorDTO author)
        {
            return new Author
            {
                Surname = author.Surname,
                Name = author.Name,
                Patronymic = author.Patronymic
            };
        }
    }
}