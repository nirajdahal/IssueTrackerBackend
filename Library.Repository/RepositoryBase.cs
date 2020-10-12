using Library.Contracts;
using Library.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Library.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _context;
        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            var output = _context.Set<T>();
            return output;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
