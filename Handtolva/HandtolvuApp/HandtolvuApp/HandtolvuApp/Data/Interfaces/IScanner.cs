using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Data.Interfaces
{
    public interface IScanner
    {
        event EventHandler<StatusEventArgs> OnScanDataCollected;
        event EventHandler<string> OnStatusChanged;

        void Read();

        void Enable();

        void Disable();

        void SetConfig(IScannerConfig a_config);
    }
}
