using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.DTOs;
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
            return _mapper.Map<IEnumerable<ServiceDTO>>(_dbContext.Service.ToList());
        }
    }
}