using MyAssessment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ManagerId { get; set; }
        public string ManagerName { get; set; }
        public decimal TotalSalary { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }


        public static Department GetDepartmentEntity(DepartmentViewModel model)
        {
            return new Department
            {
                Id = model.Id,
                Name = model.Name,
                ManagerId = model.ManagerId
            };
        }

        public static DepartmentViewModel GetDepartmentViewModel(Department department)
        {
            return new DepartmentViewModel
            {
                Id = department.Id,
                Name = department.Name,
                ManagerId = department.ManagerId,
                ManagerName = department.Manager?.FullName??""
            };
        }
    }
}
