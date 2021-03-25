using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using VogCodeChallenge.Contracts.Dtos.Employees;
using VogCodeChallenge.Contracts.Interfaces.Services;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.API.Controllers
{
    /// <summary>
    /// Controller tha manages employees.
    /// Controller methods should translate internal entities to DTO Contracts.
    /// Methods should obey REST protocol.
    /// </summary>
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

        /// <summary>
        /// General get method that return all employees.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Employee> employees = _employeesService.GetAll();

                // If no employees are found, returns 404.
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

        /// <summary>
        /// Get method that returns all employees of a single department.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("department/{departmentId}")]
        public IActionResult Get(int departmentId)
        {
            try
            {
                Department department = _departmentsService.Get(departmentId);
                IEnumerable<Employee> employees = _employeesService.GetAll(departmentId);

                // If the department does not exist, returns 404.
                if (department == null)
                {
                    return NotFound("Department not found.");
                }

                // If no employees are found for the required department, returns 404.
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
