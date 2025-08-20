using Microsoft.AspNetCore.Identity;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;
using MyAssessment.DataAccess.Data;

namespace MyAssessment.Business.Services
{
    public class DepartmentService:IDepartmentService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public DepartmentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync() =>
        await _unitOfWork.Departments.GetAllAsync(d => d.Employees, d => d.Manager);

        public async Task<Department> GetDepartmentByIdAsync(int id) =>
            await _unitOfWork.Departments.GetByIdAsync(id);

        public async Task AddDepartmentAsync(Department departmnet)
        {
            await _unitOfWork.Departments.AddAsync(departmnet);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateDepartmentAsync(DepartmentViewModel department)
        {
            var departmentEntity = DepartmentViewModel.GetDepartmentEntity(department);
            _unitOfWork.Departments.Update(departmentEntity);
            await _unitOfWork.SaveAsync();
            if (department.ManagerId != null)
            {
                var employee = await _unitOfWork.Employees.GetByIdAsync(department.ManagerId.Value);

                if (employee != null && !string.IsNullOrEmpty(employee.AppUserId))
                {
                    var appUser = await _userManager.FindByIdAsync(employee.AppUserId);

                    if (appUser != null && !await _userManager.IsInRoleAsync(appUser, "Manager"))
                    {
                        await _userManager.AddToRoleAsync(appUser, "Manager");
                    }
                }
            }
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department != null)
            {
                _unitOfWork.Departments.Delete(department);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
