using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.GenreCommands
{
    public class AddGenreCommand : GenreCommandBase<long>
    {
        public AddGenreCommand(GenreDTO genre) : base(genre) { }
    }

    public class AddGenreCommandHandler : IRequestHandler<AddGenreCommand, long>
    {
        private readonly IGenreService genreService;

        public AddGenreCommandHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<long> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = MapToGenre(request.Entity);
            var insertedGenre = await genreService.InsertAsync(genre);
            return insertedGenre.Id;
        }

        private Genre MapToGenre(GenreDTO genre)
        {
            return new Genre
            {
                Name = genre.Name,
            };
        }
    }
}
