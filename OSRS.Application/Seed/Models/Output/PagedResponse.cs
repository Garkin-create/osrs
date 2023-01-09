using System.Collections.Generic;
using OSRS.Domain.Seed.Paging;
using OSRS.Application.Seed.Interface;

namespace OSRS.Application.Seed.Models.Output
{
    public class PagedResponse<T> : Response<IEnumerable<T>>, IPagedList<T>
    {
        public PagedResponse()
        {

        }
        public PagedResponse(bool success = false, IEnumerable<T> data = null) : base(success, data)
        {
        }

        public PagedResponse(bool success = false, string message = "", IEnumerable<T> data = null) : base(success, message, data)
        {
        }
        /// <summary>
        /// The current page requested
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// The size of the page requested
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total pages found
        /// </summary>
        public long TotalPages { get; set; }
        /// <summary>
        /// Has a previus page
        /// </summary>
        public bool HasPrevious { get; set; }
        /// <summary>
        /// Has a next page
        /// </summary>
        public bool HasNext { get; set; }
        /// <summary>
        /// Total items found
        /// </summary>
        public long TotalCount { get; set; }
    }
    
    public class ConfigurationPagedResponse<T> : PagedResponse<T>, IConfigurationResponse {
        public ConfigurationPagedResponse() { }
        public ConfigurationPagedResponse(bool success = false, IEnumerable<T> data = null) : base(success, data) { }
        public ConfigurationPagedResponse(bool success = false, string message = "", IEnumerable<T> data = null) : base(success, message, data) { }
        public ConfigurationModel Configuration { get; set; }
    }
}
