using System;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Contracts.Dtos.Employees
{
    public class GetAllEmployeesByDepartmentResponse
    {
        public Department Department { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
