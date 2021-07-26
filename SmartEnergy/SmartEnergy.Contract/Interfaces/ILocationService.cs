using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ILocationService
    {
        List<LocationDto> GetAllLocations();

        LocationDto GetLocationById(int locationId);
    }
}
