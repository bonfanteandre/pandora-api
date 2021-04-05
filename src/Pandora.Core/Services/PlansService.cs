using FluentValidation.Results;
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
    public class PlansService : IPlansService
    {
        private readonly IPlansRepository _plansRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPlanValidator _validator;

        public PlansService(
            IPlansRepository plansRepository, 
            IUnitOfWork unitOfWork, 
            IPlanValidator validator)
        {
            _plansRepository = plansRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<OperationResult> AddAsync(Plan plan)
        {
            var validationResult = _validator.Validate(plan);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, plan, errors);
            }

            await _plansRepository.AddAsync(plan);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, plan, null);
        }

        public async Task<OperationResult> UpdateAsync(Guid id, Plan plan)
        {
            var planToUpdate = await _plansRepository.FindAsync(id);
            planToUpdate.Update(plan);

            var validationResult = _validator.Validate(planToUpdate);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new OperationResult(false, plan, errors);
            }

            await _plansRepository.UpdateAsync(planToUpdate);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, planToUpdate, null);
        }

        public async Task<OperationResult> RemoveAsync(Guid id)
        {
            var plan = await _plansRepository.FindAsync(id);

            if (plan == null)
            {
                var errors = new List<string> { "Plano não encontrado" };
                return new OperationResult(false, null, errors);
            }

            await _plansRepository.RemoveAsync(plan);
            await _unitOfWork.CommitAsync();

            return new OperationResult(true, null, null);
        }

        public async Task<ICollection<Plan>> ListAsync()
        {
            return await _plansRepository.AllAsync();
        }

        public async Task<ICollection<Plan>> FilterAndPaged(int skip, int take, string name)
        {
            if (name == null)
            {
                name = string.Empty;
            }

            return await _plansRepository.FilterAsync(
                skip,
                take,
                p => p.Name.ToLower().Contains(name),
                p => p.Name);
        }

        public async Task<Plan> FindAsync(Guid id)
        {
            return await _plansRepository.FindAsync(id);
        }

        public async Task<ICollection<Plan>> GetAll()
        {
            return await _plansRepository.AllAsync();
        }
    }
}
