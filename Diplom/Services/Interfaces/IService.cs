using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Save();
    }
}
