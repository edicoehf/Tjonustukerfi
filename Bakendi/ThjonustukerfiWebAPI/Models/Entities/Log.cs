using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Representation of the Log entity stored in the database, this is used to store unforseen errors thrown by the system.</summary>
    public class Log
    {
        public long Id { get; set; }
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }

        public DateTime DateOfError { get; set; }
    }
}