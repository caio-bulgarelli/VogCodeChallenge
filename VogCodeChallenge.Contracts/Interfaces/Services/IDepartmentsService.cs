using System;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Contracts.Interfaces.Services
{
    public interface IDepartmentsService : IGenericService<Department>
    {
        Department Get(int id);
    }
}
