using HandtolvuApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class ZebraScannerConfig : IScannerConfig
    {
        public TriggerType TriggerType { get; set; }

        public bool IsEAN8 { get; set; }
        public bool IsEAN13 { get; set; }
        public bool IsCode39 { get; set; }
        public bool IsCode128 { get; set; }
        public bool IsContinuous { get; set; }

        public ZebraScannerConfig()
        {
            IsEAN8 = true;
            IsEAN13 = true;
            IsCode39 = true;
            IsCode128 = true;

            IsContinuous = true;
            TriggerType = TriggerType.HARD;
        }
    }

    public enum TriggerType
    {
        HARD,
        SOFT
    }
}
