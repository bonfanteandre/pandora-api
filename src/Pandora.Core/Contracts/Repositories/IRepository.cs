using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<T> FindAsync(Guid id);
        Task<ICollection<T>> AllAsync();
        Task<ICollection<T>> FilterAsync(
            int skip, 
            int take, 
            Expression<Func<T, bool>> where,
            Expression<Func<T, object>> orderBy);
    }
}
