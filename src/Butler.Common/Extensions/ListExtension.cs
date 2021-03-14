using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Butler.Common.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> list) => list == null || !list.Any();
    }
}
