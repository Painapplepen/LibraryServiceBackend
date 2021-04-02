using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.LibraryQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Library
{
    public class GetLibraryValidator : AbstractValidator<GetLibraryQuery>
    {
        private readonly ILibraryService libraryService;

        public GetLibraryValidator(ILibraryService libraryService)
        {
            this.libraryService = libraryService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.LibraryNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await libraryService.ExistsAsync(id, cancellationToken);
        }
    }
}
