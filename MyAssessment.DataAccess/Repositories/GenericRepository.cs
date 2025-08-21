using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

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
        public void Update(T entity) => _context.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => _dbSet.Remove(entity);
        public async Task<T?> GetOneAsync( Expression<Func<T, bool>>? filter = null,string? props = null)
        {
            IQueryable<T> Data = _dbSet;

            if (filter is not null)
            {
                Data = Data.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(props))
            {
                foreach (var item in props.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    Data = Data.Include(item.Trim());
                }
            }

            return await Data.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync( Expression<Func<T, bool>>? filter = null,string? props = null)
        {
            IQueryable<T> Data = _dbSet;

            if (filter is not null)
            {
                Data = Data.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(props))
            {
                foreach (var item in props.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    Data = Data.Include(item.Trim());
                }
            }

            return await Data.ToListAsync();
        }

    }
}
