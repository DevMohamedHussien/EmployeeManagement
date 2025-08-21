using MyAssessment.Core.Entities;
using MyAssessment.Core.ViewModels;
using System.Linq.Expressions;


namespace MyAssessment.Core.IServices
{
    public interface ITaskService
    {
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
        Task<TaskItem> GetOneTaskAsync(Expression<Func<TaskItem, bool>>? filter = null, string? props = null);
        Task<IEnumerable<TaskItem>> GetAllTaskstAsync(Expression<Func<TaskItem, bool>>? filter = null, string? props = null);
    }
}
