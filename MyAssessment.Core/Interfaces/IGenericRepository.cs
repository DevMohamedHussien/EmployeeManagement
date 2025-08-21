using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetOneAsync(Expression<Func<T, bool>>? filter=null, string? props = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? props = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
