using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.CompilerServices;
using VogCodeChallenge.API.Controllers;
using VogCodeChallenge.Contracts.Dtos.Employees;
using VogCodeChallenge.Entities;
using VogCodeChallenge.Repositories;
using VogCodeChallenge.Services;
using Xunit;

namespace VogCodeChallenge.API.Tests
{
    /// <summary>
    /// Tests the controller.
    /// </summary>
    public class EmployeesControllerTests
    {
        /// <summary>
        /// Controller Initialization
        /// </summary>
        /// <param name="emptyEmployees">Creates a context without any departments</param>
        /// <returns></returns>
        private EmployeesController CreateController(bool emptyDepartments = false, bool emptyEmployees = false, [CallerMemberName] string databaseName = "")
        {
            IHost host = Host.CreateDefaultBuilder().Build();
            ILogger<EmployeesController> logger = host.Services.GetRequiredService<ILogger<EmployeesController>>();

            DbContextOptions<VogContext> contextOptions = new DbContextOptionsBuilder<VogContext>().UseInMemoryDatabase(databaseName: databaseName).Options;

            VogContext context = new VogContext(contextOptions);

            if (!emptyDepartments)
            {
                context.Departments.Add(new Department { Id = 1, Name = "Ecology Department", Address = "1865 Reserve St Campbellford, ON K0L 1L0" });
                context.Departments.Add(new Department { Id = 2, Name = "Construction Department", Address = "2635 Hammarskjold Dr Burnaby, BC V5B 3C9" });
                context.SaveChanges();
            }

            if (!emptyEmployees)
            {
                context.Employees.Add(new Employee { Id = 1, DepartmentId = 1, FirstName = "Dan", LastName = "Loden", JobTitle = "Nature Inspector", MailingAddress = "4652 Bates Brothers Road Hilliard, OH 43026" });
                context.Employees.Add(new Employee { Id = 2, DepartmentId = 1, FirstName = "Jennifer", LastName = "Evans", JobTitle = "Range Ecologist", MailingAddress = "4278 Walnut Avenue Newark, NJ 07102" });
                context.Employees.Add(new Employee { Id = 3, DepartmentId = 2, FirstName = "Teresa", LastName = "Hill", JobTitle = "Construction Equipment Technician", MailingAddress = "3789 Cook Hill Road Stamford, CT 06995" });
                context.Employees.Add(new Employee { Id = 4, DepartmentId = 2, FirstName = "Shane", LastName = "Headrick", JobTitle = "Construction Equipment Operator", MailingAddress = "1020 Yoho Valley Road Golden, BC V0A 1H0" });
                context.SaveChanges();
            }

            DepartmentsRepository departmentsRepository = new DepartmentsRepository(context);
            DepartmentsService departmentsService = new DepartmentsService(departmentsRepository);

            EmployeesRepository employeesRepository = new EmployeesRepository(context);
            EmployeesService employeesService = new EmployeesService(employeesRepository);

            return new EmployeesController(logger, employeesService, departmentsService);
        }

        [Fact]
        public void TestGet()
        {
            EmployeesController employeesController = CreateController();

            IActionResult actionResult = employeesController.Get();

            // Checks if the result is OK
            Assert.IsType<OkObjectResult>(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;

            // Checks if the result value is the correct response type.
            Assert.IsType<GetAllEmployeesResponse>(result.Value);

            GetAllEmployeesResponse response = result.Value as GetAllEmployeesResponse;

            // Checks if the controller returned the correct employees.
            Assert.Equal(4, response.Employees.Count());
        }

        [Fact]
        public void TestGetEmployeesEmpty()
        {
            EmployeesController employeesController = CreateController(emptyEmployees: true);

            IActionResult actionResult = employeesController.Get();

            // Checks if the result is NotFound 404
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void TestGetByDepartmentId()
        {
            EmployeesController employeesController = CreateController();

            IActionResult actionResult = employeesController.Get(2);

            // Checks if the result is OK
            Assert.IsType<OkObjectResult>(actionResult);

            OkObjectResult result = actionResult as OkObjectResult;

            // Checks if the result value is the correct response type.
            Assert.IsType<GetAllEmployeesByDepartmentResponse>(result.Value);

            GetAllEmployeesByDepartmentResponse response = result.Value as GetAllEmployeesByDepartmentResponse;

            // Checks if the controller found the correct department.
            Assert.Equal(2, response.Department.Id);

            // Checks if the controller returned the correct employees.
            Assert.Equal(2, response.Employees.Count());
            Assert.Collection(response.Employees,
                item => Assert.Equal(3, item.Id),
                item => Assert.Equal(4, item.Id));
        }

        [Fact]
        public void TestGetByDepartmentIdDepartmentNotFound()
        {
            EmployeesController employeesController = CreateController(emptyDepartments: true);

            IActionResult actionResult = employeesController.Get(2);

            // Checks if the result is NotFound 404
            Assert.IsType<NotFoundObjectResult>(actionResult);

            NotFoundObjectResult result = actionResult as NotFoundObjectResult;

            // Checks if the result value specifies that the department was not found.
            Assert.Equal("Department not found.", result.Value);
        }

        [Fact]
        public void TestGetByDepartmentIdEmployeesNotFound()
        {
            EmployeesController employeesController = CreateController(emptyEmployees: true);

            IActionResult actionResult = employeesController.Get(2);

            // Checks if the result is NotFound 404
            Assert.IsType<NotFoundObjectResult>(actionResult);

            NotFoundObjectResult result = actionResult as NotFoundObjectResult;

            // Checks if the result value specifies that the department was not found.
            Assert.StartsWith("No employees available for the specified department:", result.Value.ToString());
        }
    }
}
