using OSRS.Domain.Seed.Paging;

namespace OSRS.Application.Seed.Models.Output
{
    public class Location
    {
        public string ParentCode { get; set; } = string.Empty;
        public int[] Locations { get; set; }
        public PagingArgs Paging { get; set; }
    }
    
}