using AutoMapper;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using SmartEnergy.Contract.CustomExceptions;
using SmartEnergy.Contract.CustomExceptions.Icon;
using SmartEnergy.MicroserviceAPI.Infrastructure;
using SmartEnergy.MicroserviceAPI.DomainModels;

namespace SmartEnergy.MicroserviceAPI.Services
{
    public class IconService : IIconService
    {

        private readonly MicroserviceDbContext _dbContext;
        private readonly IMapper _mapper;

        public IconService(MicroserviceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void AddIconToSettings(int iconId, int settingsId)
        {

            if (_dbContext.Settings.Find(settingsId) == null)
                throw new SettingsNotFoundException();


            Icon icon = _dbContext.Icons.Find(iconId);

            if (icon == null)
                throw new IconNotFoundException();


            icon.SettingsID = settingsId;

            _dbContext.SaveChanges();
        }

        public List<IconDto> GetAllIcons()
        {
            return _mapper.Map<List<IconDto>>(_dbContext.Icons.ToList());
        }

        public IconDto GetIconById(int iconId)
        {
            return _mapper.Map<IconDto>(_dbContext.Icons.Find(iconId));
        }

        public void RemoveIconFromSettings(int iconId, int settingsId)
        {
            if (_dbContext.Settings.Find(settingsId) == null)
                throw new SettingsNotFoundException();



            Icon icon = _dbContext.Icons.Find(iconId);

            if (icon == null)
                throw new IconNotFoundException();


            icon.SettingsID = null;

            _dbContext.SaveChanges();


        }
    }
}
