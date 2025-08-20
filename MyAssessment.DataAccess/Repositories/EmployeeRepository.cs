using Microsoft.EntityFrameworkCore;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.DataAccess.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MyAssessmentDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }
        public async Task<Employee> GetEmployeeByUserIdAsync(string id)
        {
            return await _context.Employees
      .FirstOrDefaultAsync(e => e.AppUserId == id);
        }
    }
}
