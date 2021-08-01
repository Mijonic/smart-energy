using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface ISagaExecutionCoordinatorService
    {

        Task<SafetyDocumentDto> UpdateSafetyDocument(SafetyDocumentDto entity);
        Task<SafetyDocumentDto> CompensateUpdateSafetyDocument(SafetyDocumentDto oldSafetyDocument);
        Task<bool> UpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId);
        Task<bool> CompensateUpdateSafetyDocumentWorkPlan(int workPlanId, int safetyDocumentId);

    }
}
