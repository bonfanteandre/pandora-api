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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderValidator _orderValidator;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderValidator orderValidator)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderValidator = orderValidator;
        }

        public async Task<OperationResult> AddAsync(Order order)
        {
            var validationResult = _orderValidator.Validate(order);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, order, errors);
            }

            order.SetCreatedNow();

            await _orderRepository.AddAsync(order);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, order, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, Order order)
        {
            var orderToUpdate = await _orderRepository.FindAsync(id);
            orderToUpdate.Update(order);

            var validationResult = _orderValidator.Validate(orderToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, order, errors);
            }

            await _orderRepository.UpdateAsync(orderToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, orderToUpdate, null);
        }

        public async Task<Order> FindAsync(Guid id)
        {
            return await _orderRepository.FindAsync(id);
        }

        public async Task<ICollection<Order>> FilterAsync(string search, int skip, int take)
        {
            if (search == null)
            {
                search = string.Empty;
            }

            return await _orderRepository.FilterAsync(
                skip,
                take,
                o => o.Customer.Name.ToLower().Contains(search.ToLower()),
                o => o.CreatedOn);
        }

        public async Task<OperationResult> FinishAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return new OperationResult(false, null, new List<string>() { "Pedido não encontrado" });
            }

            order.Finish();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, order, null);
        }

        public async Task<OperationResult> DeliverAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return new OperationResult(false, null, new List<string>() { "Pedido não encontrado" });
            }

            order.Deliver();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, order, null);
        }

        public async Task<OperationResult> CancelAsync(Guid id)
        {
            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return new OperationResult(false, null, new List<string>() { "Pedido não encontrado" });
            }

            order.Cancel();

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, order, null);
        }
    }
}
