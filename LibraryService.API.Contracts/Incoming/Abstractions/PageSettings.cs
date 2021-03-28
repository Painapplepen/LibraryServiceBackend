namespace LibraryService.API.Contracts.Incoming.Abstractions
{
    public class PageSettings
    {
        /// <summary>
        ///     Gets the number of items to retrieve.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets the page that should be retrieved and thus how many items will be skipped.
        /// </summary>
        public int Page { get; set; }
    }
}