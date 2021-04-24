using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface IBookService : IBaseService<Book>
    {
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    }
    public class BookService : BaseService<Book>, IBookService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return dbContext.Books.AnyAsync(entity => entity.Id == id, cancellationToken);
        }
    }
}