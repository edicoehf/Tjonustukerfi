using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Extensions
{
    /// <summary>Provides extensions to the List class</summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     Removes all items in self that are the same as in the other list.
        ///     
        ///     Note that classes used in this method should have the all the Equals methods overrided to work.
        /// </summary>
        public static void RemoveExisting<T>(this IList<T> self, IEnumerable<T> other)
        {
            foreach(var item in other)
            {
                if(self.Contains(item)) { self.Remove(item); }
            }
        }
    }
}