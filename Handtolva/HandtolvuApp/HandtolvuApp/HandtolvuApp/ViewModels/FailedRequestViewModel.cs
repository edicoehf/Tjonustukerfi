using HandtolvuApp.FailRequestHandler;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class FailedRequestViewModel
    {
        public FailedRequestViewModel()
        {
            Items = new ObservableCollection<LocationStateChange>();
            FailedRequstCollection.ItemFailedRequests.ForEach(x => Items.Add(x));

            SendCommand = new Command(async () => 
            {
                List<LocationStateChange> invalidInput;
                if (CrossConnectivity.Current.IsConnected)
                {
                    invalidInput = await App.ItemManager.StateChangeByLocation(FailedRequstCollection.ItemFailedRequests);
                    if (invalidInput.Count == 0)
                    {
                        // success
                        MessagingCenter.Send<FailedRequestViewModel, string>(this, "Success", $"Allar vörur eru skannaðar í hólf");
                        Items.Clear();
                        FailedRequstCollection.ItemFailedRequests.Clear();
                    }
                    else
                    {
                        // display alert that something failed
                        MessagingCenter.Send<FailedRequestViewModel, string>(this, "Fail", $"Ekki var hægt að setja allar vörur í hólf.\n\nEftir er listi af vörum sem ekki var hægt að setja í hólfið");
                        Items.Clear();
                        FailedRequstCollection.ItemFailedRequests.Clear();

                        foreach (LocationStateChange i in invalidInput)
                        {
                            Items.Add(i);
                            FailedRequstCollection.ItemFailedRequests.Add(i);
                        }
                    }
                }
                else
                {
                    MessagingCenter.Send<FailedRequestViewModel, string>(this, "Fail", "Handskanni ótengdur\n\nHann verður að vera tengdur til að hægt sé að senda inn breytingar");
                }
            });
        }

        public Command SendCommand { get; }
        public ObservableCollection<LocationStateChange> Items { get; set; }
    }
}
