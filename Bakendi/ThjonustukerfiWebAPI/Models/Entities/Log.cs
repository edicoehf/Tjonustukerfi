using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class Log
    {
        public long Id { get; set; }
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public DateTime DateOfError { get; set; }
    }
}