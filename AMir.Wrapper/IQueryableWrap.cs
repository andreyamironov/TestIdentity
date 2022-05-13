using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AMir.Wrapper
{
    public static class IQueryableWrap
    {
        public static IQueryable<T> SimpleFunc<T>(this IQueryable<T> source, Expression<Func<T,bool>> expression) =>
            Queryable.Where(source, expression);
        public static IEnumerable<T> SkipTakeFuncPredicate<T>(this IQueryable<T> source, Func<T, bool> predicate, int skip, int take, out int total)
        {
            if (skip < 0) skip = 0;
            if (take <= 0) take = 100;
            total = source.Count();
            return Enumerable.Where(source.AsEnumerable(), predicate).Skip(skip).Take(take);
        }
    }
}
