using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;

namespace LibraryService.API.Application.Commands.AdminCommands
{
    public class AddAdminCommand : AdminCommandBase<long>
    {
        public AddAdminCommand(AdminDTO admin) : base(admin) { }
    }

    public class AddAdminCommandHandler : IRequestHandler<AddAdminCommand, long>
    {
        private readonly IAdminService adminService;

        public AddAdminCommandHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<long> Handle(AddAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = MapToAdmin(request.Entity);
            var insertedAdmin = await adminService.InsertAsync(admin);
            return insertedAdmin.Id;
        }

        private Admin MapToAdmin(AdminDTO admin)
        {
            return new Admin
            {
                Login = admin.Login,
                Password = admin.Password
            };
        }
    }
}