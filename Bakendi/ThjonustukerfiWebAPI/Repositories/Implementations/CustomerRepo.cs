using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
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
            // Mapping from input to entity and adding to database
            var entity = _dbContext.Customer.Add(_mapper.Map<Customer>(customer));
            _dbContext.SaveChanges();

            // Mapping from entity to DTO
            return _mapper.Map<CustomerDTO>(entity);
        }
    }
}