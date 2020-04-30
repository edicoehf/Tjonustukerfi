using System.Collections;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class InfoService : IInfoService
    {
        private IInfoRepo _infoRepo;

        public InfoService(IInfoRepo infoRepo)
        {
            _infoRepo = infoRepo;
        }
        public IEnumerable GetServices() => _infoRepo.GetServices();
        public IEnumerable GetStates() => _infoRepo.GetStates();

        public IEnumerable GetCategories() => _infoRepo.GetCategories();
        public List<ArchiveOrderDTO> GetArchivedOrders() => _infoRepo.GetArchivedOrders();
        public List<ItemTimeStampDTO> GetItemHistory(long itemId) => _infoRepo.GetItemHistory(itemId);
    }
}