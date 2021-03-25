using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VogCodeChallenge.Entities;
using VogCodeChallenge.Repositories;
using Xunit;

namespace VogCodeChallenge.Services.Tests
{
    /// <summary>
    /// Not testing "ListAll" Methods for brevity
    /// </summary>
    public class EmployeesServiceTests
    {
        /// <summary>
        /// Service Initialization
        /// </summary>
        /// <param name="empty">Creates a context without any departments</param>
        /// <returns></returns>
        private EmployeesService CreateService(bool empty = false, [CallerMemberName] string databaseName = "")
        {
            DbContextOptions<VogContext> contextOptions = new DbContextOptionsBuilder<VogContext>().UseInMemoryDatabase(databaseName: databaseName).Options;

            VogContext context = new VogContext(contextOptions);

            if (!empty)
            {
                context.Employees.Add(new Employee { Id = 1, DepartmentId = 1, FirstName = "Dan", LastName = "Loden", JobTitle = "Nature Inspector", MailingAddress = "4652 Bates Brothers Road Hilliard, OH 43026" });
                context.Employees.Add(new Employee { Id = 2, DepartmentId = 1, FirstName = "Jennifer", LastName = "Evans", JobTitle = "Range Ecologist", MailingAddress = "4278 Walnut Avenue Newark, NJ 07102" });
                context.Employees.Add(new Employee { Id = 3, DepartmentId = 2, FirstName = "Teresa", LastName = "Hill", JobTitle = "Construction Equipment Technician", MailingAddress = "3789 Cook Hill Road Stamford, CT 06995" });
                context.Employees.Add(new Employee { Id = 4, DepartmentId = 2, FirstName = "Shane", LastName = "Headrick", JobTitle = "Construction Equipment Operator", MailingAddress = "1020 Yoho Valley Road Golden, BC V0A 1H0" });
                context.SaveChanges();
            }

            EmployeesRepository repository = new EmployeesRepository(context);
            return new EmployeesService(repository);
        }

        /// <summary>
        /// Tests if the service is properly returning all items available in the DbContext.
        /// </summary>
        [Fact]
        public void TestGetAll()
        {
            EmployeesService service = CreateService();
            IEnumerable<Employee> employees = service.GetAll();

            // The service must always return a collection, even when empty.
            Assert.NotNull(employees);

            // The service must return four items according to test configuration.
            Assert.Equal(4, employees.Count());
        }

        /// <summary>
        /// Checks the bevaior of the GetAll method when no items are available in the context.
        /// </summary>
        [Fact]
        public void TestGetAllEmptyNoEmployees()
        {
            // Parameter named to avoid confusion.
            EmployeesService service = CreateService(empty: true);
            IEnumerable<Employee> employees = service.GetAll();

            // The service must always return a collection, even when empty.
            Assert.NotNull(employees);

            // The service must return no items according to test configuration.
            Assert.Empty(employees);
        }

        /// <summary>
        /// Tests if the service is correctly filtering employees by department.
        /// </summary>
        [Fact]
        public void TestGetAllByDeparmentId()
        {
            // Parameter named to avoid confusion.
            EmployeesService service = CreateService();
            IEnumerable<Employee> employees1 = service.GetAll(1);

            // The service must always return a collection, even when empty.
            Assert.NotNull(employees1);

            // The service must return two items according to test configuration.
            Assert.Equal(2, employees1.Count());

            Employee firstEmployee = employees1.First();

            //Checks if the employees are according to test configuration.
            Assert.Equal(1, firstEmployee.Id);
            Assert.Equal("Dan", firstEmployee.FirstName);
            Assert.Equal("Loden", firstEmployee.LastName);
            Assert.Equal("Nature Inspector", firstEmployee.JobTitle);
            Assert.Equal("4652 Bates Brothers Road Hilliard, OH 43026", firstEmployee.MailingAddress);

            // Second test is to assert that the filter was not a lucky hit.
            IEnumerable<Employee> employees2 = service.GetAll(2);
            Assert.NotNull(employees2);
            Assert.Equal(2, employees2.Count());
            Employee firstEmployee2 = employees2.First();
            Assert.Equal(3, firstEmployee2.Id);
            Assert.Equal("Teresa", firstEmployee2.FirstName);
            Assert.Equal("Hill", firstEmployee2.LastName);
            Assert.Equal("Construction Equipment Technician", firstEmployee2.JobTitle);
            Assert.Equal("3789 Cook Hill Road Stamford, CT 06995", firstEmployee2.MailingAddress);
        }

        /// <summary>
        /// Checks if the service is properly handling non existing departments.
        /// </summary>
        [Fact]
        public void TestGetAllByDepartmentIdNonExisting()
        {
            EmployeesService service = CreateService();
            IEnumerable<Employee> employees = service.GetAll(3);

            // The service must always return a collection, even when empty.
            Assert.NotNull(employees);

            // The service must return no items according to test configuration.
            Assert.Empty(employees);
        }
    }
}
