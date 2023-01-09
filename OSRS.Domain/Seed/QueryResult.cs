using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using OSRS.Domain.Seed.Paging;

namespace OSRS.Domain.Seed
{
    [AutoMap(typeof(QueryResult<>))]
    public class QueryResult<T> : OperationResult<IEnumerable<T>>, IPagedList<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public long TotalCount { get; }
        public long TotalPages => TotalCount > 0 ? (int) Math.Ceiling(TotalCount / (double) PageSize) : 0;
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        public QueryResult(OperationResultType type, string message = "") : base(type, message)
        {

        }

        public QueryResult(OperationResultType type, PagingArgs pagingArgs, string message = "") : base(type, message)
        {
            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
        }

        public QueryResult(OperationResultType type, IEnumerable<T> data, string message = "") : base(type, message, data)
        {

        }

        public QueryResult(OperationResultType type, IEnumerable<T> data, long total, string message = "") : base(type, message, data)
        {
            PageIndex = 0;
            PageSize = 0;
            TotalCount = total;
        }
        public QueryResult(OperationResultType type, IEnumerable<T> data, int pageIndex, int pageSize, long total, string message = "") : base(type, message, data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = total;
        }
        public QueryResult(OperationResultType type, IEnumerable<T> data, PagingArgs pagingArgs, long total) : base(type, data)
        {
            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
            TotalCount = total;
        }

        public QueryResult(OperationResultType type, IQueryable<T> query, PagingArgs pagingArgs,
               List<Tuple<SortingOption, Expression<Func<T, object>>>> orderByList = null,
               List<Tuple<FilteringOption, Expression<Func<T, bool>>>> filterList = null, 
               FilteringOption.LogicalOperator logicalOperator= FilteringOption.LogicalOperator.Or, string message = "") : base(type, message, query) {

            query = query.OrderBy(orderByList);
            query = logicalOperator== FilteringOption.LogicalOperator.Or? query.WhereOr(filterList): query.WhereAnd(filterList);

            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
            PageSize = PageSize > 100 ? 100 : PageSize;

            TotalCount = 0;
            var items = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                        ? query.Skip((PageIndex - 1) * PageSize).Take(PageSize + 1).ToList() : (
                        (TotalCount = query.Count()) > 0 ? query.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList() : new List<T>());

            TotalCount = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                ? (PageIndex - 1) * PageSize + items.Count: TotalCount;

            if (pagingArgs.PagingStrategy == PagingStrategy.NoCount && items.Count == PageSize + 1) { items.RemoveAt(PageSize); }

            Data = items;
        }
        
        public QueryResult(OperationResultType type, IQueryable<T> query,
            List<Tuple<SortingOption, Expression<Func<T, object>>>> orderByList = null,
            List<Tuple<FilteringOption, Expression<Func<T, bool>>>> filterList = null, 
            FilteringOption.LogicalOperator logicalOperator= FilteringOption.LogicalOperator.Or, string message = "") : base(type, message, query) {

            query = query.OrderBy(orderByList);
            query = logicalOperator== FilteringOption.LogicalOperator.Or? query.WhereOr(filterList): query.WhereAnd(filterList);
            Data = query.ToList();
        }
        
        public QueryResult(OperationResultType type, IEnumerable<T> data, PagingArgs pagingArgs) : base(type, data)
        {
            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
            PageSize = PageSize > 100 ? 100 : PageSize;

            TotalCount = 0;
            var items = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                ? data.Skip((PageIndex - 1) * PageSize).Take(PageSize + 1).ToList()
                : (TotalCount = data.Count()) > 0 ? data.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList() : new List<T>();

            TotalCount = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                ? (PageIndex - 1) * PageSize + items.Count: TotalCount;

            if (pagingArgs.PagingStrategy == PagingStrategy.NoCount && items.Count == PageSize + 1) { items.RemoveAt(PageSize); }

            Data = items;
        }
    }

    public class NotificationPagedResult<T> : NotificationResult<IEnumerable<T>>, IPagedList<T>
    {
        public int PageIndex { get; }
        public int PageSize { get; }
        public long TotalCount { get; }
        public long TotalPages { get; }
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;

        public NotificationPagedResult(OperationResultType type, string message = "") : base(type, message)
        {

        }

        public NotificationPagedResult(OperationResultType type, PagingArgs pagingArgs, string message = "") : base(type, message)
        {
            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
        }

        public NotificationPagedResult(OperationResultType type, IEnumerable<T> data, string message = "") : base(type, message, data)
        {

        }

        public NotificationPagedResult(OperationResultType type, IEnumerable<T> data, long total, string message = "") : base(type, message, data)
        {
            PageIndex = 0;
            PageSize = 0;
            TotalPages = 0;
            TotalCount = total;
        }
        public NotificationPagedResult(OperationResultType type, IEnumerable<T> data, PagingArgs pagingArgs, long total) : base(type, data)
        {
            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
            TotalCount = total;

            TotalPages = TotalCount / PageSize;
            if (TotalCount % PageSize > 0) TotalPages++;
        }

        public NotificationPagedResult(OperationResultType type, IQueryable<T> query, PagingArgs pagingArgs,
               List<Tuple<SortingOption, Expression<Func<T, object>>>> orderByList = null,
               List<Tuple<FilteringOption, Expression<Func<T, bool>>>> filterList = null,
               FilteringOption.LogicalOperator logicalOperator = FilteringOption.LogicalOperator.Or, string message = "") : base(type, message, query)
        {

            query = query.OrderBy(orderByList);
            query = logicalOperator == FilteringOption.LogicalOperator.Or ? query.WhereOr(filterList) : query.WhereAnd(filterList);

            PageIndex = pagingArgs.PageIndex < 1 ? 1 : pagingArgs.PageIndex;
            PageSize = pagingArgs.PageSize < 1 ? 10 : pagingArgs.PageSize;
            PageSize = PageSize > 100 ? 100 : PageSize;

            TotalCount = 0;
            var items = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                        ? query.Skip((PageIndex - 1) * PageSize).Take(PageSize + 1).ToList() : (
                        (TotalCount = query.Count()) > 0 ? query.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList() : new List<T>());

            TotalCount = pagingArgs.PagingStrategy == PagingStrategy.NoCount
                ? (PageIndex - 1) * PageSize + items.Count : TotalCount;

            TotalPages = TotalCount / PageSize;
            if (TotalCount % PageSize > 0) TotalPages++;

            if (pagingArgs.PagingStrategy == PagingStrategy.NoCount && items.Count == PageSize + 1) { items.RemoveAt(PageSize); }

            this.Data = items;
        }
    }
}
