using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Leonni.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetList();
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate);

        T GetSingle(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        void Save();
    }
}
