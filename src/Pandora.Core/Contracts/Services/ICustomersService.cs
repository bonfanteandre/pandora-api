using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface ICustomersService : IService<Customer>
    {
        Task<ICollection<Customer>> FilterAndPaged(int skip, int take, string name);
    }
}
