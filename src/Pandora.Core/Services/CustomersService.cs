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
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerValidator _validator;

        public CustomersService(
            ICustomersRepository customersRepository, 
            IUnitOfWork unitOfWork, 
            ICustomerValidator validator)
        {
            _customersRepository = customersRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OperationResult> AddAsync(Customer customer)
        {
            var validationResult = _validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, customer, errors);
            }

            await _customersRepository.AddAsync(customer);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, customer, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, Customer customer)
        {
            var customerToUpdate = await _customersRepository.FindAsync(id);
            customerToUpdate.Update(customer);

            var validationResult = _validator.Validate(customerToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, customer, errors);
            }

            await _customersRepository.UpdateAsync(customerToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, customerToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var customer = await _customersRepository.FindAsync(id);

            if (customer == null)
            {
                var errors = new List<string> { "Cliente não encontrado" };
                return new OperationResult(false, null, errors);
            }

            await _customersRepository.RemoveAsync(customer);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }

        public async Task<ICollection<Customer>> ListAsync()
        {
            return await _customersRepository.AllAsync();
        }

        public async Task<Customer> FindAsync(Guid id)
        {
            return await _customersRepository.FindAsync(id);
        }

        public async Task<ICollection<Customer>> FilterAndPaged(int skip, int take, string name)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            return await _customersRepository.FilterAsync(
                skip,
                take,
                p => p.Name.ToLower().Contains(name),
                p => p.Name);
        }
    }
}
