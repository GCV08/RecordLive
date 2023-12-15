using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    internal class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RLContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(RLContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", bool includeDeleted = false)
        {
            IQueryable<T> query = _entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (includeDeleted)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await orderBy(query).Where(x => !x.IsDeleted).ToListAsync();
                }
            }
            else
            {
                if (includeDeleted)
                {
                    return await query.OrderByDescending(x => x.CreatedDate).ToListAsync();
                }
                else
                {
                    return await query.Where(x => !x.IsDeleted).OrderByDescending(x => x.CreatedDate).ToListAsync();
                }
            }
        }

        public async Task Add(T entity)
        {
            _entities.Add(entity);
        }

        public async Task<bool> HardDelete(int Id)
        {
            T entity = await GetById(Id);
            var result = _entities.Remove(entity);
            if (result.State == EntityState.Deleted) { return true; } else { return false; };
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
            return await _entities.FindAsync(Id);
        }

        public async Task Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;

            _entities.Update(entity);
        }

        public async Task<bool> Delete(int Id)
        {
            T entity = await GetById(Id);
            entity.IsDeleted = true;
            var result = _entities.Update(entity);
            if (result.State == EntityState.Modified) { return true; } else { return false; };
        }
    }
}
