using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T t);
        T Update(T t);
        void Delete(T entity);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T Get(int id);
        void Save();
    }
}
