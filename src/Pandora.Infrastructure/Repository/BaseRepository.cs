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
    public abstract class BaseRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly PandoraContext _context;

        public BaseRepository(PandoraContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<T> FindAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<ICollection<T>> AllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<ICollection<T>> FilterAsync(
            int skip, 
            int take, 
            Expression<Func<T, bool>> where,
            Expression<Func<T, object>> orderBy)
        {
            return await _context.Set<T>()
                .OrderBy(orderBy)
                .Where(where)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
