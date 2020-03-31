using System.Collections;
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
    }
}