using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Interfaces;
using ThjonustukerfiWebAPI.Models.Exceptions;
using System.Collections;
using ThjonustukerfiWebAPI.Configurations;
using System.Collections.Generic;

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
        public IEnumerable GetAllOrders() => _orderRepo.GetAllOrders();

        public void CompleteOrder(long orderId)
        {
            var order = _orderRepo.CompleteOrder(orderId);  // get order
            var customer = _customerRepo.GetCustomerById(order.CustomerId); // get customer
        }

        public OrderDTO SearchOrder(string barcode)
        {
            var orderID = _orderRepo.SearchOrder(barcode);  // Throws not found exception if no order has this barcode

            return _orderRepo.GetOrderbyId(orderID);
        }

        public void RemoveOrderQuery(string barcode) => _orderRepo.DeleteByOrderId(_orderRepo.SearchOrder(barcode));

        public List<ItemPrintDetailsDTO> GetOrderPrintDetails(long orderId) => _orderRepo.GetOrderPrintDetails(orderId);
    }
}