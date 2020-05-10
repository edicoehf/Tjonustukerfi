using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
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
