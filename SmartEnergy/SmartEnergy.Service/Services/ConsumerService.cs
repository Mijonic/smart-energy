using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartEnergy.Service.Services
{
    public class ConsumerService : IConsumerService
    {

        private readonly SmartEnergyDbContext _dbContext;
        private readonly IMapper _mapper;

        public ConsumerService(SmartEnergyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ConsumerDto Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ConsumerDto> GetAll()
        {
            return _mapper.Map<List<ConsumerDto>>(_dbContext.Consumers.Include("Location").ToList());

        }

        public ConsumerDto Insert(ConsumerDto entity)
        {
            throw new NotImplementedException();
        }

        public ConsumerDto Update(ConsumerDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
