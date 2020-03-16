using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepo _customerRepo;
        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public CustomerDTO CreateCustomer(CustomerInputModel customer) => _customerRepo.CreateCustomer(customer);
        public CustomerDTO GetCustomerById(long id)
        {
            return _customerRepo.GetCustomerById(id);
        }
        public void DeleteCustomerById(long id) => _customerRepo.DeleteCustomerById(id);
        
    }
}