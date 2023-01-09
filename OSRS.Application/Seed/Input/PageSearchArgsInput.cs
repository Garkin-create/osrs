
using System.Collections.Generic;
using OSRS.Domain.Seed.Paging;

namespace OSRS.Application.Seed.Input
{
    /// <summary>
    /// Page Search Input Model
    /// </summary>
    public class PageSearchArgsInput : PagingArgsInput
    {
        /// <summary>
        /// Sorting Options
        /// </summary>
        public List<SortingOption> SortingOptions { get; set; }
        /// <summary>
        /// Filtering Options
        /// </summary>
        public List<FilteringOptionInput> FilteringOptions { get; set; }
    }
}
