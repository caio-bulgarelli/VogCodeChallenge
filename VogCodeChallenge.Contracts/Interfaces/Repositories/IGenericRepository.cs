using System;
using System.Collections.Generic;
using System.Text;

namespace VogCodeChallenge.Contracts.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        public IEnumerable<T> GetAll();
    }
}
