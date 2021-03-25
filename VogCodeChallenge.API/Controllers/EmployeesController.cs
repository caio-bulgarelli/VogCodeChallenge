using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VogCodeChallenge.Contracts.Dtos.Employees;
using VogCodeChallenge.Contracts.Interfaces.Services;

namespace VogCodeChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeesService _employeesService;
        private readonly IDepartmentsService _departmentsService;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesService employeesService, IDepartmentsService departmentsService)
        {
            _logger = logger;
            _employeesService = employeesService;
            _departmentsService = departmentsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var employees = _employeesService.GetAll();

                if (!employees.Any())
                {
                    return NotFound();
                }

                return Ok(new GetAllEmployeesResponse
                {
                    Employees = employees
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on getting all employees");
                throw ex;
            }
        }

        [HttpGet("department/{departmentId}")]
        public IActionResult Get(int departmentId)
        {
            try
            {
                var department = _departmentsService.Get(departmentId);
                var employees = _employeesService.GetAll(departmentId);

                if (department == null)
                {
                    return NotFound("Department not found.");
                }

                if (!employees.Any())
                {
                    return NotFound($"No employees available for the specified department: {department.Name}");
                }

                return Ok(new GetAllEmployeesByDepartmentResponse
                {
                    Department = department,
                    Employees = employees
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on getting all employees by department: {0}", departmentId);
                throw ex;
            }
        }
    }
}
