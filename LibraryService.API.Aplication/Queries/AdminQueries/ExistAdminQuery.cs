using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Admin;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.AdminQueries
{
    public class ExistAdminQuery : AdminCommandBase<AdminDTO>
    {
        public ExistAdminQuery(AdminDTO admin) : base(admin)
        { }
    }

    public class ExistAdminQueryHandler : IRequestHandler<ExistAdminQuery, AdminDTO>
    {
        private readonly IAdminService adminService;

        public ExistAdminQueryHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<AdminDTO> Handle(ExistAdminQuery request, CancellationToken cancellationToken)
        {
            var admin = await adminService.ExistAsync(request.Entity);

            if (admin == null)
            {
                return null;
            }

            return MapToAdminDTO(admin);
        }

        public AdminDTO MapToAdminDTO(Admin admin)
        {
            return new AdminDTO
            {
                Login = admin.Login,
                Password = admin.Password
            };
        }
    }
}