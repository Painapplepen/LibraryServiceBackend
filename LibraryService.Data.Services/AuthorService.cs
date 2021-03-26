using System.Threading.Tasks;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;

namespace LibraryService.Data.Services
{
    public interface IAuthorService : IBaseService<Author>
    {
        Task<long> FindAsync(Author author);
    }
    public class AuthorService : BaseService<LibraryServiceDbContext, Author>, IAuthorService
    {

        public AuthorService(LibraryServiceDbContext dbContext) : base(dbContext)
        { }

        public Task<long> FindAsync(Author author)
        {
            throw new System.NotImplementedException();
        }
    }
}