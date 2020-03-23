using System;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiWebAPI.Repositories.Implementations
{
    public class OrderRepo : IOrderRepo
    {
        public DataContext _dbContext;
        private IMapper _mapper;
        public OrderRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public OrderDTO CreateOrder(OrderInputModel order)
        {
            // TODO replace for actual barcode
            var orderToAdd = _mapper.Map<Order>(order);
            var barcodeEntry = _dbContext.Order.OrderByDescending(o => o.Barcode).FirstOrDefault();
            if(barcodeEntry == null)
            {
                orderToAdd.Barcode = "20200001";
            }
            else
            {
                var newBarcode = int.Parse(barcodeEntry.Barcode);
                newBarcode++;
                orderToAdd.Barcode = newBarcode.ToString();
            }

            // Get next Id for order and item, make sure the new order has correct id
            var newItemId = _dbContext.Item.Max(i => i.Id) + 1;
            var newOrderId = _dbContext.Order.Max(o => o.Id) + 1;
            orderToAdd.Id = newOrderId;

            var entity = _dbContext.Order.Add(orderToAdd).Entity;

            // Add items toDatabase
            foreach(ItemInputModel item in order.Items)
            {
                _dbContext.Item.Add(_mapper.Map<Item>(item));

                var itemOrderConnection = new ItemOrderConnection {
                    OrderId = newOrderId,
                    ItemId = newItemId
                };
                
                // Increment itemId for next Item
                newItemId++;
                _dbContext.ItemOrderConnection.Add(itemOrderConnection);
            }

            _dbContext.SaveChanges();

            return _mapper.Map<OrderDTO>(entity);
        }
    }
}