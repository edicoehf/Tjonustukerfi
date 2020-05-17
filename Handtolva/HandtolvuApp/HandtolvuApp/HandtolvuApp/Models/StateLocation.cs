using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Used to store all possible states and locations
    /// </summary>
    public class StateLocation
    {
        public List<string> States { get; set; } = new List<string>();
        public List<string> Locations { get; set; } = new List<string>();
    }
}
