using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string ImagePath { get; set; }
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public IFormFile imageFile { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<TaskItem> AssignedTasks { get; set; }  // tasks where employee is assignee
        public ICollection<TaskItem> CreatedTasks { get; set; }
        public string AppUserId { get; set; }
    }
}
