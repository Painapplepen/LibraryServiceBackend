using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Commands.Abstractions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Commands.AdminCommands
{
    public class UpdateAdminCommand : AdminCommandBase<AdminDTO>
    {
        public UpdateAdminCommand(long id, AdminDTO update) : base(id, update) { }
    }

    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, AdminDTO>
    {
        private readonly IAdminService adminService;

        public UpdateAdminCommandHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<AdminDTO> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = await adminService.GetAsync(request.Id, cancellationToken);

            var adminToUpdate = MapDTOToAdmin(request.Entity, admin);

            var updatedAdmin = await adminService.UpdateAsync(adminToUpdate);

            return MapToAdminDTO(updatedAdmin);
        }

        public Admin MapDTOToAdmin(AdminDTO adminDTO, Admin admin)
        {
            admin.Login = adminDTO.Login;
            admin.Password = adminDTO.Password;
            return admin;
        }

        public AdminDTO MapToAdminDTO(Admin admin)
        {
            return new AdminDTO()
            {
                Login = admin.Login,
                Password = admin.Password
            };
        }
    }
}