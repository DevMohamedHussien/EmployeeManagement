using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using MyAssessment.DataAccess.Repositories;

namespace MyAssessment.DataAccess.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MyAssessmentDbContext _context;
        private IEmployeeRepository _employees;
        private IDepartmentRepository _departments;
        private ITaskRepository _tasks;

        public UnitOfWork(MyAssessmentDbContext context) => _context = context;

        public IEmployeeRepository Employees =>
            _employees ??= new EmployeeRepository(_context);

        public IDepartmentRepository Departments =>
            _departments ??= new DepartmentRepository(_context);
        public ITaskRepository Tasks =>
           _tasks ??= new TaskRepository(_context);
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
