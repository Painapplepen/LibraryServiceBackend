using FluentValidation;
using LibraryService.API.Application.Commands.Abstractions;

namespace LibraryService.API.Application.Validation.Abstractions
{
    public class BookFundValidatorBase<TCommand, TResponse> : AbstractValidator<TCommand>
        where TCommand : BookFundCommandBase<TResponse>
    {
        public BookFundValidatorBase()
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