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
            // Create a list usable for Collection view in UI and fill it with failed requests
            Items = new ObservableCollection<LocationStateChange>();
            FailedRequstCollection.ItemFailedRequests.ForEach(x => Items.Add(x));

            // Remove specific item from lists
            RemoveCommand = new Command<LocationStateChange>((item) =>
            {
                Items.Remove(item);
                FailedRequstCollection.ItemFailedRequests.Remove(item);
            });

            // Remove all items from list
            RemoveAllCommand = new Command(async () =>
            {
                FailedRequstCollection.ItemFailedRequests.Clear();
                await App.Current.MainPage.Navigation.PopToRootAsync();
            });

            // Sends all items for statechange API call
            SendCommand = new Command(async () => 
            {
                List<LocationStateChange> invalidInput;

                // Check if device is connected to internet
                if (CrossConnectivity.Current.IsConnected)
                {
                    // Gather list of all unsuccessful item state changes
                    invalidInput = await App.ItemManager.StateChangeByLocation(FailedRequstCollection.ItemFailedRequests);
                    if (invalidInput.Count == 0)
                    {
                        // Message that all item state changes where successful
                        MessagingCenter.Send<FailedRequestViewModel, string>(this, "Success", $"Allar vörur eru skannaðar í hólf");
                        Items.Clear();
                        FailedRequstCollection.ItemFailedRequests.Clear();
                    }
                    else
                    {
                        // Message that something failed, 1 or more items could not have their state changed
                        MessagingCenter.Send<FailedRequestViewModel, string>(this, "Fail", $"Ekki var hægt að setja allar vörur í hólf.\n\nEftir er listi af vörum sem ekki var hægt að setja í hólfið");
                        Items.Clear();
                        FailedRequstCollection.ItemFailedRequests.Clear();

                        // Update list of items only to contain items that could not have their state changed
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

        /// <summary>
        ///     Send request to API
        /// </summary>
        public Command SendCommand { get; }

        /// <summary>
        ///     Revomes specific item from the list
        /// </summary>
        public Command RemoveCommand { get; }

        /// <summary>
        ///     Remove all items from the list
        /// </summary>
        public Command RemoveAllCommand { get; }

        /// <summary>
        ///     List of all items on the list of failed requests, usable in collection view
        /// </summary>
        public ObservableCollection<LocationStateChange> Items { get; set; }
    }
}
