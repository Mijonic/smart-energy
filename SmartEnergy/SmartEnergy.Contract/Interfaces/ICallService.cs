using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ICallService : IGenericService<CallDto>
    {
        Task<List<CallDto>> GetAllCalls();
        Task<CallDto> InsertCall(CallDto entity);
        Task<CallDto> UpdateCall(CallDto entity);
    }
}
