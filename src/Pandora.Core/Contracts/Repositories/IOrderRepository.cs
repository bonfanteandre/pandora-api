using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task<Order> FindAsync(Guid id);
        Task<ICollection<Order>> FilterAsync(
            int skip,
            int take,
            Expression<Func<Order, bool>> where,
            Expression<Func<Order, object>> orderBy);
    }
}
