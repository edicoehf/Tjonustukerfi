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
        /// 
        /// </summary>
        public static void AddOrUpdate<T>(this List<LocationStateChange> self, IEnumerable<LocationStateChange> other)
        {
            var newList = new List<LocationStateChange>();

            foreach(var otherItem in other)
            {
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
