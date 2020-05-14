using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;

namespace HandtolvuApp.Droid
{
    /// <summary>
    ///     Class around the scanner in a given device
    /// </summary>
    public class Scanner_Android : IScanner
    {
        private Context _context = null;
        private bool _bRegistered = false;
        private DataWedgeReceiver _broadcastReceiver = null;
        private static readonly string ACTION_DATAWEDGE_FROM_6_2 = "com.symbol.datawedge.api.ACTION";
        private static readonly string EXTRA_CREATE_PROFILE = "com.symbol.datawedge.api.CREATE_PROFILE";
        private static readonly string EXTRA_SET_CONFIG = "com.symbol.datawedge.api.SET_CONFIG";
        private static readonly string EXTRA_PROFILE_NAME = "Lokaverkefni Edico";                           // Name of the DataWedge profile that will be created on the device

        public Scanner_Android()
        {
            _context = Application.Context;
            _broadcastReceiver = new DataWedgeReceiver();

            // handles data from scanner event
            _broadcastReceiver.scanDataReceived += (s, scanData) =>
            {
                OnScanDataCollected?.Invoke(this, scanData);
            };

            // create a profile to use
            CreateProfile();
        }

        public event EventHandler<StatusEventArgs> OnScanDataCollected;
        public event EventHandler<string> OnStatusChanged;

        /// <summary>
        ///     Disable DataWedge profile and receiver
        /// </summary>
        public void Disable()
        {
            if(_broadcastReceiver != null && _context != null && _bRegistered)
            {
                _context.UnregisterReceiver(_broadcastReceiver);
                _bRegistered = false;
            }

            DisableProfile();
        }

        /// <summary>
        ///     Enable DataWedge profile and reset receiver
        /// </summary>
        public void Enable()
        {
            _context = Application.Context;

            if(_broadcastReceiver != null && _context != null)
            {
                IntentFilter filter = new IntentFilter(DataWedgeReceiver.IntentAction);
                filter.AddCategory(DataWedgeReceiver.IntentCategory);
                _context.RegisterReceiver(_broadcastReceiver, filter);
                _bRegistered = true;
            }

            EnableProfile();
        }

