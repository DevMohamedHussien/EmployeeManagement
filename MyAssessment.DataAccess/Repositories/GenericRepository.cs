using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyAssessment.DataAccess.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyAssessmentDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(MyAssessmentDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<IEnumerable<T>> GetAllAsync( params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
    }
}
