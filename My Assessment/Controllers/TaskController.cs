using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAssessment.Business.Services;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Enums;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;
using MyAssessment.DataAccess.Data;
using System.Security.Claims;

namespace My_Assessment.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _TaskService;
        private readonly IDepartmentService _DepartmentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeService _employeeService;

        public TaskController(ITaskService taskService,
            UserManager<AppUser> userManager,
            IEmployeeService employeeService,
            IDepartmentService departmentService)
        {
            _TaskService = taskService;
            _userManager = userManager;
            _employeeService = employeeService;
            _DepartmentService = departmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            var managerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _TaskService.GetAllTaskstAsync(t => t.CretedBy == managerUserId,props: "Employee");
            return View(tasks);
        }
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyTasks()
        {
            var userId = _userManager.GetUserId(User);
            var employee = await _employeeService.GetOneEmployeeAsync(e => e.AppUserId == userId);
            var tasks = await _TaskService.GetAllTaskstAsync(t => t.EmployeeId == employee.Id); 
            return View(tasks);
        }
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create()
        {

            var userId = _userManager.GetUserId(User);

            var manager = await _employeeService.GetOneEmployeeAsync(e => e.AppUserId == userId);
            if (manager == null)
            {
                return Unauthorized(); 
            }
            var employees = await _employeeService.GetAllEmployeeAsync(e => e.DepartmentId == manager.DepartmentId);
            ViewBag.Employees = new SelectList(employees, "Id", "FullName");
            ViewBag.DepartmentId = manager.DepartmentId;
            ViewBag.CreatedBy = manager.AppUserId;
            ViewBag.ManagerId = manager.Id; 

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(TaskItem model)
        {
            await _TaskService.AddTaskAsync(model); 
            
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> Update(int taskId)
        {
            ViewBag.Status = new SelectList( Enum.GetValues(typeof(MyAssessment.Core.Enums.TaskStatus)).Cast<MyAssessment.Core.Enums.TaskStatus>()
                    .Select(e => new { Id = (int)e, Name = e.ToString() }), "Id", "Name" );

            var task = await _TaskService.GetOneTaskAsync(t => t.Id == taskId); 
            return View(task);
        }
        [HttpPost]
        [Authorize(Roles = "Employee,Manager")]
        public async Task<IActionResult> Update(TaskViewModel model)
        {
            var task = await _TaskService.GetOneTaskAsync(t => t.Id == model.Id);
            task.Status = model.Status;
            task.Title = model.Title;
            await _TaskService.UpdateTaskAsync(task);
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null && await _userManager.IsInRoleAsync(currentUser, "Manager"))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(MyTasks));
            }
        }

    }
}
