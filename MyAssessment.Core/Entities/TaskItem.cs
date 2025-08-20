using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAssessment.Core.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public MyAssessment.Core.Enums.TaskStatus Status { get; set; } = MyAssessment.Core.Enums.TaskStatus.New; 
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
        public string CretedBy { get; set; }
    }
}
