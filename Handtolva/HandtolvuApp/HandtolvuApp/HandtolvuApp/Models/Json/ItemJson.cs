using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandtolvuApp.Models.Json
{
    public class ItemJson
    {
        public string Location { get; set; }
        public bool Sliced { get; set; }
        public bool Filleted { get; set; }
        public string OtherCategory { get; set; }
        public string OtherService { get; set; }

    }
}
