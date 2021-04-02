using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.GenreCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Genre
{
    public class DeleteGenreValidator : AbstractValidator<DeleteGenreCommand>
    {
        private IGenreService genreService;

        public DeleteGenreValidator(IGenreService genreService)
        {
            this.genreService = genreService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.GenreNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await genreService.ExistsAsync(id, cancellationToken);
        }
    }
}
