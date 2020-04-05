namespace ThjonustukerfiWebAPI.Models.Exceptions
{
    /// <summary>Exception created to handle when objects are not found.</summary>
    [System.Serializable]
    public class NotFoundException : System.Exception
    {
        public NotFoundException() : base("Requested resource not found") { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected NotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}