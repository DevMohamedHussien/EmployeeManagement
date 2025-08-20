using MyAssessment.Core.Entities;
using MyAssessment.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyAssessment.Core.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(DepartmentViewModel department);
        Task DeleteDepartmentAsync(int id);
        //Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(int managerId);
    }
}
