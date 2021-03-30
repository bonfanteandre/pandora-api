using Pandora.Core.Contracts.Repositories;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Contracts.UnitOfWork;
using Pandora.Core.Entities;
using Pandora.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Core.Services
{
    public class PaymentMethodsService : IPaymentMethodsService
    {
        private readonly IPaymentMethodsRepository _paymentMethodsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentMethodValidator _validator;

        public PaymentMethodsService(
            IPaymentMethodsRepository paymentMethodsRepository, 
            IUnitOfWork unitOfWork, 
            IPaymentMethodValidator validator)
        {
            _paymentMethodsRepository = paymentMethodsRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OperationResult> AddAsync(PaymentMethod paymentMethod)
        {
            var validationResult = _validator.Validate(paymentMethod);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, paymentMethod, errors);
            }

            await _paymentMethodsRepository.AddAsync(paymentMethod);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, paymentMethod, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, PaymentMethod paymentMethod)
        {
            var paymentMethodToUpdate = await _paymentMethodsRepository.FindAsync(id);
            paymentMethodToUpdate.Update(paymentMethod);

            var validationResult = _validator.Validate(paymentMethodToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, paymentMethod, errors);
            }

            await _paymentMethodsRepository.UpdateAsync(paymentMethodToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, paymentMethodToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodsRepository.FindAsync(id);

            if (paymentMethod == null)
            {
                var errors = new List<string> { "Forma de pagamento não encontrada" };
                return new OperationResult(false, null, errors);
            }

            await _paymentMethodsRepository.RemoveAsync(paymentMethod);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }

        public async Task<ICollection<PaymentMethod>> ListAsync()
        {
            return await _paymentMethodsRepository.AllAsync();
        }

        public async Task<ICollection<PaymentMethod>> FilterAndPaged(int skip, int take, string name)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            return await _paymentMethodsRepository.FilterAsync(
                skip,
                take,
                p => p.Name.ToLower().Contains(name),
                p => p.Name);
        }

        public async Task<PaymentMethod> FindAsync(Guid id)
        {
            return await _paymentMethodsRepository.FindAsync(id);
        }
    }
}
