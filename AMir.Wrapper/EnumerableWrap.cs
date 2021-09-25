using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMir.Wrapper
{
    public static class EnumerableWrap
    {
        public static int IndexOf<T>(this IEnumerable<T> enumerable, T element, IEqualityComparer<T> comparer = null)
        {
            int i = 0;
            comparer = comparer ?? EqualityComparer<T>.Default;
            foreach (var currentElement in enumerable)
            {
                if (comparer.Equals(currentElement, element))
                {
                    return i;
                }

                i++;
            }

            return -1;
        }
        public static IEnumerable<T> SkipTakeFuncPredicate<T>(this IEnumerable<T> source, Func<T, bool> predicate, int skip, int take,out int total)
        {
            if (skip < 0) skip = 0;
            if (take <= 0) take = 100;
            total = source.Count();
            return Enumerable.Where(source, predicate).Skip(skip).Take(take);
        }
    }
}
