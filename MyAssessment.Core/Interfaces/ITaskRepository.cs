using MyAssessment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.Core.Interfaces
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<List<TaskItem>> GetTasksByEmployeeIdAsync(int id);
        Task<List<TaskItem>> GetTaskByManagerIdAsync(string id);
        Task AssignTaskToEmployeeAsync(TaskItem task);
        Task UpdateTaskStatusAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}
