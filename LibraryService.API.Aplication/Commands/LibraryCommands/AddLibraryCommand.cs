using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.LibraryCommands
{
    public class AddLibraryCommand : LibraryCommandBase<long>
    {
        public AddLibraryCommand(LibraryDTO library) : base(library) { }
    }

    public class AddLibraryCommandHandler : IRequestHandler<AddLibraryCommand, long>
    {
        private readonly ILibraryService libraryService;

        public AddLibraryCommandHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<long> Handle(AddLibraryCommand request, CancellationToken cancellationToken)
        {
            var library = MapToLibrary(request.Entity);
            var insertedLibrary = await libraryService.InsertAsync(library);
            return insertedLibrary.Id;
        }

        private Library MapToLibrary(LibraryDTO library)
        {
            return new Library
            {
                Name = library.Name,
                Address = library.Address,
                Telephone = library.Telephone
            };
        }
    }
}