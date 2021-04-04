using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Repositories
{
    public interface IAddressesRepository
    {
        Task AddAsync(Address address);
        Task UpdateAsync(Address address);
        Task RemoveAsync(Address address);
        Task<Address> FindAsync(Guid id);
    }
}
