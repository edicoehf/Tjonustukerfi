using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IItemRepo
    {
        ItemDTO CreateItem(ItemInputModel item);
    }
}