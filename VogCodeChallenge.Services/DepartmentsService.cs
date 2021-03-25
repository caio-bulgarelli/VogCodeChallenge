using System.Collections.Generic;
using System.Linq;
using VogCodeChallenge.Contracts.Interfaces.Repositories;
using VogCodeChallenge.Contracts.Interfaces.Services;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Services
{
    /// <summary>
    /// General Departments Service that manages departments.
    /// </summary>
    public class DepartmentsService : IDepartmentsService
    {
        private IGenericRepository<Department> _repository;

        public DepartmentsService(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Department> GetAll()
        {
            return _repository.GetAll();
        }

        public Department Get(int id)
        {
            return _repository.GetAll().FirstOrDefault(e => e.Id == id);
        }

        public IList<Department> ListAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
