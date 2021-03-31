using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.AuthorQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Author
{
    public class GetAuthorValidator : AbstractValidator<GetAuthorQuery>
    {
        private readonly IAuthorService authorService;

        public GetAuthorValidator(IAuthorService authorService)
        {
            this.authorService = authorService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.AuthorNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await authorService.ExistsAsync(id, cancellationToken);
        }
    }
}
