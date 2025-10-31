using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly Prn212Context _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(Prn212Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
           _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
           _dbSet.Remove(GetById(id)!);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
          return  _dbSet.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
        }
    }
}
