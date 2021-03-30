using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.LibraryCommands
{
    public class UpdateLibraryCommand : LibraryCommandBase<LibraryDTO>
    {
        public UpdateLibraryCommand(long id, LibraryDTO update) : base(id, update) { }
    }

    public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand, LibraryDTO>
    {
        private readonly ILibraryService libraryService;

        public UpdateLibraryCommandHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<LibraryDTO> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
        {
            var library = await libraryService.GetAsync(request.Id);

            var libraryToUpdate = MapDtoToLibrary(request.Entity, library);

            var updatedLibrary = await libraryService.UpdateAsync(libraryToUpdate);

            return MapToLibraryDTO(updatedLibrary);
        }

        public Library MapDtoToLibrary(LibraryDTO libraryDTO, Library library)
        {
            library.Address = libraryDTO.Address;
            library.Name = libraryDTO.Name;
            library.Telephone = libraryDTO.Telephone;
            return library;
        }
        //Check it md this is terrible to return DTO back
        public LibraryDTO MapToLibraryDTO(Library library)
        {
            return new LibraryDTO()
            {
                Address = library.Address,
                Name = library.Name,
                Telephone = library.Telephone
            };
        }
    }
}