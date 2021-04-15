using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IOrderItemService
    {
        Task<OperationResult> AddAsync(OrderItem order);
        Task<OperationResult> UpdateAsync(Guid id, OrderItem order);
        Task<OperationResult> RemoveAsync(Guid id);
    }
}
