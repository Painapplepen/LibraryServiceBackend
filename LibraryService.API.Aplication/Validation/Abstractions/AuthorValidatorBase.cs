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

            RuleFor(cmd => cmd.Entity.Name)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.AuthorNameRequired);

            RuleFor(cmd => cmd.Entity.Surname)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.AuthorSurnameRequired);

            RuleFor(cmd => cmd.Entity.Patronymic)
                .Must(NotBeNullOrWhitespace)
                .WithMessage(Resources.Resources.AuthorPatronymicRequired);
        }

        private bool NotBeNullOrWhitespace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}