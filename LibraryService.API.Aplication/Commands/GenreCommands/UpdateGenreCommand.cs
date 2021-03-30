using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.GenreCommands
{
    public class UpdateGenreCommand : GenreCommandBase<GenreDTO>
    {
        public UpdateGenreCommand(long id, GenreDTO update) : base(id, update) { }
    }

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, GenreDTO>
    {
        private readonly IGenreService genreService;

        public UpdateGenreCommandHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<GenreDTO> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await genreService.GetAsync(request.Id);

            var genreToUpdate = MapDTOToGenre(request.Entity, genre);

            var updatedGenre = await genreService.UpdateAsync(genreToUpdate);

            return MapToGenreDTO(updatedGenre);
        }

        public Genre MapDTOToGenre(GenreDTO genreDTO, Genre genre)
        {
            genre.Name = genreDTO.Name;
            return genre;
        }

        public GenreDTO MapToGenreDTO(Genre genre)
        {
            return new GenreDTO()
            {
                Name = genre.Name
            };
        }
    }
}
