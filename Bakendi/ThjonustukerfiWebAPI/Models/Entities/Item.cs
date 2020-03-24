using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class Item
    {
        public long Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("State")]
        public long StateId { get; set; }
        [ForeignKey("Service")]
        public long ServiceId { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }    // t.d. bitar, heil flök o.s.frv.
        // Auto generated
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }

        //*     Overrides     *//
        public static bool operator ==(Item i1, Item i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.Id == i2.Id && i1.Type == i2.Type && i1.StateId == i2.StateId &&
                        i1.ServiceId == i2.ServiceId && i1.Barcode == i2.Barcode && i1.JSON == i2.JSON &&
                        i1.DateCreated == i2.DateCreated && i1.DateModified == i2.DateModified && i1.DateCompleted == i2.DateCompleted;
        }

        public static bool operator !=(Item i1, Item i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(Item other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Item);
        }
    }
}