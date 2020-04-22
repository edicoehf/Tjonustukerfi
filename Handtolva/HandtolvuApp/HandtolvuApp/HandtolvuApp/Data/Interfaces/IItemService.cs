using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Interfaces
{
    public interface IItemService
    {
        Task<NextStates> GetNextStatesAsync(string barcode);

        Task<Item> GetItemAsync(string barcode);

        Task StateChangeWithId(long itemId, string barcode);
    }
}
