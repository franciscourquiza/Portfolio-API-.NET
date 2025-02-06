using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private readonly DbContext _context;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<T?> Get<TId>(TId id)
        {
            var entity = await _context.Set<T>().FindAsync(new object?[] { id });
            return entity;
        }
        public async Task<List<T>> Get()
        {
            List<T> list = await _context.Set<T>().ToListAsync();
            return list;
        }
        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteByEmail<TEmail>(TEmail email) 
        {
            var entity = _context.Set<T>().Find(new object?[] { email });
            _context.Set<T>().Remove(entity);   
            await _context.SaveChangesAsync();  
        }

        public async Task<bool> Delete(T entity)
        {
            try
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
