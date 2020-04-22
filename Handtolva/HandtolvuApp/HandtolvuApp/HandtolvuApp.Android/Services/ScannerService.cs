using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HandtolvuApp.Droid.Services;
using Symbol.XamarinEMDK;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Symbol.XamarinEMDK.Barcode;

[assembly: Dependency(typeof(ScannerService))]

namespace HandtolvuApp.Droid.Services
{
    public class ScannerService : Java.Lang.Object, EMDKManager.IEMDKListener, IScannerService
    {
        private EMDKManager _emdkManager;
        private BarcodeManager _barcodeManager;
        private Scanner _scanner;
        public ScannerService()
        {
            if(_emdkManager != null)
            {
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

        public event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;

        public void OnClosed()
        {
            throw new NotImplementedException();
        }

        public void OnOpened(EMDKManager emdkManager)
        {
            _emdkManager = emdkManager;
            InitScanner();
        }

        public void InitScanner()
        {
            try
            {
                if(_emdkManager == null) { return; }

                if(_barcodeManager == null)
                {
                    _barcodeManager = (BarcodeManager)_emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);
                    _scanner = _barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                    if(_scanner == null) { return; }

                    _scanner.Data += _scanner_Data;
                    _scanner.Status += _scanner_Status;

                    _scanner.Enable();

                    // Configuration
                    ScannerConfig scannerConfig = _scanner.GetConfig();
                    scannerConfig.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                    scannerConfig.ScanParams.DecodeLEDFeedback = true;
                    scannerConfig.DecoderParams.Code11.Enabled = true;
                    scannerConfig.DecoderParams.Code39.Enabled = true;
                    scannerConfig.DecoderParams.Code93.Enabled = true;
                    scannerConfig.DecoderParams.Code128.Enabled = true;
                    _scanner.SetConfig(scannerConfig);

                }

            }
            catch (Exception ex)
            {

            }
        }

        internal void Destroy()
        {
            if(_emdkManager != null)
            {
                _emdkManager.Release();
                _emdkManager = null;
            }
        }

        internal void DeinitScanner()
        {
            if(_emdkManager != null)
            {
                if(_scanner != null)
                {
                    try
                    {
                        _scanner.Data -= _scanner_Data;
                        _scanner.Status -= _scanner_Status;
                        _scanner.Disable();
                    }
                    catch (ScannerException e)
                    {

                    }
                }

                if(_barcodeManager != null)
                {
                    _emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }
                _barcodeManager = null;
                _scanner = null;
            }
        }

        private void _scanner_Data(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if(scanDataCollection != null && scanDataCollection.Result == ScannerResults.Success)
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();
                List<ScannedBarcodes> scannedBarcodes = new List<ScannedBarcodes>();
                foreach(var data in scanData)
                {
                    string barcode = data.Data;
                    string symbology = data.LabelType.Name();

                    scannedBarcodes.Add(new ScannedBarcodes(barcode, symbology));
                }

                this.OnBarcodeScanned?.Invoke(this, new OnBarcodeScannedEventArgs(scannedBarcodes));
            }



        }

        private void _scanner_Status(object sender, Scanner.StatusEventArgs e)
        {
            StatusData.ScannerStates state = e.P0.State;
            if(state == StatusData.ScannerStates.Idle)
            {
                try
                {
                    if (_scanner.IsEnabled && !_scanner.IsReadPending)
                    {
                        _scanner.Read();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}