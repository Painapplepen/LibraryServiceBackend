using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;

namespace LibraryService.Data.Services
{
    public interface IAdminService : IBaseService<Admin>
    {
        Task<long> FindAsync(Admin admin);
    }
    public class AdminService : BaseService<LibraryServiceDbContext, Admin> , IAdminService 
    {

        public AdminService(LibraryServiceDbContext dbContext) : base(dbContext)
        { }

        public Task<long> FindAsync(Admin admin)
        {
            throw new System.NotImplementedException();
        }
    }
}