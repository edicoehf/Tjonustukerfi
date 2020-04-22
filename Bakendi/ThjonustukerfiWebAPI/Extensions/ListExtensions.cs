using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        ///     Adds item to list, only if it hasn't been added before
        ///     
        ///     Note that classes used in this method should have the all the Equals methods overrided to work.
        /// </summary>
        public static void AddIfUnique<T>(this IList<T> self, T other)
        {
            if(!self.Any() || !self.Contains(other))
            {
                self.Add(other);
            }
        }

        /// <summary>
        ///     Gets all the elements in other that are not in itself.
        ///     
        ///     Note that classes used in this method should have the all the Equals methods overrided to work.
        /// </summary>
        /// <returns>
        ///     Returns a list of elements in other that are not in itself. Empty list if self contains all items in other.
        /// </returns>
        public static List<T> GetNotSame<T>(this IList<T> self, IEnumerable<T> other)
        {
            var newList = new List<T>();

            foreach (var otheritem in other)
            {
                if(!self.Contains(otheritem))
                {
                    newList.Add(otheritem);
                }
            }

            return newList;
        }

        /// <summary>
        ///     Checks if the lists contain exactly the same elements.
        ///     
        ///     Note that classes used in this method should have the all the Equals methods overrided to work.
        /// </summary>
        /// <returns>
        ///     Returns True if lists are the same. False if not.
        /// </returns>
        public static bool ContainsSameElements<T>(this IList<T> self, IEnumerable<T> other)
        {
            if(self.Count != other.Count()) { return false; }   // list are not same size, return false

            foreach (var otheritem in other)
            {
                if(!self.Contains(otheritem))   // If the item is not in self, return false
                {
                    return false;
                }
            }

            return true;
        }
    }
}