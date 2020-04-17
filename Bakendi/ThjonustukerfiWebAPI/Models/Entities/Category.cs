namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Represents the type of produce, e.g. Cod</summary>
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string JSON { get; set; }

        //*         Overrides         *//
        public static bool operator ==(Category t1, Category t2)
        {
            if(object.ReferenceEquals(t1, t2)) { return true; }
            if(object.ReferenceEquals(t1, null) || object.ReferenceEquals(t2, null)) { return false; }

            return t1.Id == t2.Id && t1.Name == t2.Name && t1.JSON == t2.JSON;
        }

        public static bool operator !=(Category t1, Category t2)
        {
            return !(t1 == t2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(Category other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Category);
        }
    }
}