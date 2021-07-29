using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.SagaCompensationInfo
{
    public class CompensationInformation
    {
        public SagaState CurrentState { get; set; }

        public SafetyDocumentDto OldSafetyDocument { get; set; }

        public WorkPlanSafetyDocumentDto OldWorkPanSafetyDocument { get; set; }


        public void UpdateState(SagaState newState)
        {
            CurrentState = newState;
        }

        public SagaState GetCurrentState()
        {
            return CurrentState;
        }
    }
}
