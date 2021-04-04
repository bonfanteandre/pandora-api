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
    public class AddressesService : IAddressesService
    {
        private readonly IAddressesRepository _addressesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressValidator _validator;

        public AddressesService(
            IAddressesRepository addressesRepository, 
            IUnitOfWork unitOfWork, 
            IAddressValidator addressValidator)
        {
            _addressesRepository = addressesRepository;
            _unitOfWork = unitOfWork;
            _validator = addressValidator;
        }

        public async Task<OperationResult> AddAsync(Address address)
        {
            var validationResult = _validator.Validate(address);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, address, errors);
            }

            await _addressesRepository.AddAsync(address);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, address, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, Address address)
        {
            var addressToUpdate = await _addressesRepository.FindAsync(id);
            addressToUpdate.Update(address);

            var validationResult = _validator.Validate(addressToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, address, errors);
            }

            await _addressesRepository.UpdateAsync(addressToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, addressToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var address = await _addressesRepository.FindAsync(id);

            if (address == null)
            {
                var errors = new List<string> { "Endereço não encontrado" };
                return new OperationResult(false, null, errors);
            }

            await _addressesRepository.RemoveAsync(address);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }

    }
}