        /// <summary>
        ///     Can be used for soft trigger scan - scan by pressing button in UI
        /// </summary>
        public void Read()
        {
            // can be used for soft trigger scan
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Used to configure DataWedge profile and connect it to this app
        /// </summary>
        /// <param name="a_config">Scanner config interface </param>
        public void SetConfig(IScannerConfig a_config)
        {
            ZebraScannerConfig config = (ZebraScannerConfig)a_config;

            Bundle profileConfig = new Bundle();
            profileConfig.PutString("PROFILE_NAME", EXTRA_PROFILE_NAME);
            profileConfig.PutString("PROFILE_ENABLED", _bRegistered ? "true" : "false"); //  Seems these are all strings
            profileConfig.PutString("CONFIG_MODE", "UPDATE");
            Bundle barcodeConfig = new Bundle();
            barcodeConfig.PutString("PLUGIN_NAME", "BARCODE");
            barcodeConfig.PutString("RESET_CONFIG", "false"); //  This is the default but never hurts to specify
            Bundle barcodeProps = new Bundle();
            barcodeProps.PutString("scanner_input_enabled", "true");
            barcodeProps.PutString("scanner_selection", "auto"); //  Could also specify a number here, the id returned from ENUMERATE_SCANNERS.
                                                                 //  Do NOT use "Auto" here (with a capital 'A'), it must be lower case.
            barcodeProps.PutString("decoder_ean8", config.IsEAN8 ? "true" : "false");
            barcodeProps.PutString("decoder_ean13", config.IsEAN13 ? "true" : "false");
            barcodeProps.PutString("decoder_code39", config.IsCode39 ? "true" : "false");
            barcodeProps.PutString("decoder_code128", config.IsCode128 ? "true" : "false");

            barcodeConfig.PutBundle("PARAM_LIST", barcodeProps);
            profileConfig.PutBundle("PLUGIN_CONFIG", barcodeConfig);
            Bundle appConfig = new Bundle();
            appConfig.PutString("PACKAGE_NAME", Android.App.Application.Context.PackageName);      //  Associate the profile with this app
            appConfig.PutStringArray("ACTIVITY_LIST", new String[] { "*" });
            profileConfig.PutParcelableArray("APP_LIST", new Bundle[] { appConfig });
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
        }

        /// <summary>
        ///     Enable the DataWedge profile
        /// </summary>
        private void EnableProfile()
        {
            //  Now configure that created profile to apply to our application
            Bundle profileConfig = new Bundle();
            profileConfig.PutString("PROFILE_NAME", EXTRA_PROFILE_NAME);
            profileConfig.PutString("PROFILE_ENABLED", "true"); //  Seems these are all strings
            profileConfig.PutString("CONFIG_MODE", "UPDATE");
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
        }

        /// <summary>
        ///     Disable the DataWedge profile
        /// </summary>
        private void DisableProfile()
        {
            //  Now configure that created profile to apply to our application
            Bundle profileConfig = new Bundle();
            profileConfig.PutString("PROFILE_NAME", EXTRA_PROFILE_NAME);
            profileConfig.PutString("PROFILE_ENABLED", "false"); //  Seems these are all strings
            profileConfig.PutString("CONFIG_MODE", "UPDATE");
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
        }

        /// <summary>
        ///     Create DataWedge profile
        /// </summary>
        private void CreateProfile()
        {
            String profileName = EXTRA_PROFILE_NAME;
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_CREATE_PROFILE, profileName);

            //  Now configure that created profile to apply to our application
            Bundle profileConfig = new Bundle();
            profileConfig.PutString("PROFILE_NAME", EXTRA_PROFILE_NAME);
            profileConfig.PutString("PROFILE_ENABLED", "true"); //  Seems these are all strings
            profileConfig.PutString("CONFIG_MODE", "UPDATE");
            Bundle barcodeConfig = new Bundle();
            barcodeConfig.PutString("PLUGIN_NAME", "BARCODE");
            barcodeConfig.PutString("RESET_CONFIG", "true"); //  This is the default but never hurts to specify
            Bundle barcodeProps = new Bundle();
            barcodeConfig.PutBundle("PARAM_LIST", barcodeProps);
            profileConfig.PutBundle("PLUGIN_CONFIG", barcodeConfig);
            Bundle appConfig = new Bundle();
            appConfig.PutString("PACKAGE_NAME", Android.App.Application.Context.PackageName);      //  Associate the profile with this app
            appConfig.PutStringArray("ACTIVITY_LIST", new String[] { "*" });
            profileConfig.PutParcelableArray("APP_LIST", new Bundle[] { appConfig });
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);

            //  You can only configure one plugin at a time, we have done the barcode input, now do the intent output
            profileConfig.Remove("PLUGIN_CONFIG");
            Bundle intentConfig = new Bundle();
            intentConfig.PutString("PLUGIN_NAME", "INTENT");
            intentConfig.PutString("RESET_CONFIG", "true");
            Bundle intentProps = new Bundle();
            intentProps.PutString("intent_output_enabled", "true");
            intentProps.PutString("intent_action", DataWedgeReceiver.IntentAction);
            intentProps.PutString("intent_delivery", "2");
            intentConfig.PutBundle("PARAM_LIST", intentProps);
            profileConfig.PutBundle("PLUGIN_CONFIG", intentConfig);
            SendDataWedgeIntentWithExtra(ACTION_DATAWEDGE_FROM_6_2, EXTRA_SET_CONFIG, profileConfig);
        }

        /// <summary>
        ///     Broadcast DataWedge profile to the device to set/update it with bundle
        /// </summary>
        /// <param name="action">Action you want to set</param>
        /// <param name="extraKey">What kind of extra information you want to use, example config</param>
        /// <param name="extras">the extra information to use, like config specifications</param>
        private void SendDataWedgeIntentWithExtra(String action, String extraKey, Bundle extras)
        {
            Intent dwIntent = new Intent();
            dwIntent.SetAction(action);
            dwIntent.PutExtra(extraKey, extras);
            _context.SendBroadcast(dwIntent);
        }

        /// <summary>
        ///     Broadcast DataWedge profile to the device to set/update with string
        /// </summary>
        /// <param name="action">Action you want to set</param>
        /// <param name="extraKey">What kind of extra information you want to use, example config</param>
        /// <param name="extraValue">he extra information to use, like config specifications but only string</param>
        private void SendDataWedgeIntentWithExtra(String action, String extraKey, String extraValue)
        {
            Intent dwIntent = new Intent();
            dwIntent.SetAction(action);
            dwIntent.PutExtra(extraKey, extraValue);
            _context.SendBroadcast(dwIntent);
        }

    }
}