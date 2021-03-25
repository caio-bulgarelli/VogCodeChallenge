using System.Collections.Generic;
using VogCodeChallenge.Contracts.Interfaces.Repositories;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Repositories
{
    public class DepartmentsRepository: IGenericRepository<Department>
    {
        private readonly VogContext _vogContext;

        public DepartmentsRepository(VogContext vogContext)
        {
            _vogContext = vogContext;
        }

        public IEnumerable<Department> GetAll()
        {
            return _vogContext.Departmets;
        }
    }
}
