using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO CreateCustomer(CustomerInputModel customer);
        CustomerDetailsDTO GetCustomerById(long id);
        void UpdateCustomerDetails(CustomerInputModel customer, long id);
        void DeleteCustomerById(long id);
    }
}