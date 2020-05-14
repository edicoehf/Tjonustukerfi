using System;
using System.Drawing;
using AutoMapper;
using BarcodeLib;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;
namespace ThjonustukerfiWebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        /// <summary>
        ///     Provides a profile to use with AutoMapper. Automapper is a setup as a singleton.
        /// </summary>
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

            // Automapper for mapping item to item archive
            CreateMap<Item, ItemArchive>()
            .ForMember(src => src.Id, opt => opt.Ignore()); // don't map ID, let the database give the ID

            // Automapper for item archive to DTO
            CreateMap<ItemArchive, ArchiveItemDTO>();

            // Automapper for item print
            CreateMap<ItemDTO, ItemPrintDetailsDTO>()
            .AfterMap((src, dst) =>
            {
                var bCode = new Barcode();  // get barcode lib class

                // encode to image
                bCode.Encode(BarcodeLib.TYPE.CODE128, src.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);
                
                // set as base64
                dst.BarcodeImage = Convert.ToBase64String(bCode.Encoded_Image_Bytes);
            });

            // ItemTimestamp Mappings
            CreateMap<Item, ItemTimestamp>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ForMember(src => src.TimeOfChange, opt => opt.MapFrom(src => DateTime.Now))
                .AfterMap((src, dst) => { dst.ItemId = src.Id; });

            // ItemTimestamp to dto
            CreateMap<ItemTimestamp, ItemTimeStampDTO>();

            // ItemstateInput Mappings
            CreateMap<ItemStateChangeInputIdScanner, ItemStateChangeBarcodeScanner>();

            //* Order Mappings
            // Automapper for OrderInputModel to Order entity
            CreateMap<OrderInputModel, Order>()
                .ForMember(src => src.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.DateModified, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(src => src.NotificationCount, opt => opt.MapFrom(src => 0));

            // Automapper for Order entity to Order DTO
            CreateMap<Order, OrderDTO>();
                
            // Automapper for mapping order to order archive
            CreateMap<Order, OrderArchive>()
                .ForMember(src => src.Id, opt => opt.Ignore()); // ignore ID, let the dabase give the ID

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
        }
    }
}