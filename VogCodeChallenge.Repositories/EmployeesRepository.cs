using System.Collections.Generic;
using VogCodeChallenge.Contracts.Interfaces.Repositories;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Repositories
{
    public class EmployeesRepository : IGenericRepository<Employee>
    {
        private readonly VogContext _vogContext;

        public EmployeesRepository(VogContext vogContext)
        {
            _vogContext = vogContext;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _vogContext.Employees;
        }
    }
}
