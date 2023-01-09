using System.Collections.Generic;

namespace OSRS.Infrastructure.Dapper.Model
{
    public class PagedRows<T>
    {
        public int TotalRows { get; set; }
        public ICollection<T> Rows { get; set; } = new List<T>();
    }

    public class PagedRows<T, TS>
    {
        public int TotalRows { get; set; }
        public ICollection<T> Rows { get; set; } = new List<T>();
        public TS Summary { get; set; }
    }

}
