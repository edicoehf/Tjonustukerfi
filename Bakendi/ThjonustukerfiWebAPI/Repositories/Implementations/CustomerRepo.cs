using System;
using System.Collections.Generic;
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
    public class CustomerRepo : ICustomerRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public CustomerRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            return _mapper.Map<IEnumerable<CustomerDTO>>(_dbContext.Customer.ToList());
        }
        public long CreateCustomer(CustomerInputModel customer)
        {
            var check = _dbContext.Customer.FirstOrDefault(p => p.Email == customer.Email);
            if(check != null) { throw new InvalidIdException("This person already exists."); }
            // Mapping from input to entity and adding to database
            var entity = _dbContext.Customer.Add(_mapper.Map<Customer>(customer)).Entity;
            _dbContext.SaveChanges();

            return entity.Id;
        }
        public CustomerDetailsDTO GetCustomerById(long id)
        {
            // Get customer customer entity form database
            var entity = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            // Check if found
            if(entity == null) { throw new NotFoundException($"Customer with id {id} was not found."); }
            // Mapping from entity to DTO
            return _mapper.Map<CustomerDetailsDTO>(entity);
        }

        public void UpdateCustomerDetails(CustomerInputModel customer, long id)
        {
            // Find customer entity in database
            var entity = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            if(entity == null) { throw new NotFoundException($"Customer with id {id} was not found."); }

            // Store original date created to fix autmappers overwriting datecreated
            entity.Name         = customer.Name;
            entity.SSN          = customer.SSN;
            entity.Email        = customer.Email;
            entity.Phone        = customer.Phone;
            entity.Address      = customer.Address;
            entity.PostalCode   = customer.PostalCode;
            entity.Comment      = customer.Comment;
            entity.DateModified = DateTime.Now;

            _dbContext.SaveChanges();
        }

        public void UpdateCustomerEmail(CustomerEmailInputModel customer, long id)
        {
            // Find customer entity in database
            var entity = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            if(entity == null) { throw new NotFoundException($"Customer with id {id} was not found."); }

            entity.Email        = customer.Email;
            entity.DateModified = DateTime.Now;

            _dbContext.SaveChanges();
        }

        public void DeleteCustomerById(long id)
        {
            // Get customer entity from database
            var customer = _dbContext.Customer.FirstOrDefault(r => r.Id == id);
            // Check if customer exists throw exception if not
            if(customer == null) { throw new NotFoundException($"Customer with id {id} was not found"); }
            // Remove customer from database
            _dbContext.Customer.Remove(customer);
            _dbContext.SaveChanges();
        }
        public bool CustomerExists(long id)
        {
             var entity = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            // Check if found
            if(entity == null) { return false; }
            return true;
        }
    }
}