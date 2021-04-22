using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.Outgoing.Admin;
using LibraryService.Data.Services;
using MediatR;

namespace LibraryService.API.Application.Queries.AdminQueries
{
    public class ExistAdminQuery : IRequest<FoundAdminDTO>
    {
        public AdminDTO Entity { get; set; }

        public ExistAdminQuery(AdminDTO admin)
        {
            Entity = admin;
        }
    }

    public class ExistAdminQueryHandler : IRequestHandler<ExistAdminQuery, FoundAdminDTO>
    {
        private readonly IAdminService adminService;

        public ExistAdminQueryHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<FoundAdminDTO> Handle(ExistAdminQuery request, CancellationToken cancellationToken)
        {
            var tokenSecurity =  await adminService.ExistAsync(request.Entity);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenSecurity);
            var tokenId =  tokenHandler.WriteToken(token);

            return MapToFoundAdminDTO(tokenId, tokenSecurity.Expires.ToString());
        }

        private FoundAdminDTO MapToFoundAdminDTO(string idToken, string expiresId)
        {
            return new FoundAdminDTO()
            {
                IdToken = idToken,
                ExpiresIn = expiresId
            };
        }
    }
}