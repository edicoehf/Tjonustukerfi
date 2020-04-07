using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepo _customerRepo;
        private IOrderRepo _orderRepo;
        public CustomerService(ICustomerRepo customerRepo, IOrderRepo orderRepo)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
        }
        public CustomerDTO CreateCustomer(CustomerInputModel customer) => _customerRepo.CreateCustomer(customer);
        public CustomerDetailsDTO GetCustomerById(long id) => _customerRepo.GetCustomerById(id);
        public void UpdateCustomerDetails(CustomerInputModel customer, long id) => _customerRepo.UpdateCustomerDetails(customer, id);
        public List<OrderDTO> DeleteCustomerById(long id)
        {
            List<OrderDTO> activeOrders = _orderRepo.GetActiveOrdersByCustomerId(id);

            //TODO: Archive orders that are done, but not archived yet before deleting customer connection
            if(!activeOrders.Any()) { _customerRepo.DeleteCustomerById(id); }

            return activeOrders;
        }
        public IEnumerable GetAllCustomers() => _customerRepo.GetAllCustomers();
    }
}