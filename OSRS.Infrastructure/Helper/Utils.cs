using System;
using System.Collections.Generic;
using System.Linq;
using OSRS.Infrastructure.Helper.Enumeration;

namespace OSRS.Infrastructure.Helper
{
    public class Utils
    {
        public static IEnumerable<T> GetEnumValues<T>(OrderBy order = OrderBy.Numerically, OrderType type = OrderType.Ascending)
        {
            if (order != OrderBy.Alphabetically)
                return type == OrderType.Descending
                    ? Enum.GetValues(typeof(T)).Cast<T>().OrderByDescending(x => x)
                    : Enum.GetValues(typeof(T)).Cast<T>();
            if (type == OrderType.Descending)
                return from e in Enum.GetValues(typeof(T)).Cast<T>()
                    let nm = e.ToString()
                    orderby nm descending
                    select e;

            return from e in Enum.GetValues(typeof(T)).Cast<T>()
                let nm = e.ToString()
                orderby nm
                select e;
        }
    }
}