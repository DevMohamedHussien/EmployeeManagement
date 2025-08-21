using MyAssessment.Core.Entities;
using MyAssessment.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.IServices
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(Employee emp, string email, string password);
        Task UpdateEmployeeAsync(Employee emp);
        Task DeleteEmployeeAsync(int id);
        Task<Employee> GetOneEmployeeAsync(Expression<Func<Employee, bool>>? filter = null, string? props = null);
        Task<IEnumerable<Employee>> GetAllEmployeeAsync(Expression<Func<Employee, bool>>? filter = null, string? props = null);
    }
}
