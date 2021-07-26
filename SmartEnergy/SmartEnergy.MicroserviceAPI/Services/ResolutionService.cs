using AutoMapper;
using SmartEnergy.Contract.CustomExceptions.Resolution;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SmartEnergy.Contract.Enums;
using SmartEnergy.Contract.CustomExceptions.Incident;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using SmartEnergy.MicroserviceAPI.DomainModels;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class ResolutionService : IResolutionService
    {
        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ResolutionService(MicroserviceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Delete(int id)
        {
            Resolution resolution = _dbContext.Resolutions.FirstOrDefault(x => x.ID.Equals(id));

            if (resolution == null)
                throw new ResolutionNotFoundException($"Resolution with Id = {id} does not exists!");

            _dbContext.Resolutions.Remove(resolution);
            _dbContext.SaveChanges();
        }

        public ResolutionDto Get(int id)
        {
            return _mapper.Map<ResolutionDto>(_dbContext.Resolutions.FirstOrDefault(x => x.ID == id));
        }

        public List<ResolutionDto> GetAll()
        {
            return _mapper.Map<List<ResolutionDto>>(_dbContext.Resolutions.ToList());
        }

        public ResolutionDto GetResolutionIncident(int incidentId)
        {
            return _mapper.Map<ResolutionDto>(_dbContext.Resolutions.FirstOrDefault(x => x.IncidentID == incidentId));
        }

        public ResolutionDto Insert(ResolutionDto entity)
        {

            ValidateResolution(entity);

            Resolution newResolution = _mapper.Map<Resolution>(entity);
            newResolution.ID = 0;
           
       

            _dbContext.Resolutions.Add(newResolution);
            _dbContext.SaveChanges();

            return _mapper.Map<ResolutionDto>(newResolution);
        }

        public ResolutionDto Update(ResolutionDto entity)
        {

            ValidateResolution(entity);

            Resolution updatedResolution = _mapper.Map<Resolution>(entity);
            Resolution oldResolution = _dbContext.Resolutions.FirstOrDefault(x => x.ID.Equals(updatedResolution.ID));

 

            if (oldResolution == null)
                throw new ResolutionNotFoundException($"Resolution with Id = {updatedResolution.ID} does not exists!");

         



            oldResolution.UpdateResolution(updatedResolution);
            _dbContext.SaveChanges();

            return _mapper.Map<ResolutionDto>(oldResolution);
        }


        private void ValidateResolution(ResolutionDto entity)
        {
            if (!Enum.IsDefined(typeof(Cause), entity.Cause))
                throw new InvalidResolutionException("Undefined resolution cause!");

            if (!Enum.IsDefined(typeof(ConstructionType), entity.Construction))
                throw new InvalidResolutionException("Undefined resolution construction type!");

            if (!Enum.IsDefined(typeof(Material), entity.Material))
                throw new InvalidResolutionException("Undefined resolution material!");

            if (!Enum.IsDefined(typeof(Subcause), entity.Subcause))
                throw new InvalidResolutionException("Undefined resolution subcase!");


            if(entity.Cause.Equals("FAILURE"))
            {
                if (!entity.Subcause.Equals("BURNED_OUT") && !entity.Subcause.Equals("SHORT_CIRCUIT") && !(entity.Subcause.Equals("MECHANICAL_FAILURE")))
                    throw new InvalidResolutionException($"{entity.Subcause} is not subcase of Failure!");

            }
            else if(entity.Cause.Equals("WEATHER"))
            {
                if (!entity.Subcause.Equals("STORM") && !entity.Subcause.Equals("RAIN") && !entity.Subcause.Equals("WIND") && !entity.Subcause.Equals("SNOW"))
                    throw new InvalidResolutionException($"{entity.Subcause} is not subcase of Weather!");
        
            }else
            {

                if (!entity.Subcause.Equals("BAD_INSTALL") && !entity.Subcause.Equals("NO_SUPERVISION") && !entity.Subcause.Equals("UNDER_VOLTAGE"))
                    throw new InvalidResolutionException($"{entity.Subcause} is not subcase of Human error!");
               

            }


            if (_dbContext.Incidents.Any(x => x.ID == entity.IncidentID) == false)
                throw new IncidentNotFoundException($"Incident with id = {entity.IncidentID} does not exists!");

            //if (_dbContext.Resolutions.Any(x => x.IncidentID == entity.IncidentID) == true)
            //    throw new InvalidResolutionException($"Incident with id = {entity.IncidentID} already has associated resolution!");

        }

    }
}
