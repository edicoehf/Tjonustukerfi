using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface ICustomerRepo
    {
        CustomerDTO CreateCustomer(CustomerInputModel customer);
        CustomerDetailsDTO GetCustomerById(long id);
        void UpdateCustomerDetails(CustomerInputModel customer, long id);
        void DeleteCustomerById(long id);
        bool CustomerExists(long id);
        IEnumerable<CustomerDTO> GetAllCustomers();
    }
}