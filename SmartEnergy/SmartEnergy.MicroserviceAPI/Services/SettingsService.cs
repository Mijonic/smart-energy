using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using SmartEnergy.MicroserviceAPI.DomainModels;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class SettingsService : ISettingsService
    {

        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;

        public SettingsService(MicroserviceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public SettingsDto GetDefaultSettings()
        {
            return _mapper.Map<SettingsDto>(_dbContext.Settings.Where( x => x.IsDefault == true).FirstOrDefault());
        }

        public SettingsDto GetLastSettings()
        {
            return _mapper.Map<SettingsDto>(_dbContext.Settings.OrderByDescending(x => x.ID).FirstOrDefault());
        }

        public void ResetToDefault()
        {
            foreach(Settings settings in _dbContext.Settings.Where(x => (bool)!x.IsDefault))
            {
                _dbContext.Settings.Remove(settings);
            }

            _dbContext.SaveChanges();
        }

        public SettingsDto UpdateSettings(SettingsDto modified)
        {
            
            if (modified.IsDefault)
            {

                modified.ID = 0;
                Settings newSett = _mapper.Map<Settings>(modified);
                newSett.IsDefault = false;
                _dbContext.Settings.Add(newSett);
                _dbContext.SaveChanges();

                return _mapper.Map<SettingsDto>(newSett);

            }
            else
            {

                Settings settings = _dbContext.Settings.Find(modified.ID);

                if (modified == null)
                    throw new SettingsNotFoundException();

                settings.UpdateSetting(_mapper.Map<Settings>(modified));
                _dbContext.SaveChanges();
                return _mapper.Map<SettingsDto>(settings);
            }


        }
    }
}
