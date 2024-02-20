using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        protected readonly CarTroubleSolverDbContext _context;
        protected GenericRepository(CarTroubleSolverDbContext context)
        {
            _context = context;
        }
        public Task<T> Add(T entity)
        {
             _context.Set<T>().Add(entity);
             _context.SaveChanges();
            return Task.FromResult(entity);
        }

        public Task<T> Get(Guid id)
        {
            return Task.FromResult(_context.Set<T>().Find(id));
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(Guid id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

    }
}
