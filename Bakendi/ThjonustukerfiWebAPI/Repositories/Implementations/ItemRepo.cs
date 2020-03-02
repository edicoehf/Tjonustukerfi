using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.InputModels;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiWebAPI.Repositories.Implementations
{
    public class ItemRepo : IItemRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public ItemRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public ItemDTO CreateItem(ItemInputModel item)
        {
            // Mapping from input to entity and adding to database
            var entity = _dbContext.Item.Add(_mapper.Map<Item>(item));
            _dbContext.SaveChanges();
            // Mapping from entity to DTO
            return _mapper.Map<ItemDTO>(entity);
        }
        
    }
}