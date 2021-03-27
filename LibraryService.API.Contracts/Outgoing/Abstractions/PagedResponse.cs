using System;

namespace LibraryService.API.Contracts.Outgoing.Abstractions
{
    public class PagedResponse<TFoundItem>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PagedResponse{T}" /> class.
        /// </summary>
        public PagedResponse()
        {
            Items = Array.Empty<TFoundItem>();
        }

        /// <summary>
        ///     Gets or sets a single page of found items matching the search query.
        /// </summary>
        public TFoundItem[] Items { get; set; }

        /// <summary>
        ///     Gets or sets the total count of all items matching the search query.
        /// </summary>
        public long TotalCount { get; set; }
    }
}