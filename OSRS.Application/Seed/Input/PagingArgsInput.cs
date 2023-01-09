using System.ComponentModel;
using OSRS.Domain.Seed.Paging;

namespace OSRS.Application.Seed.Input
{
    /// <summary>
    /// Paging Parameters
    /// </summary>
    public class PagingArgsInput
    {
        /// <summary>
        /// The current page requested
        /// </summary>
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        /// <summary>
        /// The size of the page requested
        /// </summary>
        [DefaultValue(5)]
        public int PageSize { get; set; }
        /// <summary>
        /// Paging Strategy
        /// </summary>
        [DefaultValue(PagingStrategy.WithCount)]
        public PagingStrategy PagingStrategy { get; set; }
    }
}
