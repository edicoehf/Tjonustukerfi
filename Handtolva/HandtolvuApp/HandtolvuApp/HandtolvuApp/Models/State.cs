using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
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
