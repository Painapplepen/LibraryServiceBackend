using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.GenreCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Genre
{
    public class UpdateGenreValidator : GenreValidatorBase<UpdateGenreCommand, Response>
    {
        private readonly IGenreService genreService;

        public UpdateGenreValidator(IGenreService genreService) : base()
        {
            this.genreService = genreService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.GenreNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await genreService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}
