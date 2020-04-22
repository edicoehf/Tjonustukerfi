using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class ScannedBarcodes
    {
        public string Barcode { get; set; }
        public string Symbology { get; set; }

        public ScannedBarcodes(string barcode, string symbology)
        {
            this.Barcode = barcode;
            this.Symbology = symbology;
        }
    }
}
