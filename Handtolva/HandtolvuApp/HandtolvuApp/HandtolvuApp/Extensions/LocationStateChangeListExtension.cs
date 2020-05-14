using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandtolvuApp.Extensions
{
    public static class LocationStateChangeListExtension
    {
        /// <summary>
        ///     An extinsion method to evaluate if same ItemBarcode exists in both list and replace it if there is
        ///     
        ///     if not then just add to the list
        /// </summary>
        public static void AddOrUpdate<T>(this List<LocationStateChange> self, IEnumerable<LocationStateChange> other)
        {
            var newList = new List<LocationStateChange>();

            foreach(var otherItem in other)
            {
                // Check for same ItemBarcode, null if not found
                var itemToRemove = self.Where(item => item.ItemBarcode == otherItem.ItemBarcode).FirstOrDefault();
                if (itemToRemove != null)
                {
                    self.Remove(itemToRemove);
                }
                self.Add(otherItem);
            }
        }
    }
}
