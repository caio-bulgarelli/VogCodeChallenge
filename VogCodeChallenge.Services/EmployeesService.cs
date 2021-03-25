using System.Collections.Generic;
using System.Linq;
using VogCodeChallenge.Contracts.Interfaces.Repositories;
using VogCodeChallenge.Contracts.Interfaces.Services;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    /// <summary>
    /// General Employees Service that manages employees.
    /// </summary>
    public class EmployeesService : IEmployeesService
    {
        private IGenericRepository<Employee> _repository;

        public EmployeesService(IGenericRepository<Employee> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Employee> GetAll(int departmentId)
        {
            return _repository.GetAll().Where(e => e.DepartmentId == departmentId);
        }

        public IList<Employee> ListAll()
        {
            return _repository.GetAll().ToList();
        }

        public IList<Employee> ListAll(int departmentId)
        {
            return _repository.GetAll().Where(e => e.DepartmentId == departmentId).ToList();
        }
    }
}
