using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAssessment.Core.Entities;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;

namespace My_Assessment.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View(departments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //ViewBag.Employees = new SelectList(await _employeeService.GetAllEmployeesAsync(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            await _departmentService.AddDepartmentAsync(new Department { Name = model.Name });
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var department = await _departmentService.GetDepartmentByIdAsync(id);
            var model = DepartmentViewModel.GetDepartmentViewModel(department);
            ViewBag.Employees = new SelectList(await _employeeService.GetEmployeesByDepartmentAsync(id), "Id", "FullName", model.ManagerId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentViewModel model)
        {
            await _departmentService.UpdateDepartmentAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Search(int departmentId)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(departmentId);

            if (department == null)
            {
                return NotFound();
            }

            var employees = await _employeeService.GetEmployeesByDepartmentAsync(departmentId);

            var viewModel = new DepartmentDetailsViewModel
            {
                DepartmentName = department.Name,
                ManagerName = department.Manager?.FullName ?? "No Manager Assigned",
                TotalSalary = employees.Sum(e => e.Salary),
                Employees = employees.Select(e => new EmployeeDto
                {
                    Name = e.FullName,
                    Salary = e.Salary
                }).ToList()
            };

            return View(viewModel);
        }

    }
}
