using CarTroubleSolver.Data.Data;
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
        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T>? GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(T entity)
        {
             _context.Remove(entity);
        }

        public async void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
