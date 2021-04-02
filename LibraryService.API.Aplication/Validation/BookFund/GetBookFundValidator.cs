using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.BookFundQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.BookFund
{
    public class GetBookFundValidator : AbstractValidator<GetBookFundQuery>
    {
        private readonly IBookFundService bookFundService;

        public GetBookFundValidator(IBookFundService bookFundService)
        {
            this.bookFundService = bookFundService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.BookFundNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await bookFundService.ExistsAsync(id, cancellationToken);
        }
    }
}
