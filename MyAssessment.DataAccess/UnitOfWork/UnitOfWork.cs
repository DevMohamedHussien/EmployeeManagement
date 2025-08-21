using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using MyAssessment.DataAccess.Repositories;

namespace MyAssessment.DataAccess.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MyAssessmentDbContext _context;
        private IGenericRepository<Employee> _employees;
        private IGenericRepository<Department> _departments;
        private IGenericRepository<TaskItem> _tasks;

        public UnitOfWork(MyAssessmentDbContext context) => _context = context;

        public IGenericRepository<Employee> Employees =>
            _employees ??= new GenericRepository<Employee>(_context);

        public IGenericRepository<Department> Departments =>
            _departments ??= new GenericRepository<Department>(_context);
        public IGenericRepository<TaskItem> Tasks =>
           _tasks ??= new GenericRepository<TaskItem>(_context);
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
