using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.API.Contracts.Outgoing.Book;
using LibraryService.API.Contracts.Outgoing.Genre;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.GenreQueries
{
    public class GetAllGenreQuery : IRequest<IReadOnlyCollection<FoundGenreDTO>>
    {
        public GetAllGenreQuery()
        {
        }
    }

    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, IReadOnlyCollection<FoundGenreDTO>>
    {
        private readonly IGenreService genreService;

        public GetAllGenreQueryHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<IReadOnlyCollection<FoundGenreDTO>> Handle(GetAllGenreQuery request,
            CancellationToken cancellationToken)
        {
            var genres = await genreService.GetAllAsync(cancellationToken);

            return genres.Select(MapToFoundGenreDTO).ToArray();
        }

        private FoundGenreDTO MapToFoundGenreDTO(Genre genre)
        {
            return new FoundGenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}