using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Contracts.UnitOfWork;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Services
{
    public class ProductService : IService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductValidator _productValidator;

        public ProductService(
            IProductRepository productRepository, 
            IUnitOfWork unitOfWork, 
            IProductValidator productValidator)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _productValidator = productValidator;
        }

        public async Task<OperationResult> AddAsync(Product product)
        {
            var validationResult = _productValidator.Validate(product);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, product, errors);
            }

            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, product, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, Product product)
        {
            var productToUpdate = await _productRepository.FindAsync(id);
            productToUpdate.Update(product);

            var validationResult = _productValidator.Validate(productToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, product, errors);
            }

            await _productRepository.UpdateAsync(productToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, productToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var product = await _productRepository.FindAsync(id);

            if (product == null)
            {
                var errors = new List<string> { "Produto não encontrado" };
                return new OperationResult(false, null, errors);
            }

            await _productRepository.RemoveAsync(product);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }

        public async Task<Product> FindAsync(Guid id)
        {
            return await _productRepository.FindAsync(id);
        }

        public async Task<ICollection<Product>> ListAsync()
        {
            return await _productRepository.AllAsync();
        }

        public async Task<ICollection<Product>> FilterAndPaged(int skip, int take, string name)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            return await _productRepository.FilterAsync(
                skip,
                take,
                p => p.Name.ToLower().Contains(name),
                p => p.Name);
        }
    }
}
