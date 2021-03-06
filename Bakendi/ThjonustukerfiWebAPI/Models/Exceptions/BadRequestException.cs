namespace ThjonustukerfiWebAPI.Models.Exceptions
{
    /// <summary>Exception created to handle bad requests from client.</summary>
    [System.Serializable]
    public class BadRequestException : System.Exception
    {
        public BadRequestException() : base("Bad request") { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, System.Exception inner) : base(message, inner) { }
        protected BadRequestException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}