using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using FluentScheduler;
using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;

namespace ThjonustukerfiWebAPI.Schedules.Tasks
{
    /// <summary>Schedule task to send an e-mail notification when a customer doesn't pick up their order</summary>
    public class OrderCompleteReminder : IJob
    {
        private IOrderRepo _orderRepo;
        private ICustomerRepo _customerRepo;
        private Mapper _mapper;

        // This constructor is called each time the task is run, the context will then be created, used and then destroyed
        public OrderCompleteReminder()
        {
            // setup database
            DataContext dbContext;
            var options = new DbContextOptionsBuilder<DataContext>().UseNpgsql(Constants.DBConnection).Options;
            dbContext = new DataContext(options);

            // Create the mapping profile and the mapper
            var profile = new MappingProfile(Constants.DBConnection);
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            _mapper = new Mapper(config);

            // create the repo and inject the context and mapper
            _orderRepo = new OrderRepo(dbContext, _mapper);
            _customerRepo = new CustomerRepo(dbContext, _mapper);
        }

        // Execute the task
        public void Execute()
        {
            var orders = _orderRepo.GetOrdersReadyForPickup();

            // Get all orders that are ready and have been so for a week or more
            var oldReadyOrders = new List<Order>();
            var dateNow = DateTime.Now;
            foreach (var order in orders)
            {
                // Get total weeks since ready
                var totalWeeksready = Math.Floor((dateNow.Subtract(order.DateModified).TotalDays) / 7);
                if(totalWeeksready > order.NotificationCount)
                {
                    var customer = _customerRepo.GetCustomerById(order.CustomerId); // get the customer
                    if(Constants.sendEmail) { MailService.SendOrderNotification(_mapper.Map<OrderDTO>(order), customer, totalWeeksready); } // send the mail
                    if(Constants.sendSMS)   { /* send sms */ }
                    _orderRepo.IncrementNotification(order.Id); // Increment notification count so it only sends once a week
                }
            }
        }
    }
}