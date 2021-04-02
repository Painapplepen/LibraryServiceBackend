using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.BookFundCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.BookFund
{
    public class DeleteBookFundValidator : AbstractValidator<DeleteBookFundCommand>
    {
        private IBookFundService bookFundService;

        public DeleteBookFundValidator(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.BookFundNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await bookFundService.ExistsAsync(id, cancellationToken);
        }
    }
}
