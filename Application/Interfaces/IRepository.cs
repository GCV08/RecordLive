using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task Add(T entity);
        Task Update(T entity);
        Task<bool> Delete(int Id);
        Task<bool> HardDelete(int Id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool includeDeleted = false);
    }
}
