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

        public OrderDTO GetOrderbyId(long id) => _orderRepo.GetOrderbyId(id);

        public long CreateOrder(OrderInputModel order) 
        {
            if(!_customerRepo.CustomerExists(order.CustomerId)) { throw new NotFoundException($"Customer with id {order.CustomerId} was not found"); }
            return _orderRepo.CreateOrder(order);
        }

        public void UpdateOrder(OrderInputModel order, long id) => _orderRepo.UpdateOrder(order, id);
        public void DeleteByOrderId(long id) => _orderRepo.DeleteByOrderId(id);
    }
}