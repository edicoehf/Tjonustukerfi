using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Managers
{
    public class InfoManager
    {
        /// <summary>
        ///             Holds the list for all states and locations in the system
        /// </summary>
        public StateLocation StateLocation = new StateLocation();
        readonly IItemService itemService;

        public InfoManager(IItemService service)
        {
            itemService = service;
        }

        /// <summary>
        ///             Gets All States and locations for the system
        /// </summary>
        /// <returns></returns>
        public async Task GetStateAndLocations()
        {
            if(CrossConnectivity.Current.IsConnected)
            {
                StateLocation.States.Clear();
                StateLocation.Locations.Clear();
                List<State> states = await itemService.GetAllStates();
                foreach(var state in states)
                {
                    StateLocation.States.Add(state.Name);
                }
                StateLocation.Locations = await itemService.GetAllLocations();

            }
        }

        /// <summary>
        ///             Checks if the location barcode is valid
        /// </summary>
        /// <param name="ScannedBarcodeText">Barcode for the location</param>
        /// <returns>bool that returns true if it is valid</returns>
        public async Task<bool> CheckLocationBarcode(string ScannedBarcodeText)
        {

            await GetStateAndLocations();
            
            if (StateLocation.Locations.Count == 0 || StateLocation.States.Count == 0)
            {
                return false;
            }

            string[] locationCheck = ScannedBarcodeText.Split('-');

            if (!StateLocation.States.Contains(locationCheck[0]))
            {
                return false;
            }
            else
            {
                if (locationCheck.Length == 0 || locationCheck.Length > 2)
                {
                    return false;
                }
                else
                {
                    if (locationCheck.Length == 2)
                    {
                        if (!StateLocation.Locations.Contains(locationCheck[1]))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
