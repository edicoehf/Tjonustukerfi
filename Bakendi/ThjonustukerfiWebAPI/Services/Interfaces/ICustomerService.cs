using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO CreateCustomer(CustomerInputModel customer);
        CustomerDTO GetCustomerById(long id);
    }
}