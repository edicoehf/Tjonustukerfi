using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Used to send state change for item
    ///     
    ///     Has both item barcode and state/location barcode
    /// </summary>
    public class LocationStateChange
    {
        public string ItemBarcode { get; set; }
        public string StateChangeBarcode { get; set; }
    }
}
