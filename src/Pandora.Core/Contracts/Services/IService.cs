using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IService<T> where T : Entity
    {
        Task<OperationResult> AddAsync(T entity);
        Task<OperationResult> UpdateAsync(Guid id, T entity);
        Task<OperationResult> RemoveAsync(Guid id);
        Task<ICollection<T>> ListAsync();
        Task<T> FindAsync(Guid id);
    }
}
