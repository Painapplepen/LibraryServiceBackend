using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.AuthorCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Author
{
    public class UpdateAuthorValidator : AuthorValidatorBase<UpdateAuthorCommand, Response>
    {
        private readonly IAuthorService authorService;

        public UpdateAuthorValidator(IAuthorService authorService) : base()
        {
            this.authorService = authorService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.AuthorNotFound);
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await authorService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}
