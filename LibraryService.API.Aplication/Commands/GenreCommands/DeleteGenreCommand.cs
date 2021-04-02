using System.Threading;
using System.Threading.Tasks;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.GenreCommands
{
    public class DeleteGenreCommand : IRequest
    {
        public long Id { get; }

        public DeleteGenreCommand(long id)
        {
            Id = id;
        }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreService genreService;

        public DeleteGenreCommandHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            await genreService.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
