using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Extensions
{
    public static class LinqExtensions
    {
        // Removes all duplicate items in other list
        public static void RemovExisting<T>(this IList<T> self, IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                if(self.Contains(item)) { self.Remove(item); }
            }
        }
    }
}