using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        [NotMapped]
        public decimal TotalSalary => Employees?.Sum(e => e.Salary) ?? 0;
    }
}
