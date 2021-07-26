using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.CustomExceptions.Call;
using Microsoft.EntityFrameworkCore;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.CustomExceptions.Location;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.Contract.CustomExceptions.Consumer;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using SmartEnergy.MicroserviceAPI.DomainModels;
using Dapr.Client;
using System.Threading.Tasks;
using System.Net.Http;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class CallService : ICallService
    {

        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly DaprClient _daprClient;

        public CallService(MicroserviceDbContext dbContext, IMapper mapper, DaprClient daprClient)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _daprClient = daprClient;
        }

        public void Delete(int id)
        {
            Call call = _dbContext.Calls.FirstOrDefault(x => x.ID == id);

            if (call == null)
                throw new CallNotFoundExcpetion("Call not found!");

            _dbContext.Calls.Remove(call);
            _dbContext.SaveChanges();
        }

        public CallDto Get(int id)
        {
            return _mapper.Map<CallDto>(_dbContext.Calls.FirstOrDefault(x => x.ID == id));
        }

        public List<CallDto> GetAll()
        {
            //return _mapper.Map<List<CallDto>>(_dbContext.Calls.Include(x => x.Location)
            //                                                  .ThenInclude(x => x.Consumers)
            //                                                  .ToList());

            return _mapper.Map<List<CallDto>>(_dbContext.Calls.ToList());
        }

        public async Task<List<CallDto>> GetAllCalls()
        {
            //return _mapper.Map<List<CallDto>>(_dbContext.Calls.Include(x => x.Location)
            //                                                  .ThenInclude(x => x.Consumers)
            //                                                  .ToList());

            List<CallDto> calls = _mapper.Map<List<CallDto>>(_dbContext.Calls.Include(x => x.Consumer).ToList());
            
            foreach(CallDto call in calls)
            {
                try
                {
                    LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{call.LocationID}");
                    call.Location = location;

                }
                catch (Exception e)
                {
                    throw new LocationNotFoundException("Location service is unavailable right now.");
                }

            }


            return calls;
        }

        public CallDto Insert(CallDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CallDto> InsertCall(CallDto entity)
        {
            if (entity.Hazard.Trim().Equals("") || entity.Hazard == null)
               throw new InvalidCallException("You have to enter call hazard!");

            if (!Enum.IsDefined(typeof(CallReason), entity.CallReason))
                throw new InvalidCallException("Undefined call reason!");


            //if (_dbContext.Location.Any(x => x.ID == entity.LocationID) == false)
            //    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exists!");

            try
            {
                LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{entity.LocationID}");
              
                if(location == null)
                    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exist!");


            }
            catch (Exception e)
            {
                throw new LocationNotFoundException("Location service is unavailable right now.");
            }


            if (entity.ConsumerID != 0 && entity.ConsumerID != null)
            {
                if (_dbContext.Consumers.Any(x => x.ID == entity.ConsumerID) == false)
                    throw new ConsumerNotFoundException($"Consumer with id = {entity.ConsumerID} does not exists!");
            }else
            {
                entity.ConsumerID = null;
            }


            Call newCall = _mapper.Map<Call>(entity);

            newCall.ID = 0;
            //newCall.Location = null;
            newCall.Consumer = null;
            newCall.Incident = null;
            //newCall.IncidentID = null;



            _dbContext.Calls.Add(newCall);
            _dbContext.SaveChanges();

            return _mapper.Map<CallDto>(newCall);



        }

        public CallDto Update(CallDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CallDto> UpdateCall(CallDto entity)
        {
            Call updatedCall = _mapper.Map<Call>(entity);
            Call oldCall = _dbContext.Calls.FirstOrDefault(x => x.ID.Equals(updatedCall.ID));


           // updatedCall.Location = null;
           
            if (oldCall == null)
                throw new CallNotFoundExcpetion($"Call with Id = {updatedCall.ID} does not exists!");

            if (updatedCall.Hazard.Trim().Equals("") || updatedCall.Hazard == null)
                throw new InvalidCallException("You have to enter hazard!");

            if (!Enum.IsDefined(typeof(CallReason), entity.CallReason))
                throw new InvalidCallException("Undefined call reason!");



            //if (_dbContext.Location.Where(x => x.ID.Equals(updatedCall.LocationID)) == null)
            //    throw new LocationNotFoundException($"Location with id = {updatedCall.LocationID} does not exists!");

            try
            {
                LocationDto location = await _daprClient.InvokeMethodAsync<LocationDto>(HttpMethod.Get, "smartenergylocation", $"/api/locations/{entity.LocationID}");

                if (location == null)
                    throw new LocationNotFoundException($"Location with id = {entity.LocationID} does not exist!");


            }
            catch (Exception e)
            {
                throw new LocationNotFoundException("Location service is unavailable right now.");
            }


            if (_dbContext.Incidents.Where(x => x.ID.Equals(updatedCall.IncidentID)) == null)
                throw new IncidentNotFoundException($"Incident with id = {updatedCall.IncidentID} does not exists!");


            oldCall.UpdateCall(updatedCall);
            _dbContext.SaveChanges();

            return _mapper.Map<CallDto>(oldCall);
        }
    }
}
