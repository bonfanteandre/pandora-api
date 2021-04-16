using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Repositories
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        void Update(OrderItem orderItem);
        Task RemoveAsync(OrderItem orderItem);
        Task<OrderItem> FindAsync(Guid id);
    }
}
