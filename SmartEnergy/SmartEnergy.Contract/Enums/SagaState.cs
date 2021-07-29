using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Enums
{
    public enum SagaState
    {
        START_TRANSACTION,
        UPDATE_SAFETY_DOCUMENT,
        SAFETY_DOCUMENT_UPDATED,
        UPDATE_DEVICE_USAGE,
        DEVICE_USAGE_UPDATED,
        FINISHED_TRANSACTION,
        COMPENSATE_SF_UPDATE,
        COMPENSATE_DEV_USAGE_UPDATE,
        TRANSACTION_FAILED

    }
}
