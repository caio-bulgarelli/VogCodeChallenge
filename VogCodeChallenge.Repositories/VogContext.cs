using Microsoft.EntityFrameworkCore;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Repositories
{
    public class VogContext : DbContext
    {
        public DbSet<Department> Departmets { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public VogContext(DbContextOptions<VogContext> options) : base(options) { }

        public static void InitializeContext()
        {
            DbContextOptions<VogContext> contextOptions = new DbContextOptionsBuilder<VogContext>().UseInMemoryDatabase(databaseName: "Vog").Options;

            using (VogContext context = new VogContext(contextOptions))
            {
                context.Employees.Add(new Employee { Id = 1, DepartmentId = 1, FirstName = "Dan", LastName = "Loden", JobTitle = "Nature Inspector", MailingAddress = "4652 Bates Brothers Road Hilliard, OH 43026" });
                context.Employees.Add(new Employee { Id = 2, DepartmentId = 1, FirstName = "Jennifer", LastName = "Evans", JobTitle = "Range Ecologist", MailingAddress = "4278 Walnut Avenue Newark, NJ 07102" });
                context.Employees.Add(new Employee { Id = 3, DepartmentId = 2, FirstName = "Teresa", LastName = "Hill", JobTitle = "Construction Equipment Technician", MailingAddress = "3789 Cook Hill Road Stamford, CT 06995" });
                context.Employees.Add(new Employee { Id = 4, DepartmentId = 2, FirstName = "Shane", LastName = "Headrick", JobTitle = "Construction Equipment Operator", MailingAddress = "1020 Yoho Valley Road Golden, BC V0A 1H0" });
                context.Departmets.Add(new Department { Id = 1, Name = "Ecology Department", Address = "1865 Reserve St Campbellford, ON K0L 1L0" });
                context.Departmets.Add(new Department { Id = 2, Name = "Construction Department", Address = "2635 Hammarskjold Dr Burnaby, BC V5B 3C9" });
                context.SaveChanges();
            }
        }
    }
}
