using System;
using AutoMapper;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
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

            //* Order Mappings
            // Automapper for OrderInputModel to Order entity
            CreateMap<OrderInputModel, Order>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now));
                
            // Automapper for Order entity to Order DTO
            CreateMap<Order, OrderDTO>();

            //* Service Mappings
            // Automapper for Service to ServiceDTO
            CreateMap<Service, ServiceDTO>();

        }
    }
}