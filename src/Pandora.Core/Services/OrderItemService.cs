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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderItemValidator _orderItemValidator;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockService _stockService;

        public OrderItemService(
            IOrderItemRepository orderItemRepository,
            IOrderItemValidator orderItemValidator,
            IOrderRepository orderRepository,
            IUnitOfWork unitOfWork, 
            IStockService stockService)
        {
            _orderItemRepository = orderItemRepository;
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderItemValidator = orderItemValidator;
            _stockService = stockService;
        }

        public async Task<OperationResult> AddAsync(OrderItem item)
        {
            var validationResult = _orderItemValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, item, errors);
            }

            await _orderItemRepository.AddAsync(item);
            await _stockService.DecrementStock(item.ProductId, item.Amount);
            await _unitOfWork.CommitAsync();

            await UpdateOrderTotal(item.OrderId);

            return new OperationResult(true, item, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, OrderItem item)
        {
            var itemToUpdate = await _orderItemRepository.FindAsync(id);

            await _stockService.IncrementStock(itemToUpdate.ProductId, itemToUpdate.Amount);

            itemToUpdate.Update(item);

            var validationResult = _orderItemValidator.Validate(itemToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, itemToUpdate, errors);
            }

            await _orderItemRepository.UpdateAsync(itemToUpdate);
            await _stockService.DecrementStock(itemToUpdate.ProductId, itemToUpdate.Amount);
            await _unitOfWork.CommitAsync();

            await UpdateOrderTotal(item.OrderId);

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
            await _stockService.IncrementStock(item.ProductId, item.Amount);
            await _unitOfWork.CommitAsync();

            await UpdateOrderTotal(item.OrderId);

            return new OperationResult(true, null, null);
        }

        private async Task UpdateOrderTotal(Guid orderId)
        {
            var order = await _orderRepository.FindAsync(orderId);
            order.SetTotal();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();
        }
    }
}
