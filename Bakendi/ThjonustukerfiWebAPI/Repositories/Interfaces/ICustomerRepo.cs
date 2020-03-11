using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        CustomerDTO CreateCustomer(CustomerInputModel customer);
        CustomerDTO GetCustomer(long id);
    }
}