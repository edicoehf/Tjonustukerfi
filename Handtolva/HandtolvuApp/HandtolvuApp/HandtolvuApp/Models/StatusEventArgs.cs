using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Used to handle scanning from device
    ///     
    ///     Gives the barcode value and typing
    /// </summary>
    public class StatusEventArgs : EventArgs
    {
        private string barcodeData;

        public StatusEventArgs(string dataIn, string barcodeTypeIn)
        {
            barcodeData = dataIn;
            barcodeType = barcodeTypeIn;
        }

        public string Data { get { return barcodeData; } }

        private string barcodeType;
        public string BarcodeType { get { return barcodeType; } }
    }
}
