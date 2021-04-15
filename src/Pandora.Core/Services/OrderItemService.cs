using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Contracts.UnitOfWork;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> AddAsync(OrderItem item)
        {
            await _orderItemRepository.AddAsync(item);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, item, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, OrderItem item)
        {
            var itemToUpdate = await _orderItemRepository.FindAsync(id);
            itemToUpdate.Update(item);

            return new OperationResult(true, itemToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var item = await _orderItemRepository.FindAsync(id);

            if (item == null)
            {
                var errors = new List<string> { "Item não encontrado" };
                return new OperationResult(false, null, errors);
            }

            await _orderItemRepository.RemoveAsync(item);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }
    }
}
