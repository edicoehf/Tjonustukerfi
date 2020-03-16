using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        CustomerDTO CreateCustomer(CustomerInputModel customer);
        CustomerDTO GetCustomerById(long id);
        void UpdateCustomerDetails(CustomerInputModel customer, long id);
        void DeleteCustomerById(long id);
    }
}