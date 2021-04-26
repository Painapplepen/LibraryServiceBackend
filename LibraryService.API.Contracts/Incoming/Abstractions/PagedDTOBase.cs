using System.ComponentModel;
using Newtonsoft.Json;

namespace LibraryService.API.Contracts.Incoming.Abstractions
{
    public class PagedDTOBase : PageSettings
    {
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