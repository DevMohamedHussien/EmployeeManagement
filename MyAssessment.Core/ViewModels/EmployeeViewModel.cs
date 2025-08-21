using Microsoft.AspNetCore.Http;
using MyAssessment.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyAssessment.Core.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public string ManagerName { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string AppUserId { get; set; }
        public static Employee GetEmployeeEntity(EmployeeViewModel model)
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salary = model.Salary,
                ImagePath = model.ImagePath,
                ManagerId = model.ManagerId,
                DepartmentId = model.DepartmentId,imageFile= model.ImageFile,
                AppUserId= model.AppUserId,
                
            };
        }
        public static EmployeeViewModel GetEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                ImagePath = employee.ImagePath,
                ManagerId = employee.ManagerId,
                ManagerName = employee.Manager?.FullName ?? "",
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name ?? "",
                ImageFile=employee.imageFile,
                AppUserId=employee.AppUserId
            };
        }
    }
}
