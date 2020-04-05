namespace ThjonustukerfiWebAPI.Models.Exceptions
{
    /// <summary>Exception created to handle invalid ID lookup.</summary>
    [System.Serializable]
    public class InvalidIdException : System.Exception
    {
        public InvalidIdException() : base("The Id is not valid") { }
        public InvalidIdException(string message) : base(message) { }
        public InvalidIdException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidIdException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}