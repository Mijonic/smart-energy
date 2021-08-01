using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.CustomExceptions.Saga
{
    public class SagaException : Exception
    {
        public SagaException(string message) : base(message)
        {

        }
    }
}
