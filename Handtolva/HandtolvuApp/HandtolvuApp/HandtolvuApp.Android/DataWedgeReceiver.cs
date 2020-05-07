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
using HandtolvuApp.Models;

namespace HandtolvuApp.Droid
{
    [BroadcastReceiver]
    public class DataWedgeReceiver : BroadcastReceiver
    {
        // This intent string contains the source of the data as a string
        private static string SOURCE_TAG = "com.motorolasolutions.emdk.datawedge.source";
        // This intent string contains the barcode symbology as a string
        private static string LABEL_TYPE_TAG = "com.motorolasolutions.emdk.datawedge.label_type";
        // This intent string contains the captured data as a string
        // (in the case of MSR this data string contains a concatenation of the track data)
        private static string DATA_STRING_TAG = "com.motorolasolutions.emdk.datawedge.data_string";
        // Intent Action for our operation
        public static string IntentAction = "barcodescanner.RECVR";
        public static string IntentCategory = "android.intent.category.DEFAULT";

        public EventHandler<StatusEventArgs> scanDataReceived;

        public override void OnReceive(Context context, Intent intent)
        {
            if(intent.Action.Equals(IntentAction))
            {
                // Define strings that handle our output
                string Out = "";
                string sLabelType = "";
                // get the source of the data
                String source = intent.GetStringExtra(SOURCE_TAG);
                // save it to use later
                if (source == null)
                    source = "scanner";
                // get the data from the intent
                String data = intent.GetStringExtra(DATA_STRING_TAG);
                // let's define a variable for the data length
                int data_len = 0;
                // and set it to the length of the data
                if (data != null) { data_len = data.Length; }

                // check if the data has come from the barcode scanner
                if(source.Equals("scanner"))
                {
                    // Check if there is any data
                    if(data != null && data.Length > 0)
                    {
                        // get symbology
                        sLabelType = intent.GetStringExtra(LABEL_TYPE_TAG);
                        // check if the string is empty
                        if(sLabelType != null && sLabelType.Length > 0)
                        {
                            sLabelType = sLabelType.Substring(11);
                        }
                        else
                        {
                            sLabelType = "Unknown";
                        }

                        Out = data.ToString() + "\r\n";
                    }
                }

                if(scanDataReceived != null)
                {
                    scanDataReceived(this, new StatusEventArgs(Out, sLabelType));
                }
            }
        }
    }
}