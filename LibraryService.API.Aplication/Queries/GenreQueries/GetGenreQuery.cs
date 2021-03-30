using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Queries.BookQueries;
using LibraryService.API.Contracts.IncomingOutgoing.Book;
using LibraryService.API.Contracts.IncomingOutgoing.Genre;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.GenreQueries
{
    public class GetGenreQuery : IRequest<GenreDTO>
    {
        public long Id { get; }

        public GetGenreQuery(long id)
        {
            Id = id;
        }
    }

    public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, GenreDTO>
    {
        private readonly IGenreService genreService;

        public GetGenreQueryHandler(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        public async Task<GenreDTO> Handle(GetGenreQuery request, CancellationToken cancellationToken)
        {
            var genre = await genreService.GetAsync(request.Id);
            return MapToGenreDTO(genre);
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