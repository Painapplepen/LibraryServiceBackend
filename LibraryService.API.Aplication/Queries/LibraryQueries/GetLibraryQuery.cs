using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Library;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.LibraryQueries
{
    public class GetLibraryQuery : IRequest<LibraryDTO>
    {
        public long Id { get; }

        public GetLibraryQuery(long id)
        {
            Id = id;
        }
    }

    public class GetLibraryQueryHandler : IRequestHandler<GetLibraryQuery, LibraryDTO>
    {
        private readonly ILibraryService libraryService;

        public GetLibraryQueryHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<LibraryDTO> Handle(GetLibraryQuery request, CancellationToken cancellationToken)
        {
            var library = await libraryService.GetAsync(request.Id, cancellationToken);

            if (library == null)
            {
                return null;
            }

            return MapToLibraryDTO(library);
        }

        private LibraryDTO MapToLibraryDTO(Library library)
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