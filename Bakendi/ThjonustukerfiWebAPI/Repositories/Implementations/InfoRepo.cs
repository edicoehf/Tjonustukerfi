using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiWebAPI.Repositories.Implementations
{
    public class InfoRepo : IInfoRepo
    {
        private DataContext _dbContext;
        private IMapper _mapper;
        public InfoRepo(DataContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        public IEnumerable<ServiceDTO> GetServices()
        {
            return _mapper.Map<IEnumerable<ServiceDTO>>(_dbContext.Service.ToList());   // Get list and map to DTO list
        }

        public IEnumerable<StateDTO> GetStates()
        {
            return _mapper.Map<IEnumerable<StateDTO>>(_dbContext.State.ToList());   // Get list and map to DTO list
        }

        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_dbContext.Category.ToList()); // Get list and map to DTO list
        }

        public StateDTO GetStatebyId(long id)
        {
            var state = _mapper.Map<StateDTO>(_dbContext.State.FirstOrDefault(s => s.Id == id));

            if(state == null) { throw new NotFoundException($"Invalid state ID {id}"); }

            return state;
        }

        public List<StateDTO> GetNextStates(long serviceId, long stateId)
        {
            // Get the current step
            var currentStep = _dbContext.ServiceState.FirstOrDefault(ss => ss.ServiceId == serviceId && ss.StateId == stateId).Step;

            // Get IDs of all next available states
            var nextStateIDs = _dbContext.ServiceState.Where(ss => ss.ServiceId == serviceId && ss.Step == currentStep + 1).Select(s => s.StateId).ToList();

            // Get all the next states available to the Item
            var retVal = new List<StateDTO>();
            foreach (var stateID in nextStateIDs)
            {
                retVal.Add(_mapper.Map<StateDTO>(_dbContext.State.FirstOrDefault(s => s.Id == stateID)));
            }

            return retVal;
        }

        public List<ArchiveOrderDTO> GetArchivedOrders()
        {
            var orders = _mapper.Map<List<ArchiveOrderDTO>>(_dbContext.OrderArchive.ToList());

            foreach (var order in orders)
            {
                order.Items = _mapper.Map<List<ArchiveItemDTO>>(_dbContext.ItemArchive.Where(ia => ia.OrderArchiveId == order.Id).ToList());
            }

            return orders;
        }

        public List<ItemTimeStampDTO> GetItemHistory(long itemId)
        {
            var item = _dbContext.Item.FirstOrDefault(i => i.Id == itemId); // get item
            if(item == null) { throw new NotFoundException($"Item with ID {itemId} was not found."); }

            // get timestamps and order by state
            var timestamps = _dbContext.ItemTimestamp.Where(its => its.ItemId == itemId).ToList().OrderBy(s => s.StateId);

            return _mapper.Map<List<ItemTimeStampDTO>>(timestamps); // return mapped timestamps
        }
    }
}