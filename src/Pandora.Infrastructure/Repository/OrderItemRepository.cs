using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Entities;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly PandoraContext _context;

        public OrderItemRepository(PandoraContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }

        public Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(OrderItem orderItem)
        {
            _context.OrderItems.Remove(orderItem);
            return Task.CompletedTask;
        }

        public async Task<OrderItem> FindAsync(Guid id)
        {
            return await _context.OrderItems.FindAsync(id);
        }
    }
}
