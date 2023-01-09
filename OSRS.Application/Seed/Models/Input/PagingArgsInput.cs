using System.ComponentModel;
using AutoMapper;
using OSRS.Domain.Seed.Paging;

namespace OSRS.Application.Seed.Models.Input
{
    /// <summary>
    /// Paging Parameters
    /// </summary>
    [AutoMap(typeof(PagingArgs), ReverseMap = true)]
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
        [DefaultValue(25)]
        public int PageSize { get; set; }
        /// <summary>
        /// Paging Strategy
        /// </summary>
        [DefaultValue(PagingStrategy.WithCount)]
        public PagingStrategy PagingStrategy { get; set; }
    }

}
