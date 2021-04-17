using Pandora.Core.Contracts.Services;
using Pandora.Core.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;

        public StockService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task DecrementStock(Guid productId, int amount)
        {
            var product = await _productRepository.FindAsync(productId);
            product.DecrementStock(amount);
            await _productRepository.UpdateAsync(product);
        }

        public async Task IncrementStock(Guid productId, int amount)
        {
            var product = await _productRepository.FindAsync(productId);
            product.IncrementStock(amount);
            await _productRepository.UpdateAsync(product);
        }
    }
}
