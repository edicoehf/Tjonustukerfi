namespace ThjonustukerfiWebAPI.Models.Exceptions
{
    /// <summary>Exception to handle email falures</summary>
    [System.Serializable]
    public class EmailException : System.Exception
    {
        public EmailException() : base("Could not send email.") { }
        public EmailException(string message) : base(message) { }
        public EmailException(string message, System.Exception inner) : base(message, inner) { }
        protected EmailException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}