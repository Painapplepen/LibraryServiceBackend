using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;

namespace LibraryService.Data.Services
{
    public interface IBookFundService : IBaseService<Author>
    {
        Task<long> FindAsync(Author author);
    }
    public class BookFundService : BaseService<Author>, IBookFundService
    {

        public BookFundService(LibraryServiceDbContext dbContext) : base(dbContext)
        { }

        public Task<long> FindAsync(Author author)
        {
            throw new System.NotImplementedException();
        }
    }
}