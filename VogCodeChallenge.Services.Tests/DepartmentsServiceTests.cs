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
    public class DepartmentsServiceTests
    {
        /// <summary>
        /// Service Initialization
        /// </summary>
        /// <param name="empty">Creates a context without any departments</param>
        /// <returns></returns>
        private DepartmentsService CreateService(bool empty = false, [CallerMemberName] string databaseName = "")
        {
            DbContextOptions<VogContext> contextOptions = new DbContextOptionsBuilder<VogContext>().UseInMemoryDatabase(databaseName: databaseName).Options;

            VogContext context = new VogContext(contextOptions);

            if (!empty)
            {
                context.Departments.Add(new Department { Id = 1, Name = "Ecology Department", Address = "1865 Reserve St Campbellford, ON K0L 1L0" });
                context.Departments.Add(new Department { Id = 2, Name = "Construction Department", Address = "2635 Hammarskjold Dr Burnaby, BC V5B 3C9" });
                context.SaveChanges();
            }

            DepartmentsRepository repository = new DepartmentsRepository(context);
            return new DepartmentsService(repository);
        }

        /// <summary>
        /// Tests if the service is properly returning all items available in the DbContext.
        /// </summary>
        [Fact]
        public void TestGetAll()
        {
            DepartmentsService service = CreateService();
            IEnumerable<Department> departments = service.GetAll();

            // The service must always return a collection, even when empty.
            Assert.NotNull(departments);

            // The service must return two items according to test configuration.
            Assert.Equal(2, departments.Count());
        }

        /// <summary>
        /// Checks the bevaior of the GetAll method when no items are available in the context.
        /// </summary>
        [Fact]
        public void TestGetAllEmpty()
        {
            // Parameter named to avoid confusion.
            DepartmentsService service = CreateService(empty: true);
            IEnumerable<Department> departments = service.GetAll();

            // The service must always return a collection, even when empty.
            Assert.NotNull(departments);

            // The service must return no items according to test configuration.
            Assert.Empty(departments);
        }

        /// <summary>
        /// Checks if the service is properly geting an Item based on it's Id.
        /// </summary>
        [Fact]
        public void TestGetOneExisting()
        {
            DepartmentsService service = CreateService();
            Department department1 = service.Get(1);

            // The service must find this item.
            Assert.NotNull(department1);

            // Checks if the service is returning the proper item.
            Assert.Equal(1, department1.Id);
            Assert.Equal("Ecology Department", department1.Name);
            Assert.Equal("1865 Reserve St Campbellford, ON K0L 1L0", department1.Address);

            // Second check to verify if the service is correctly considering the Id on search.
            Department department2 = service.Get(2);
            Assert.NotNull(department2);
            Assert.Equal(2, department2.Id);
            Assert.Equal("Construction Department", department2.Name);
            Assert.Equal("2635 Hammarskjold Dr Burnaby, BC V5B 3C9", department2.Address);
        }

        /// <summary>
        /// Checks if the service is properly handling non existing departments.
        /// </summary>
        [Fact]
        public void TestGetOneNonExisting()
        {
            DepartmentsService service = CreateService();
            Department department = service.Get(3);

            // The service must return default(Department) == null when the department is not found.
            Assert.Null(department);
        }
    }
}
