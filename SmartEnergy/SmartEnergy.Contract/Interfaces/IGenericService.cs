using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(int id);
    }

  
}
