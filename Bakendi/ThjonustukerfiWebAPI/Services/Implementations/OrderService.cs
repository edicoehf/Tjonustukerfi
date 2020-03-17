using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;
using ThjonustukerfiWebAPI.Models.Exceptions;


namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public IOrderRepo _orderRepo;
        public ICustomerRepo _customerRepo;
        public OrderService(IOrderRepo orderRepo, ICustomerRepo customerRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
        }

        public OrderDTO CreateOrder(OrderInputModel order) 
        {
            if(!_customerRepo.CustomerExists(order.CustomerId)) { throw new NotFoundException($"Customer with id {order.CustomerId} was not found"); }
            return _orderRepo.CreateOrder(order);
        }
    }
}