using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Business.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EmployeeService(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        public async Task AddEmployeeAsync(Employee employee, string email, string password)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(user, "Employee");
            employee.AppUserId = user.Id;
            employee.ImagePath = await SaveImageAsync(employee.imageFile);
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            employee.ImagePath = await ReplaceImageAsync(employee.imageFile, employee.ImagePath);
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetOneAsync(c=>c.Id==id);
            var department = await _unitOfWork.Departments.GetOneAsync(d => d.Id == employee.DepartmentId);

            if (employee != null)
            {
                if (department.ManagerId == id)
                {
                    department.ManagerId = null;
                    var assignedTasks = await _unitOfWork.Tasks.GetAllAsync(t => t.EmployeeId == id);
                    foreach (var task in assignedTasks)
                    {
                        _unitOfWork.Tasks.Delete(task);
                         await _unitOfWork.SaveAsync(); 
                    }
                    var departmentEmployees = await _unitOfWork.Employees.GetAllAsync(e => e.ManagerId == id);
                    foreach (var emp in departmentEmployees)
                    {
                        emp.ManagerId = null;
                        await _unitOfWork.SaveAsync();
                    }
                }
                else
                {
                    await AssignTasksToManagerWhenTheEmployeeIsDeleted(id);
                }
                _unitOfWork.Employees.Delete(employee);
                await _unitOfWork.SaveAsync();
                DeleteImage(employee.ImagePath);
            
            }
        }


        public async Task<Employee> GetOneEmployeeAsync(Expression<Func<Employee, bool>>? filter=null, string? props = null)
        {
            try
            {
                var employee = await _unitOfWork.Employees.GetOneAsync(filter,props);
                return employee;
            }
            catch (Exception ex)
            {
                return new Employee();
            }
        }
        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync(Expression<Func<Employee, bool>>? filter = null, string? props = null) 
        {
            try
            {
                var employee = await _unitOfWork.Employees.GetAllAsync(filter, props);
                return employee;
            }
            catch (Exception ex)
            {
                return new List<Employee>();
            }
        }      
      

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {

            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads/employees");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
        private async Task<string> ReplaceImageAsync(IFormFile imageFile, string oldFileName = null)
        {
            if (imageFile!=null)
            {
                DeleteImage(oldFileName);
            }
            else
            {
                return oldFileName;
            }

            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads/employees");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
        private void DeleteImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                // Get the physical folder path
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", "employees");

                // Combine with the file name
                string filePath = Path.Combine(uploadsFolder, fileName);

                // Check and delete
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }
        private async Task AssignTasksToManagerWhenTheEmployeeIsDeleted(int employeeId)
        {
            var tasks = await _unitOfWork.Tasks.GetAllAsync(c=>c.EmployeeId==employeeId);
            foreach (var task in tasks)
            {
                task.EmployeeId = task.ManagerId;
            }
        }
    }
}
