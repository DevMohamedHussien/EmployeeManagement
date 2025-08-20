using Microsoft.EntityFrameworkCore;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.DataAccess.Data;
using MyAssessment.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAssessment.DataAccess.Repositories
{

    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(MyAssessmentDbContext context) : base(context) { }

        public async Task<List<TaskItem>> GetTasksByEmployeeIdAsync(int id)
        {
            return await _context.TaskItems
                .Where(e => e.EmployeeId == id).Include(e => e.Employee)
                .ToListAsync();
        }

        public async Task<List<TaskItem>> GetTaskByManagerIdAsync(string id)
        {
            return await _context.TaskItems
                .Where(e => e.CretedBy == id).Include(t=>t.Employee)
                .ToListAsync();
        }

        public async Task AssignTaskToEmployeeAsync(TaskItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
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
