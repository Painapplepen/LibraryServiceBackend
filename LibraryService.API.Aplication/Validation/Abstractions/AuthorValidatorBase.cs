using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class AuthorValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : AuthorCommandBase<TResponse>
    {
        public AuthorValidatorBase()
        {
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Entity)
                .NotNull()
                .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Entity)));
        }
    }
}