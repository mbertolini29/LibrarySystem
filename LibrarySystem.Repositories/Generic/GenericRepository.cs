using LibrarySystem.Repositories.Data;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace LibrarySystem.Repositories.Generic
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        private bool disposed = false;
        public void Dispose() //disponer/desechar.
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //metodo para eliminar todos los recursos...
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }

        public bool Exists(Expression<Func<T, bool>> filter = null)
        {
            return dbSet.Any(filter);
        }

        public async Task<T> AddAsync(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            //independiente/separado
            if (_context.Entry(entity).State == EntityState.Detached) 
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        
        public async Task<T> DeleteAsync(T entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return entity;
        }

        public void DeleteRange(List<T> entityList)
        {
            dbSet.RemoveRange(entityList);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;
            if(disabledTracking) //seguimiento desactivado.
            {
                query = query.AsNoTracking();
            }
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(include != null)
            {
                query = include(query);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public T GetByIdAsync(Expression<Func<T, bool>> filter = null,              Func<IQueryable<T>, IIncludableQueryable<T,
            object>> include = null, bool disabledTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (disabledTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include!=null)
            {
                query = include(query);
            }

            return query.FirstOrDefault();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            dbSet.Entry(entity).State = EntityState.Modified;
            return entity;
        }        
        public void AddRange(List<T> entity)
        {
            dbSet.AddRange(entity);
        }
    }
}
