using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class PublisherValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : PublisherCommandBase<TResponse>
    {
        public PublisherValidatorBase()
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
                .WithMessage(Resources.Resources.PublisherNameRequired);
        }

        private bool NotBeNullOrWhitespace(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}