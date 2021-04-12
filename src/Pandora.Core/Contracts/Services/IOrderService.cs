using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IOrderService
    {
        Task<OperationResult> AddAsync(Order order);
        Task<OperationResult> UpdateAsync(Guid id, Order order);
        Task<OperationResult> FinishAsync(Guid id);
        Task<OperationResult> DeliverAsync(Guid id);
        Task<OperationResult> CancelAsync(Guid id);
        Task<Order> FindAsync(Guid id);
        Task<ICollection<Order>> FilterAsync(
            string search,
            int skip,
            int take);
    }
}
