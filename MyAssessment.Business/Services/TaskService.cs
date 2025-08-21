using Microsoft.EntityFrameworkCore;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;
using System.Linq.Expressions;

namespace MyAssessment.Business.Services
{
    public class TaskService:ITaskService
    {

        private readonly IUnitOfWork _unitOfWork;
        public TaskService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task AddTaskAsync(TaskItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.SaveAsync();
        }
        public async Task UpdateTaskAsync(TaskItem task)
        {
            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.SaveAsync();
        }
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetOneAsync(t=>t.Id==id);
            if (task!=null)
            {
                _unitOfWork.Tasks.Delete(task);
                await _unitOfWork.SaveAsync();
            }
        
        }

        public async Task<TaskItem> GetOneTaskAsync(Expression<Func<TaskItem, bool>>? filter = null, string? props = null)
        {
            try
            {
                var task = await _unitOfWork.Tasks.GetOneAsync(filter, props);
                return task;
            }
            catch (Exception ex)
            {
                return new TaskItem();
            }
        }
        public async Task<IEnumerable<TaskItem>> GetAllTaskstAsync(Expression<Func<TaskItem, bool>>? filter = null, string? props = null)
        {
            try
            {
                var tasks = await _unitOfWork.Tasks.GetAllAsync(filter, props);
                return tasks;
            }
            catch (Exception ex)
            {
                return new List<TaskItem>();
            }
        }


    }
}