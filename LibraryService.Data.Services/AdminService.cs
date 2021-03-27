using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface IAdminService : IBaseService<Admin>
    {
        Task<bool> ExistAsync(AdminDTO admin);
        Task<IReadOnlyCollection<Admin>> FindAsync(AdminSearchCondition searchCondition);
        Task<long> CountAsync(AdminSearchCondition searchCondition);
    }
    public class AdminService : BaseService<Admin> , IAdminService
    {
        private readonly LibraryServiceDbContext dbContext;

        public AdminService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistAsync(AdminDTO admin)
        {
            if (!await dbContext.Admins.AnyAsync(entity => entity.Login == admin.Login && entity.Password == admin.Password))
            {
                return false;
            }

            return true;
        }

        public async Task<IReadOnlyCollection<Admin>> FindAsync(AdminSearchCondition searchCondition)
        {
            IQueryable<Admin> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(searchCondition => searchCondition.Login)
                : query.OrderByDescending(searchCondition => searchCondition.Login);

            return await query.Skip((searchCondition.Page - 1) * searchCondition.PageSize).Take(searchCondition.PageSize).ToListAsync();
        }

        public async Task<long> CountAsync(AdminSearchCondition searchCondition)
        {
            IQueryable<Admin> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        public async Task<IReadOnlyCollection<Admin>> FindAsync()
        {
            return await dbContext.Admins.AsNoTracking().OrderBy(c => c.Login)
                .ToListAsync();
        }

        private IQueryable<Admin> BuildFindQuery(AdminSearchCondition searchCondition)
        {
            IQueryable<Admin> query = dbContext.Admins;

            if (searchCondition.Login.Any())
            {
                foreach (var contactPerson in searchCondition.Login)
                {
                    var upperContactPerson = contactPerson.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Login != null && x.Login.ToUpper().Contains(upperContactPerson));
                }
            }

            return query;
        }
    }
}