using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ICrewService :IGenericService<CrewDto>
    {
        public CrewsListDto GetCrewsPaged(CrewField sortBy, SortingDirection direction, int page, int pageSize);
        
    }
}
