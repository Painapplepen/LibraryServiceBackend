using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.LibraryCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Library
{
    public class DeleteLibraryValidator : AbstractValidator<DeleteLibraryCommand>
    {
        private ILibraryService libraryService;

        public DeleteLibraryValidator(ILibraryService libraryService)
        {
            this.libraryService = libraryService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.LibraryNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await libraryService.ExistsAsync(id, cancellationToken);
        }
    }
}
