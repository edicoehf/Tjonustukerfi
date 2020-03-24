using System;
using System.Collections.Generic;
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

        public OrderDTO GetOrderbyId(long id)
        {
            // Get order
            var entity = _dbContext.Order.FirstOrDefault(o => o.Id == id);
            // Check if order exists
            if(entity == null) { throw new NotFoundException($"Order with id {id} was not found."); }

            //! many calls to DB :(, suggestion: add order ID in Item, then only need to search id DB once to get all items
            var dto = _mapper.Map<OrderDTO>(entity);
            dto.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == entity.CustomerId).Name; // get customer name

            // Loop through a list of item order connections where all elements in list have this order ID
            var itemList = _dbContext.ItemOrderConnection.Where(c => c.OrderId == id).ToList();
            dto.Items = new List<ItemDTO>();
            foreach (var item in itemList)
            {
                var itemEntity = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);                  // get item entity
                var add = _mapper.Map<ItemDTO>(itemEntity);                                                 // map to DTO
                add.Service = _dbContext.Service.FirstOrDefault(s => s.Id == itemEntity.ServiceId).Name;    // Find service name
                dto.Items.Add(add);     // add item DTO to orderDTO item list
            }

            return dto;
        }

        public long CreateOrder(OrderInputModel order)
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
            long newItemId = 1;
            // throws error if list is empty
            if(_dbContext.Item.Any()) { newItemId = _dbContext.Item.Max(i => i.Id) + 1; }
            
            long newOrderId = 1;
            // throws error if list is empty
            if(_dbContext.Order.Any()) { newOrderId = _dbContext.Order.Max(o => o.Id) + 1; }
            orderToAdd.Id = newOrderId;

            var entity = _dbContext.Order.Add(orderToAdd).Entity;

            // Add items toDatabase
            foreach(ItemInputModel item in order.Items)
            {
                // Creates a custom ID to make sure everything is connected correctly
                var itemToAdd = _mapper.Map<Item>(item);
                itemToAdd.Id = newItemId;
                _dbContext.Item.Add(itemToAdd);

                var itemOrderConnection = new ItemOrderConnection {
                    OrderId = newOrderId,
                    ItemId = newItemId
                };
                
                // Increment itemId for next Item
                newItemId++;
                _dbContext.ItemOrderConnection.Add(itemOrderConnection);
            }

            _dbContext.SaveChanges();

            return entity.Id;
        }
    }
}