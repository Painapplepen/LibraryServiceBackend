using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.LibraryCommands
{
    public class DeleteLibraryCommand : IRequest
    {
        public long Id { get; }

        public DeleteLibraryCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand>
    {
        private readonly ILibraryService libraryService;

        public DeleteLibraryCommandHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<Unit> Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
        {
            await libraryService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}