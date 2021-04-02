using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.LibraryCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Library
{
    public class UpdateLibraryValidator : LibraryValidatorBase<UpdateLibraryCommand, Response>
    {
        private readonly ILibraryService libraryService;

        public UpdateLibraryValidator(ILibraryService libraryService) : base()
        {
            this.libraryService = libraryService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.LibraryNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await libraryService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}
