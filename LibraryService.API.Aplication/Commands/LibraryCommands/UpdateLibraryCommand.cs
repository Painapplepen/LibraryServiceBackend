using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Commands.LibraryCommands
{
    public class UpdateLibraryCommand : LibraryCommandBase<Response>
    {
        public UpdateLibraryCommand(long id, LibraryDTO update) : base(id, update) { }
    }

    public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand, Response>
    {
        private readonly ILibraryService libraryService;

        public UpdateLibraryCommandHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<Response> Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
        {
            var library = await libraryService.GetAsync(request.Id);

            if (library == null)
            {
                return Response.Error;
            }

            var libraryToUpdate = MapDtoToLibrary(request.Entity, library);

            var updatedLibrary = await libraryService.UpdateAsync(libraryToUpdate);

            if (updatedLibrary == null)
            {
                return Response.Error;
            }

            return Response.Successful;
        }

        public Library MapDtoToLibrary(LibraryDTO libraryDTO, Library library)
        {
            library.Address = libraryDTO.Address;
            library.Name = libraryDTO.Name;
            library.Telephone = libraryDTO.Telephone;
            return library;
        }
       
    }
}