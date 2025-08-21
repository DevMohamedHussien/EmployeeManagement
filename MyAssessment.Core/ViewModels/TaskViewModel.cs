using Microsoft.AspNetCore.Http;
using MyAssessment.Core.Entities;

namespace MyAssessment.Core.ViewModels
{
    public class TaskViewModel
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
