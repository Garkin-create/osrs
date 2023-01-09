using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using LinqKit.Core;

namespace OSRS.Domain.Seed.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, List<Tuple<SortingOption, Expression<Func<T, object>>>> orderByList)
        {
            if (orderByList == null)
                return query;

            orderByList = orderByList.OrderBy(ob => ob.Item1.Priority).ToList();

            IOrderedQueryable<T> orderedQuery = null;
            foreach (var orderBy in orderByList)
            {
                if (orderedQuery == null)
                    orderedQuery = orderBy.Item1.Direction == SortingOption.SortingDirection.ASC ? query.OrderBy(orderBy.Item2) : query.OrderByDescending(orderBy.Item2);
                else
                    orderedQuery = orderBy.Item1.Direction == SortingOption.SortingDirection.ASC ? orderedQuery.ThenBy(orderBy.Item2) : orderedQuery.ThenByDescending(orderBy.Item2);
            }

            return orderedQuery ?? query;
        }
        public static IQueryable<T> WhereAnd<T>(this IQueryable<T> query, List<Tuple<FilteringOption, Expression<Func<T, bool>>>> filterList)
        {
            if (filterList == null)
                return query;

            //foreach (var filter in filterList){query = query.Where(filter.Item2);}
            var predicate = PredicateBuilder.New<T>();
            foreach (var filter in filterList) { predicate = predicate.And(filter.Item2); }

            if (filterList.Count > 0) { query = query.Where(predicate); }
            return query;
        }
        public static IQueryable<T> WhereOr<T>(this IQueryable<T> query, List<Tuple<FilteringOption, Expression<Func<T, bool>>>> filterList)
        {
            if (filterList == null)
                return query;

            var predicate = PredicateBuilder.New<T>();
            foreach (var filter in filterList) { predicate = predicate.Or(filter.Item2); }
            if (filterList.Count > 0) { query = query.Where(predicate); }
            return query;
        }
        public static IQueryable<T> WhereOr<T>(this IQueryable<T> query, params Expression<Func<T, bool>>[] predicates)
        {
            var orPredicate = PredicateBuilder.New<T>();
            foreach (var predicate in predicates)
            {
                orPredicate = orPredicate.Or(predicate);
            }
            return query.AsExpandable().Where(orPredicate);
        }
    }
}
