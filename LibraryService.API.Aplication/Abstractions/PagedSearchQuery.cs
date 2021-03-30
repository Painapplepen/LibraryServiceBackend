using LibraryService.API.Contracts.Incoming.Abstractions;
using LibraryService.API.Contracts.Incoming.SearchConditions;
using LibraryService.API.Contracts.Outgoing.Abstractions;
using MediatR;

namespace LibraryService.API.Application.Abstractions
{
    public class PagedSearchQuery<TFound> : PagedSearchQuery<TFound, AdminSearchCondition>
    {
        /// <inheritdoc />
        public PagedSearchQuery(AdminSearchCondition searchCondition)
            : base(searchCondition)
        { }
    }

    public class PagedSearchQuery<TFound, TSearchCondition> : IRequest<PagedResponse<TFound>>
        where TSearchCondition : PagedDTOBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PagedSearchQuery{TFound, TSearchCondition}" /> class.
        /// </summary>
        public PagedSearchQuery(TSearchCondition searchCondition)
        {
            SearchCondition = searchCondition;
        }

        /// <summary>
        ///     Gets or sets the parameters that will be used to execute the search query.
        /// </summary>
        public TSearchCondition SearchCondition { get; }
    }
}