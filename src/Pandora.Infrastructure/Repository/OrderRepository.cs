using Microsoft.EntityFrameworkCore;
using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Entities;
using Pandora.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PandoraContext _context;

        public OrderRepository(PandoraContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public async Task<Order> FindAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ICollection<Order>> FilterAsync(int skip, int take, Expression<Func<Order, bool>> where, Expression<Func<Order, object>> orderBy)
        {
            return await _context
                .Orders
                .Include(o => o.Customer)
                .Include(o => o.PaymentMethod)
                .Skip(skip)
                .Take(take)
                .Where(where)
                .OrderBy(orderBy)
                .ToListAsync();
        }
    }
}
