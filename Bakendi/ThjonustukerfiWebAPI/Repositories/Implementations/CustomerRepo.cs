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
    public class CustomerRepo : ICustomerRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public CustomerRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public CustomerDTO CreateCustomer(CustomerInputModel customer)
        {
            var check = _dbContext.Customer.FirstOrDefault(p => p.Email == customer.Email);
            if(check != null) { throw new InvalidIdException("This person already exists."); }
            // Mapping from input to entity and adding to database
            var entity = _dbContext.Customer.Add(_mapper.Map<Customer>(customer)).Entity;
            _dbContext.SaveChanges();

            // Mapping from entity to DTO
            return _mapper.Map<CustomerDTO>(entity);
        }
        public CustomerDTO GetCustomerById(long id)
        {
            // Get customer customer entity form database
            var entity = _dbContext.Customer.FirstOrDefault(c => c.Id == id);
            // Check if found
            if(entity == null) { throw new NotFoundException($"Customer with id {id} was not found."); }
            // Mapping from entity to DTO
            return _mapper.Map<CustomerDTO>(entity);
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
    }
}