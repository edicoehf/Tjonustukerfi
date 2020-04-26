using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;
namespace ThjonustukerfiWebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        private DataContext _dbContext;
        private string _connectionString;
        private bool _runningTests = false;

        /// <summary>This is used for tests when setting the DbContext to mapper</summary>
        public MappingProfile(DataContext context) : this()
        {
            _dbContext = context;
            _runningTests = true;
        }

        /// <summary>
        ///     Provides a profile to use with AutoMapper. Automapper is a setup as a singleton and therefor  
        ///     if you are using db context you must create it in every map that will use it.
        /// </summary>
        public MappingProfile(string connectionString = null)
        {
            //* Setup DB connection
            if(connectionString != null)
            {
                _connectionString = connectionString;
            }

            //* Customer Mappings
            // Automapper for CustomerInputModel to Customer entity
            CreateMap<CustomerInputModel, Customer>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now));
            
            // Automapper for Customer entity to Customer/Details DTO
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Customer, CustomerDetailsDTO>();

            //* Item Mappings
            // Automapper for ItemInputModel to Item entity
            CreateMap<ItemInputModel, Item>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.StateId, opt => opt.MapFrom(src => 1));

            CreateMap<Item, ItemDTO>();
            CreateMap<Item, ItemStateDTO>();
            // .ForMember(src => src.OrderId, opt => 
                //     opt.MapFrom((src, dst) => dst.OrderId = _dbContext.ItemOrderConnection.FirstOrDefault(ioc => ioc.ItemId == src.Id).OrderId))
                // .ForMember(src => src.State, opt =>
                //     opt.MapFrom((src, dst) => dst.State = _dbContext.State.FirstOrDefault(s => s.Id == src.StateId).Name));

            // Automapper for mapping item to item archive
            CreateMap<Item, ItemArchive>()
            .ForMember(src => src.Id, opt => opt.Ignore())
            .AfterMap((src, dst) =>
            {
                DatabaseBuilder();  // create a db instance

                dst.extraDataJSON = src.JSON;   // get the json data
                dst.Category = _dbContext.Category.FirstOrDefault(c => c.Id == src.CategoryId).Name;    // Get category name
                dst.Service = _dbContext.Service.FirstOrDefault(s => s.Id == src.ServiceId).Name;       // Get service name

                var timestampList = _dbContext.ItemTimestamp.Where(its => its.ItemId == src.Id).OrderBy(state => state.StateId).ToList();   // get them in the "correct" order
                // Add the timestamps to the json
                var propName = "timestamps";    // name of new json array
                JObject json = JObject.Parse(dst.extraDataJSON);    // parse the json object we want to change
                json.Add(new JProperty(propName, new JArray()));    // Create a new array as a json property
                JArray timeStamps = (JArray)json[propName];         // Get the array we created

                // add all timestamps of the item to the json array
                foreach (var stamp in timestampList)
                {
                    timeStamps.Add(JsonConvert.SerializeObject(new TimestampArchiveInput()  // special input that does not include the timestamp ID (no need)
                    {
                        StateId = stamp.StateId,
                        TimeOfChange = stamp.TimeOfChange
                    }));
                }

                dst.extraDataJSON = json.ToString();    // update the json file

                DestroyDatabase();  // remove the instance so it can be removed from the memory
            });

            // Automapper for item archive to DTO
            CreateMap<ItemArchive, ArchiveItemDTO>();

            //* Order Mappings
            // Automapper for OrderInputModel to Order entity
            CreateMap<OrderInputModel, Order>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.NotificationCount, opt => opt.MapFrom(src => 0));

            // Automapper for Order entity to Order DTO
            CreateMap<Order, OrderDTO>()
                .AfterMap((src, dst) =>
                {
                    DatabaseBuilder();  // build instance of db

                    dst.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == src.CustomerId).Name;

                    // Loop through all items in the order and add them to the DTO
                    var itemList = _dbContext.ItemOrderConnection.Where(c => c.OrderId == src.Id).ToList();
                    dst.Items = new List<ItemDTO>();
                    foreach (var item in itemList)
                    {
                        var itemEntity = _dbContext.Item.FirstOrDefault(i => i.Id == item.ItemId);                  // get item entity
                        var add = new ItemDTO()
                        {
                            Id = itemEntity.Id,
                            Category = _dbContext.Category.FirstOrDefault(c => c.Id == itemEntity.CategoryId).Name, // Find category name
                            Service = _dbContext.Service.FirstOrDefault(s => s.Id == itemEntity.ServiceId).Name,    // Find Service name
                            State = _dbContext.State.FirstOrDefault(s => s.Id == itemEntity.StateId).Name,          // Find state name
                            Barcode = itemEntity.Barcode,
                            JSON = itemEntity.JSON
                        };

                        dst.Items.Add(add);     // Add the itemDTO to the orderDTO
                    }

                    DestroyDatabase();  // remove the instance so it can be removed from the memory
                });
                
            // Automapper for mapping order to order archive
            CreateMap<Order, OrderArchive>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .AfterMap((src, dst) =>
                {
                    DatabaseBuilder();  // Create the db instance

                    dst.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == src.CustomerId).Name;        // get the customers name
                    dst.OrderSize = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == src.Id).Count(); // see the size of the order

                    DestroyDatabase();  // remove the instance so it can be removed from the memory
                });

            // Automapper for archived orders to DTO
            CreateMap<OrderArchive, ArchiveOrderDTO>()
                .ForMember(src => src.Items, opt => opt.Ignore());

            //* Service Mappings
            // Automapper for Service to ServiceDTO
            CreateMap<Service, ServiceDTO>();

            //* State Mappings
            // Automapper for State to StateDTO
            CreateMap<State, StateDTO>();

            //* Category Mappings
            // Automapper for Category to CategoryDTO
            CreateMap<Category, CategoryDTO>();

            //* ItemTimestamp Mappings
            CreateMap<Item, ItemTimestamp>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.TimeOfChange, opt => opt.MapFrom(src => DateTime.Now))
                .AfterMap((src, dst) => { dst.ItemId = src.Id; });

            //* ItemstateInput Mappings
            CreateMap<ItemStateChangeInputIdScanner, ItemStateChangeBarcodeScanner>();
        }

        private void DatabaseBuilder()
        {
            if(_connectionString != null)
            {
                var options = new DbContextOptionsBuilder<DataContext>().UseNpgsql(_connectionString).Options;
                _dbContext = new DataContext(options);
            }
        }

        private void DestroyDatabase()
        {
            if(!_runningTests)
            {
                _dbContext = null;
            }
        }
    }
}