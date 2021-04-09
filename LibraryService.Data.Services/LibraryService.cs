using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.Data.Domain.Models;
using LibraryService.Data.EF.SQL;
using LibraryService.Data.Services.Abstraction;
using LibraryService.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Data.Services
{
    public interface ILibraryService : IBaseService<Library>
    {
        Task<IReadOnlyCollection<Library>> FindAsync(LibrarySearchCondition searchCondition, string sortProperty);
        Task<long> CountAsync(LibrarySearchCondition searchCondition);
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    }

    public class LibraryService : BaseService<Library>, ILibraryService
    {
        private readonly LibraryServiceDbContext dbContext;

        public LibraryService(LibraryServiceDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
        {
            return dbContext.Libraries.AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<Library>> FindAsync(LibrarySearchCondition searchCondition, string sortProperty)
        {
            IQueryable<Library> query = BuildFindQuery(searchCondition);

            query = searchCondition.ListSortDirection == ListSortDirection.Ascending
                ? query.OrderBy(sortProperty)
                : query.OrderByDescending(sortProperty);

            return await query.Page(searchCondition.PageSize, searchCondition.Page).ToListAsync();
        }

        public async Task<long> CountAsync(LibrarySearchCondition searchCondition)
        {
            IQueryable<Library> query = BuildFindQuery(searchCondition);

            return await query.LongCountAsync();
        }

        private IQueryable<Library> BuildFindQuery(LibrarySearchCondition searchCondition)
        {
            IQueryable<Library> query = dbContext.Libraries;

            if (searchCondition.Name.Any())
            {
                foreach (var name in searchCondition.Name)
                {
                    var upperName = name.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Name != null && x.Name.ToUpper().Contains(upperName));
                }
            }

            if (searchCondition.Address.Any())
            {
                foreach (var address in searchCondition.Address)
                {
                    var upperAddress = address.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Address != null && x.Address.ToUpper().Contains(upperAddress));
                }
            }

            if (searchCondition.Telephone.Any())
            {
                foreach (var telephone in searchCondition.Telephone)
                {
                    var upperTelephone = telephone.ToUpper().Trim();
                    query = query.Where(x =>
                        x.Telephone != null && x.Telephone.ToUpper().Contains(upperTelephone));
                }
            }

            return query;
        }
    }
}
