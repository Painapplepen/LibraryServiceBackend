using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.GenreQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Genre
{
    public class GetGenreValidator : AbstractValidator<GetGenreQuery>
    {
        private readonly IGenreService genreService;

        public GetGenreValidator(IGenreService genreService)
        {
            this.genreService = genreService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));
            
            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.GenreNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await genreService.ExistsAsync(id, cancellationToken);
        }
    }
}
