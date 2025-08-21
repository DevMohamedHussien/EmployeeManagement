using MyAssessment.Core.Entities;
using MyAssessment.Core.ViewModels;
using System.Linq.Expressions;

namespace MyAssessment.Core.IServices
{
    public interface IDepartmentService
    {
        Task AddDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(DepartmentViewModel department);
        Task DeleteDepartmentAsync(int id);
        Task<Department> GetOneDepartmentAsync(Expression<Func<Department, bool>>? filter= null, string? props = null);
        Task<IEnumerable<Department>> GetAllDepartmentAsync(Expression<Func<Department, bool>>? filter=null, string? props = null);
    }
}
