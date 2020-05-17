using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Model to hold information about barcode that is scanned
    ///     
    ///     Has information about Symbol - Code128 for example
    /// </summary>
    class Barcode
    {
        public string Data { get; set; }
        public string Symbol { get; set; }
        public string Info { get; set; }
    }
}
