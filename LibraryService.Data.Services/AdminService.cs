using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibraryService.Data.Services
{
    public interface IAdminService : IBaseService<Admin>
    {
        Task<string> ExistAsync(AdminDTO admin);
        Task<IReadOnlyCollection<Admin>> FindAsync(AdminSearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(AdminSearchCondition searchCondition);
    }
    public class AdminService : BaseService<Admin> , IAdminService
    {
        private readonly LibraryServiceDbContext dbContext;
        private readonly string jwtKey = "Library service token key";

        public AdminService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> ExistAsync(AdminDTO adminDTO)
        {
            if (!await dbContext.Admins.AnyAsync(entity =>
                entity.Login == adminDTO.Login))
            {
                return null;
            }

            var admin = await dbContext.Admins.Where(entity => entity.Login == adminDTO.Login).FirstOrDefaultAsync();

            if (admin.Password == adminDTO.Password)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, adminDTO.Login)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IReadOnlyCollection<Admin>> FindAsync(AdminSearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Admin> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.Page, searchCondition.PageSize).ToListAsync();
        }

        public async Task<long> CountAsync(AdminSearchCondition searchCondition)
        {
            IQueryable<Admin> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Admin> BuildFindQuery(AdminSearchCondition searchCondition)
        {
            IQueryable<Admin> query = dbContext.Admins;

            if (searchCondition.Login.Any())
            {
                foreach (var loginPerson in searchCondition.Login)
                {
                    var upperLoginPerson = loginPerson.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Login != null && x.Login.ToUpper().Contains(upperLoginPerson));
                }
            }

            return query;
        }
    }
}