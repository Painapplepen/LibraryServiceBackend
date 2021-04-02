using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.BookFundCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.BookFund
{
    public class UpdateBookFundValidator : BookFundValidatorBase<UpdateBookFundCommand, Response>
    {
        private readonly IBookFundService bookFundService;

        public UpdateBookFundValidator(IBookFundService bookFundService, 
            IBookService bookService, 
            ILibraryService libraryService) : base(bookService, libraryService)
        {
            this.bookFundService = bookFundService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(cmd => string.Format(Resources.Resources.BookFundNotFound, cmd.Id));
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await bookFundService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}
