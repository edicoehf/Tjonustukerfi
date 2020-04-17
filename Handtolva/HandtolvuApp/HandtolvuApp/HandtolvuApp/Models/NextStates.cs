using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class NextStates
    {
        public State CurrentState { get; set; }
        public List<State> NextAvailableStates { get; set; }

        //*     Overrides     *//

        public override string ToString()
        {
            string print = $"Current State => {CurrentState}";

            foreach(var state in NextAvailableStates)
            {
                print += $"\n{state}";
            }

            return print;
        }
    }
}
