using System.Collections.Generic;
using System.Linq;

namespace Webdotnet.Custom.Core.Helpers
{
    public static  class EnumerableExtnsions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;
            return !enumerable.Any();
        }
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.IsNullOrEmpty();
        }
    }
}
