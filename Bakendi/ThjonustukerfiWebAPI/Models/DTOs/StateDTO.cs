namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>A data transfer object that represents a state in the system.</summary>
    public class StateDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }

        //*     Overrides     *//
        public static bool operator ==(StateDTO s1, StateDTO s2)
        {
            if(object.ReferenceEquals(s1, s2)) { return true; }

            if(object.ReferenceEquals(s1, null) || object.ReferenceEquals(s2, null)) { return false; }

            return s1.Id == s2.Id && s1.Name == s2.Name;
        }

        public static bool operator !=(StateDTO s1, StateDTO s2)
        {
            return !(s1 == s2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(StateDTO other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StateDTO);
        }
    }
}