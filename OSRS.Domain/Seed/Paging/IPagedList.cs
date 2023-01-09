namespace OSRS.Domain.Seed.Paging
{
    public interface IPagedList<out T>
    {
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Total count
        /// </summary>
        long TotalCount { get; }

        /// <summary>
        /// Total pages
        /// </summary>
        long TotalPages { get; }

        /// <summary>
        /// Has previous page
        /// </summary>
        bool HasPrevious { get; }

        /// <summary>
        /// Has next page
        /// </summary>
        bool HasNext{ get; }

        /// <summary>
        /// Items
        /// </summary>
        //IEnumerable<T> Items { get; }
    }
}
