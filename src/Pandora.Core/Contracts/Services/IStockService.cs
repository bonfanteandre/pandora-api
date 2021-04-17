using Pandora.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Contracts.Services
{
    public interface IStockService
    {
        Task IncrementStock(Guid productId, int amount);
        Task DecrementStock(Guid productId, int amount);
    }
}
