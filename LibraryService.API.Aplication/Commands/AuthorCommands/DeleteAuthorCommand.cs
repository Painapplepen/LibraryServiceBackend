using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.AuthorCommands
{
    public class DeleteAuthorCommand : IRequest
    {
        public long Id { get; }

        public DeleteAuthorCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAuthorService authorService;

        public DeleteAuthorCommandHandler(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            await authorService.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}