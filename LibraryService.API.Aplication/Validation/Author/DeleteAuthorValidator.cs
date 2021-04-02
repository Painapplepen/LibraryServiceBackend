using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.AuthorCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Author
{
    public class DeleteAuthorValidator : AbstractValidator<DeleteAuthorCommand>
    {
        private IAuthorService authorService;

        public DeleteAuthorValidator(IAuthorService authorService)
        {
            this.authorService = authorService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.AuthorNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await authorService.ExistsAsync(id, cancellationToken);
        }
    }
}
