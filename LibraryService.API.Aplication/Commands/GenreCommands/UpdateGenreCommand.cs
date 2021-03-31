using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.GenreCommands
{
    public class UpdateGenreCommand : GenreCommandBase<Response>
    {
        public UpdateGenreCommand(long id, GenreDTO update) : base(id, update) { }
    }

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Response>
    {
        private readonly IGenreService genreService;

        public UpdateGenreCommandHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<Response> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await genreService.GetAsync(request.Id);

            if (genre == null)
            {
                return Response.Error;
            }

            var genreToUpdate = MapDTOToGenre(request.Entity, genre);

            var updatedGenre = await genreService.UpdateAsync(genreToUpdate);

            if (updatedGenre == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        public Genre MapDTOToGenre(GenreDTO genreDTO, Genre genre)
        {
            genre.Name = genreDTO.Name;
            return genre;
        }
    }
}
