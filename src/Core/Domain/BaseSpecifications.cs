﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OSRS.Domain
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        public IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query)
        {
            return query.Where(Criteria);
        }
    }
}