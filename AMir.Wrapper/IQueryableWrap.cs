using System;
using System.Linq;
using System.Linq.Expressions;

namespace AMir.Wrapper
{
    public static class IQueryableWrap
    {
        public static IQueryable<T> SimpleFunc<T>(this IQueryable<T> source, Expression<Func<T,bool>> expression) =>
            Queryable.Where(source, expression);
    }
}
