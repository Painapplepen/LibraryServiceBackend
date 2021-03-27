using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;

namespace LibraryService.Data.Services
{
    public interface IBookService : IBaseService<Admin>
    {
        Task<long> FindAsync(Author author);
    }
    public class BookService : BaseService<Admin>, IBookService
    {

        public BookService(LibraryServiceDbContext dbContext) : base(dbContext)
        { }

        public Task<long> FindAsync(Author author)
        {
            throw new System.NotImplementedException();
        }
    }
}