using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Representation of the Order entity stored in the database.</summary>
    public class Order
    {
        public long Id { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }
        // Auto generated
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int NotificationCount { get; set; }

        //*     Overrides     *//
        public static bool operator ==(Order o1, Order o2)
        {
            if(object.ReferenceEquals(o1, o2)) { return true; }
            if(object.ReferenceEquals(o1, null) || object.ReferenceEquals(o2, null)) {return false; }

            return  o1.Id == o2.Id && o1.CustomerId == o2.CustomerId && o1.Barcode == o2.Barcode &&
                    o1.JSON == o2.JSON && o1.DateCreated == o2.DateCreated &&
                    o1.DateModified == o2.DateModified && o1.DateCompleted == o2.DateCompleted &&
                    o1.NotificationCount == o2.NotificationCount;
        }

        public static bool operator !=(Order o1, Order o2)
        {
            return !(o1 == o2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(Order other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Order);
        }
    }
}