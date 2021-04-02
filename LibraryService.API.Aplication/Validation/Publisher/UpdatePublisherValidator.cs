using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.PublisherCommands;
using LibraryService.API.Application.Validation.Abstractions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Publisher
{
    public class UpdatePublisherValidator : PublisherValidatorBase<UpdatePublisherCommand, Response>
    {
        private readonly IPublisherService publisherService;

        public UpdatePublisherValidator(IPublisherService publisherService) : base()
        {
            this.publisherService = publisherService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
               .NotNull()
               .WithMessage(cmd => string.Format(Resources.Resources.ValueRequired, nameof(cmd.Id)));

            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.PublisherNotFound);
        }

        private async Task<bool> Exist(long? id, CancellationToken cancellationToken)
        {
            return id.HasValue && await publisherService.ExistsAsync(id.Value, cancellationToken);
        }
    }
}
