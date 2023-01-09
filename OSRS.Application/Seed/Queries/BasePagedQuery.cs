using OSRS.Domain.Seed.Paging;
using OSRS.Application.Seed.Models.Input;

namespace OSRS.Application.Seed.Queries
{
    public abstract class BasePagedQuery
    {
        /// <summary>
        /// The current page requested
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// The size of the page requested
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Paging Strategy
        /// </summary>
        public PagingStrategy PagingStrategy { get; set; }
        protected BasePagedQuery() : this(1, 25, PagingStrategy.WithCount)
        {

        }
        protected BasePagedQuery(PagingArgsInput input) : this(input.PageIndex, input.PageSize, input.PagingStrategy)
        {
        }

        protected BasePagedQuery(int pageIndex, int pageSize, PagingStrategy pagingStrategy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PagingStrategy = pagingStrategy;
        }
    }
}
