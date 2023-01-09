using System.Collections.Generic;
using OSRS.Domain.Seed.Paging;
using OSRS.Application.Seed.Input;

namespace OSRS.Application.Seed.Queries
{
    public abstract class FilteredBasePagedQuery : BasePagedQuery
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