using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using Dapr.Client;
using System.Threading.Tasks;
using SmartEnergy.Contract.CustomExceptions.Location;
using System.Net.Http;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class ConsumerService : IConsumerService
    {

        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DaprClient _daprClient;

        public ConsumerService(MicroserviceDbContext dbContext, IMapper mapper, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _daprClient = daprClient;
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


        public async Task<List<ConsumerDto>> GetAllConsumers()
        {
            List<ConsumerDto> consumers = _mapper.Map<List<ConsumerDto>>(_dbContext.Consumers.ToList());

            foreach(ConsumerDto consumer in consumers)
            {
                try
                {
                    LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{consumer.LocationID}");
                    consumer.Location = location;

                }
                catch (Exception e)
                {
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }

            }

            return consumers;

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
