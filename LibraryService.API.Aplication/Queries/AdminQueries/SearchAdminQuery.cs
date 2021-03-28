using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraryService.API.Application.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.IncomingOutgoing.Admin;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using LibraryService.API.Contracts.Outgoing.Admin;
using LibraryService.Data.Services;
using LibraryService.Domain.Core.Entities;
using MediatR;

namespace LibraryService.API.Application.Queries.AdminQueries
{
    public class SearchAuthorQuery : PagedSearchQuery<FoundAdminDTO, AdminSearchCondition>
    {
        public SearchAuthorQuery(AdminSearchCondition searchCondition) : base(searchCondition)
        { }
    }

    public class SearchAdminQueryHandler : IRequestHandler<SearchAuthorQuery, PagedResponse<FoundAdminDTO>>
    {
        private readonly IAdminService adminService;

        public SearchAdminQueryHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<PagedResponse<FoundAdminDTO>> Handle(SearchAuthorQuery request, CancellationToken cancellationToken)
        {
            AdminSearchCondition searchCondition = new AdminSearchCondition()
            {
                Login = GetFilterValues(request.SearchCondition.Login),
                Page = request.SearchCondition.Page,
                PageSize = request.SearchCondition.PageSize,
                SortDirection = request.SearchCondition.SortDirection,
                SortProperty = request.SearchCondition.SortProperty
            };

            var sortProperty = GetSortProperty(searchCondition.SortProperty);
            IReadOnlyCollection<Admin> foundAdmin = await adminService.FindAsync(searchCondition, sortProperty);
            FoundAdminDTO[] mappedAdmins = foundAdmin.Select(MapToFoundAdmin).ToArray();
            var totalCount = await adminService.CountAsync(searchCondition);

            return new PagedResponse<FoundAdminDTO>
            {
                Items = mappedAdmins,
                TotalCount = totalCount
            };
        }

        public FoundAdminDTO MapToFoundAdmin(Admin admin)
        {
            return new FoundAdminDTO
            {
                Id = admin.Id,
                Login = admin.Login,
                Password = admin.Password
            };
        }

        private string[] GetFilterValues(ICollection<string> values)
        {
            return values == null
                       ? Array.Empty<string>()
                       : values.Select(v => v.Trim()).Distinct().ToArray();
        }

        protected string GetSortProperty(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return nameof(Admin.Id);
            }

            if (propertyName.Equals("adminLogin", StringComparison.InvariantCultureIgnoreCase))
            {
                return nameof(Admin.Login);
            }

            return propertyName;
        }
    }
}