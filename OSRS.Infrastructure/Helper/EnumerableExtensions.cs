using System;
using System.Collections.Generic;
using System.Linq;

namespace OSRS.Infrastructure.Helper
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        public static bool EquivalentListTo<T>(this List<T> list, List<T> other) where T : IEquatable<T> {
            if (list.Except(other).Any())
                return false;
            return !other.Except(list).Any();
        }
    }

}
