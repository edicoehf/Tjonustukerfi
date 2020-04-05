using Newtonsoft.Json;

namespace ThjonustukerfiWebAPI.Models.Exceptions
{
    /// <summary>Generic exception that is used when there is an unknown error, also stores the stracktrace of that error.</summary>
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}