using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Admin;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.AdminQueries
{
    public class ExistAdminQuery : IRequest<string>
    {
        public AdminDTO Entity { get; set; }

        public ExistAdminQuery(AdminDTO admin)
        {
            Entity = admin;
        }
    }

    public class ExistAdminQueryHandler : IRequestHandler<ExistAdminQuery, string>
    {
        private readonly IAdminService adminService;

        public ExistAdminQueryHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<string> Handle(ExistAdminQuery request, CancellationToken cancellationToken)
        {
            return await adminService.ExistAsync(request.Entity);
        }
    }
}