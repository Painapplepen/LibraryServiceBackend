using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class BookValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BookCommandBase<TResponse>
    {
        public BookValidatorBase()
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