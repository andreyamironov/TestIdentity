using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AMir.Wrapper
{
    public static class IQueryableWrap
    {
        static int TAKE_DEFAULT = 100;
        public const string ASC = "asc";
        public const string DESC = "desc";

        public static IQueryable<T> SimpleFunc<T>(this IQueryable<T> source, Expression<Func<T,bool>> expression) => Queryable.Where(source, expression);
        public static IEnumerable<T> SkipTakeFuncPredicate<T>(this IQueryable<T> source, Func<T, bool> predicate, int skip, int take, out int total)
        {
            if (skip < 0) skip = 0;
            if (take <= 0) take = TAKE_DEFAULT;
            total = source.Count();
            return Enumerable.Where(source.AsEnumerable(), predicate).Skip(skip).Take(take);
        }


        public static IEnumerable<T> SkipTakeFuncPredicate<T>(this IQueryable<T> source, Func<T, object> orderByKeySelector, Func<T, bool> whereKeySelector, int skip, int take, out int total, string ascDesc = ASC)
        {
            if (skip < 0) skip = 0;
            if (take <= 0) take = TAKE_DEFAULT;
            total = source.Count();

            IEnumerable<T> ieSource;

            if(orderByKeySelector!=null)
            {
                if(ascDesc==DESC)
                {
                    ieSource = source.OrderByDescending(orderByKeySelector);
                }
                else
                {
                    ieSource = source.OrderBy(orderByKeySelector);
                }
            }
            else
            {
                ieSource = source;
            }

            if (whereKeySelector != null)
            {
                return Enumerable.Where(ieSource, whereKeySelector).Skip(skip).Take(take);
            }
            else
            {
                return ieSource.Skip(skip).Take(take);
            }
        }
    }
}
