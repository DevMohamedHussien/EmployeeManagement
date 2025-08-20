using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;

namespace MyAssessment.Business.Services
{
    public class TaskService:ITaskService
    {

        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public Task<TaskItem> GetTaskByEmployeeIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskItem> GetTaskByManagerIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AssignTaskToEmployeeAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskStatusAsync(TaskItem task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
