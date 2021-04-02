using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.API.Application.Commands.PublisherCommands;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Validation.Publisher
{
    public class DeletePublisherValidator : AbstractValidator<DeletePublisherCommand>
    {
        private IPublisherService publisherService;

        public DeletePublisherValidator(IPublisherService publisherService)
        {
            this.publisherService = publisherService;

            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(cmd => cmd.Id)
                .MustAsync(Exist)
                .WithMessage(Resources.Resources.GenreNotFound);
        }

        private async Task<bool> Exist(long id, CancellationToken cancellationToken)
        {
            return await publisherService.ExistsAsync(id, cancellationToken);
        }
    }
}
