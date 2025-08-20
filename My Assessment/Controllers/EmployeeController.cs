using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAssessment.Business.Services;
using MyAssessment.Core.Interfaces;
using MyAssessment.Core.IServices;
using MyAssessment.Core.ViewModels;

namespace My_Assessment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IEmployeeService employeeService, IUnitOfWork unitOfWork, IDepartmentService departmentService)
        {
            _EmployeeService = employeeService;
            _unitOfWork = unitOfWork;
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _EmployeeService.GetAllEmployeesAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task< IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "Id", "Name");
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            var employee = EmployeeViewModel.GetEmployeeEntity(model);
            await _EmployeeService.AddEmployeeAsync(employee,model.Email,model.Password); 
            return RedirectToAction(nameof(Index)); 
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var employee = await _EmployeeService.GetEmployeeByIdAsync(id);
            var model =EmployeeViewModel.GetEmployeeViewModel(employee);
            ViewBag.Departments = new SelectList(await _departmentService.GetAllDepartmentsAsync(), "Id", "Name");

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel model)
        {
            var employee = EmployeeViewModel.GetEmployeeEntity(model); 
            await _EmployeeService.UpdateEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _EmployeeService.DeleteEmployeeAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}