using System;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Contracts.Interfaces.Services
{
    public interface IEmployeesService : IGenericService<Employee>
    {
        IEnumerable<Employee> GetAll(int departmentId);
        IList<Employee> ListAll(int departmentId);
    }
}
