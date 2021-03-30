using System.ComponentModel;
using LibraryService.API.Contracts.Extensions;
using Newtonsoft.Json;

namespace LibraryService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase : PageSettings
    {
        /// <summary>
        ///     Gets the direction in which the result should be sorted, ascending or descending. Prefer this property over
        ///     <c>SortDirection</c>.
        ///     This property will not be serialized.
        /// </summary>
        [JsonIgnore]
        public ListSortDirection ListSortDirection => SortDirection.ToListSortDirection();

        /// <summary>
        ///     Gets the direction in which the result should be sorted (asc or desc). Prefer <c>ListSortDirection</c> for
        ///     type-safety.
        /// </summary>
        public string SortDirection { get; set; } = "asc";

        /// <summary>
        ///     Gets the property on which the result should be sorted.
        /// </summary>
        public string SortProperty { get; set; }
    }
}