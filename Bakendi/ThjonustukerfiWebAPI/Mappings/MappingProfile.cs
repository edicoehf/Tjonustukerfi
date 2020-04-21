using System;
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
        private IConfiguration Configuration { get; }
        /// <summary>Provides a profile to use with AutoMapper</summary>
        public MappingProfile(string connectionString = null)
        {
            //* Setup DB connection
            if(connectionString != null)
            {
                var options = new DbContextOptionsBuilder<DataContext>().UseNpgsql(connectionString).Options;
                _dbContext = new DataContext(options);
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
                        ItemId = stamp.ItemId,
                        StateId = stamp.StateId,
                        TimeOfChange = stamp.TimeOfChange
                    }));
                }

                dst.extraDataJSON = json.ToString();    // update the json file
            });

            //* Order Mappings
            // Automapper for OrderInputModel to Order entity
            CreateMap<OrderInputModel, Order>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now));

            // Automapper for Order entity to Order DTO
            CreateMap<Order, OrderDTO>();
                
            // Automapper for mapping order to order archive
            CreateMap<Order, OrderArchive>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .AfterMap((src, dst) =>
                {
                    dst.Customer = _dbContext.Customer.FirstOrDefault(c => c.Id == src.CustomerId).Name;        // get the customers name
                    dst.OrderSize = _dbContext.ItemOrderConnection.Where(ioc => ioc.OrderId == src.Id).Count(); // see the size of the order
                });

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
            CreateMap<ItemStateChangeInputModel, ItemStateChangeBarcodeInputModel>();
        }
    }
}