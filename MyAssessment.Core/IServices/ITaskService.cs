using Microsoft.Build.Framework;
using MyAssessment.Core.Entities;
using MyAssessment.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyAssessment.Core.IServices
{
    public interface ITaskService
    {
        Task<TaskItem> GetTaskByEmployeeIdAsync(int id);
        Task<TaskItem> GetTaskByManagerIdAsync(int id);
        Task AssignTaskToEmployeeAsync(TaskItem task);
        Task UpdateTaskStatusAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
    }
}
