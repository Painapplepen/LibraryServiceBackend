using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Queries.PublisherQueries;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Publisher
{
    public class GetPublisherValidator : AbstractValidator<GetPublisherQuery>
    {
        private readonly IPublisherService publisherService;

        public GetPublisherValidator(IPublisherService publisherService)
        {
            this.publisherService = publisherService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(query => query.Id)
                .NotNull()
                .WithMessage(query => string.Format(Resources.Resources.ValueRequired, nameof(query.Id)));

            RuleFor(query => query.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.PublisherNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await publisherService.ExistsAsync(id, cancellationToken);
        }
    }
}
