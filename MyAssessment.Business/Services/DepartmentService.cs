using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;
using MyAssessment.DataAccess.Data;
using NuGet.Protocol.Core.Types;
using System.Linq.Expressions;

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
                var employee = await _unitOfWork.Employees.GetOneAsync(e => e.Id == department.ManagerId);

                if (employee != null && !string.IsNullOrEmpty(employee.AppUserId))
                {
                    var appUser = await _userManager.FindByIdAsync(employee.AppUserId);

                    if (appUser != null && !await _userManager.IsInRoleAsync(appUser, "Manager"))
                    {
                        await _userManager.AddToRoleAsync(appUser, "Manager");
                    }
                    var DepartmentEmployees = await _unitOfWork.Employees.GetAllAsync(e => e.DepartmentId == department.Id && e.Id != employee.Id);
                    if (DepartmentEmployees.Any())
                    {
                        foreach (var emp in DepartmentEmployees)
                        {
                            emp.ManagerId = department.ManagerId;
                        }
                    }
                    await _unitOfWork.SaveAsync();
                }
          
            }

        }
        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetOneAsync(d => d.Id == id);
            if (department != null)
            {
                _unitOfWork.Departments.Delete(department);
                await _unitOfWork.SaveAsync();
            }
        }


        public async Task<Department> GetOneDepartmentAsync(Expression<Func<Department, bool>>? filter = null, string? props = null)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetOneAsync(filter, props);
                return department;
            }
            catch (Exception ex)
            {
                return new Department();
            }
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentAsync(Expression<Func<Department, bool>>? filter=null, string? props = null)
        {
            try
            {
                var department = await _unitOfWork.Departments.GetAllAsync(filter,props);
                return department;
            }
            catch (Exception ex)
            {
                return new List<Department>();
            }
        }
   
       
    }
}
