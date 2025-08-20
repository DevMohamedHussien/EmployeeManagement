using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAssessment.Business.Services;
using MyAssessment.Core.Entities;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaskRepository _TaskRpo;
        private readonly IEmployeeRepository _EmployeeRpo;
        private readonly UserManager<AppUser> _userManager;

        private readonly IEmployeeService _employeeService;

        public TaskController(ITaskService taskService, IUnitOfWork unitOfWork, ITaskRepository taskRpo, IEmployeeRepository employeeRpo, UserManager<AppUser> userManager, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _TaskService = taskService;
            _unitOfWork = unitOfWork;
            _TaskRpo = taskRpo;
            _EmployeeRpo = employeeRpo;
            _userManager = userManager;
            _employeeService = employeeService;
            _DepartmentService = departmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            var managerUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tasks = await _TaskRpo.GetTaskByManagerIdAsync(managerUserId);
            return View(tasks);
        }
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyTasks()
        {
            var userId = _userManager.GetUserId(User);
            var employee = await _EmployeeRpo.GetEmployeeByUserIdAsync(userId);
            var tasks = await _TaskRpo.GetTasksByEmployeeIdAsync(employee.Id); 
            return View(tasks);
        }
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create()
        {

            var userId = _userManager.GetUserId(User);

            var manager = await _EmployeeRpo.GetEmployeeByUserIdAsync(userId);

            if (manager == null)
            {
                return Unauthorized(); 
            }
            var employees = await _employeeService.GetEmployeesByDepartmentAsync(manager.DepartmentId);
            var department = await _DepartmentService.GetDepartmentByIdAsync(manager.DepartmentId);
            ViewBag.Employees = new SelectList(employees, "Id", "FullName");
            ViewBag.DepartmentId = manager.DepartmentId;
            ViewBag.CreatedBy = manager.AppUserId;
            ViewBag.ManagerId = department.ManagerId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItem model)
        {
            await _TaskRpo.AssignTaskToEmployeeAsync(model); 
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TaskViewModel model)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}
