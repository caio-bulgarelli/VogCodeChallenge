using System;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Contracts.Dtos.Employees
{
    public class GetAllEmployeesResponse
    {
        public IEnumerable<Employee> Employees { get; set; }
    }
}
