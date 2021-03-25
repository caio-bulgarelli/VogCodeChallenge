using System;
using System.Collections.Generic;
using System.Text;
using VogCodeChallenge.Entities;

namespace VogCodeChallenge.Contracts.Interfaces.Services
{
    public interface IGenericService<T>
    {
        IEnumerable<T> GetAll();
        IList<T> ListAll();
    }
}
