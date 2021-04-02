using LibraryService.API.Application.Commands.PublisherCommands;
using LibraryService.API.Application.Validation.Abstractions;

namespace LibraryService.API.Application.Validation.Publisher
{
    public class AddPublisherValidator : PublisherValidatorBase<AddPublisherCommand, long>
    {
        public AddPublisherValidator() : base() { }
    }
}
