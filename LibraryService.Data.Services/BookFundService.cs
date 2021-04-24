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
    public interface IBookFundService : IBaseService<BookFund>
    {
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    }
    public class BookFundService : BaseService<BookFund>, IBookFundService
    {
        private readonly LibraryServiceDbContext dbContext;

        public BookFundService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return dbContext.BookFunds.AnyAsync(entity => entity.Id == id, cancellationToken);
        }
    }
}