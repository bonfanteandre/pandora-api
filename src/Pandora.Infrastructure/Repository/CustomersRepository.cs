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
    public class CustomersRepository : BaseRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(PandoraContext context) : base(context)
        {
        }

        public override async Task<ICollection<Customer>> AllAsync()
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public override async Task<Customer> FindAsync(Guid id)
        {
            return await _context.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id.Equals(id));
        }

        public override async Task<ICollection<Customer>> FilterAsync(int skip, int take, Expression<Func<Customer, bool>> where, Expression<Func<Customer, object>> orderBy)
        {
            return await _context.Customers
                .Include(c => c.Plan)
                .OrderBy(orderBy)
                .Where(where)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
