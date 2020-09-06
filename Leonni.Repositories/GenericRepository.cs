using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Leonni.Interfaces;
using System.Collections.Generic;

namespace Leonni.Repositories
{
    public abstract class GenericRepository<C, T> : IGenericRepository<T>
        where T : class
        where C : DbContext, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetList()
        {
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public virtual T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);

        }

        public virtual void Delete(T entity)
        {
            if (entity != null)
            {
                _entities.Set<T>().Remove(entity);

            }
        }

        public virtual void Update(T entity)
        {
            _entities.Entry(entity).State = System.Data.EntityState.Modified;

        }

        public virtual void Save()
        {
            try
            {
                _entities.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        errors.AppendFormat("Property: {0}   Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errors.ToString());
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
