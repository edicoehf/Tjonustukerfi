using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
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
            List<OrderDTO> activeOrders = _orderRepo.GetActiveOrdersByCustomerId(id);   // doesn't need to throw any exception, will just return an empty list if none is found

            if(!activeOrders.Any()) 
            {
                _orderRepo.ArchiveCompleteOrdersByCustomerId(id);
                _customerRepo.DeleteCustomerById(id);
            }

            return activeOrders;
        }
        public IEnumerable GetAllCustomers() => _customerRepo.GetAllCustomers();

        public void DeleteCustomerByIdAndOrders(long customerId)
        {
            // Check if the customer exists
            if(!_customerRepo.CustomerExists(customerId)) { throw new NotFoundException($"Customer with ID {customerId} was not found."); }

            // Get active orders
            List<OrderDTO> activeOrders = _orderRepo.GetActiveOrdersByCustomerId(customerId);

            // If any active orders, delete them
            if(activeOrders.Any())
            {
                foreach (var order in activeOrders)
                {
                    _orderRepo.DeleteByOrderId(order.Id);
                }
            }

            // After deleting orders (if any) delete the customer
            _customerRepo.DeleteCustomerById(customerId);
        }
    }
}