using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IInfoRepo
    {
         IEnumerable<ServiceDTO> GetServices();
    }
}