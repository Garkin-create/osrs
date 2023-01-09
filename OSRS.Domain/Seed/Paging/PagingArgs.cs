
namespace OSRS.Domain.Seed.Paging
{
    public class PagingArgs
    {
        public PagingArgs() { }
        public PagingArgs(int pageIndex, int pageSize, PagingStrategy pagingStrategy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PagingStrategy = pagingStrategy;
        }

        /// <summary>
        /// The current page requested
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// The size of the page requested
        /// </summary>
        public int PageSize { get; set; }

        public PagingStrategy PagingStrategy { get; set; }
    }
}
