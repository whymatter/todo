using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoAPI.Extensions {
    public static class IEnumerableExtensions {
        public static int MaxDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int @default) {
            var enumerable = source as IList<TSource> ?? source.ToList();
            return enumerable.Any() ? enumerable.Max(selector) : @default;
        }
    }
}