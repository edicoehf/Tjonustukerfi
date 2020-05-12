using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Modal for State
    ///     
    ///     Name and Id
    /// </summary>
    public class State
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} - Name: {Name}";
        }
    }
}
