using System.Collections.Generic;

namespace OSRS.Domain.Seed.Paging
{
    public class PageSearchArgs : PagingArgs
    {
        /// <summary>
        /// Sorting options
        /// </summary>
        public List<SortingOption> SortingOptions { get; set; }

        /// <summary>
        /// Filtering options
        /// </summary>
        public List<FilteringOption> FilteringOptions { get; set; }
    }
}
