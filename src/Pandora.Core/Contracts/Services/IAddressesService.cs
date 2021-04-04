using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IAddressesService
    {
        Task<OperationResult> AddAsync(Address address);
        Task<OperationResult> UpdateAsync(Guid id, Address address);
        Task<OperationResult> RemoveAsync(Guid id);
    }
}
