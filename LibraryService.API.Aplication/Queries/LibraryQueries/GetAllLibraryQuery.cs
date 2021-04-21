using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Outgoing.Library;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.LibraryQueries
{
    public class GetAllLibraryQuery : IRequest<IReadOnlyCollection<FoundLibraryDTO>>
    {
        public GetAllLibraryQuery()
        {
        }
    }

    public class GetAllLibraryQueryHandler : IRequestHandler<GetAllLibraryQuery, IReadOnlyCollection<FoundLibraryDTO>>
    {
        private readonly ILibraryService libraryService;

        public GetAllLibraryQueryHandler(ILibraryService libraryService)
        {
            this.libraryService = libraryService;
        }

        public async Task<IReadOnlyCollection<FoundLibraryDTO>> Handle(GetAllLibraryQuery request,
            CancellationToken cancellationToken)
        {
            var libraries = await libraryService.GetAllAsync(cancellationToken);

            return libraries.Select(MapToFoundLibraryDTO).ToArray();
        }

        private FoundLibraryDTO MapToFoundLibraryDTO(Library library)
        {
            return new FoundLibraryDTO
            {
                Id = library.Id,
                Address = library.Address,
                Name = library.Name,
                Telephone = library.Telephone
            };
        }
    }
}