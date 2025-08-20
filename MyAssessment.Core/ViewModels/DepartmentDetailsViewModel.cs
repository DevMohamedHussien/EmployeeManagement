using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.ViewModels
{
    public class DepartmentDetailsViewModel
    {
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }
        public decimal TotalSalary { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}
