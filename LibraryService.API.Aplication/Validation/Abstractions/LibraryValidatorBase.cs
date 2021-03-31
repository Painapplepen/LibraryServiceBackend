using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class LibraryValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : LibraryCommandBase<TResponse>
    {
        public LibraryValidatorBase()
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