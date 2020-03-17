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

            var entity = _dbContext.Order.Add(orderToAdd).Entity;
            _dbContext.SaveChanges();

            // Add items toDatabase
            foreach(ItemInputModel item in order.Items)
            {
                var itemId = _dbContext.Item.Add(_mapper.Map<Item>(item)).Entity;
                // Save changes so we get the correct id for item.id
                _dbContext.SaveChanges();

                var itemOrderConnection = new ItemOrderConnectionInputModel {
                    OrderId = entity.Id,
                    ItemId = itemId.Id
                };

                _dbContext.ItemOrderConnection.Add(_mapper.Map<ItemOrderConnection>(itemOrderConnection));
                _dbContext.SaveChanges();
            }

            return _mapper.Map<OrderDTO>(entity);
        }
    }
}