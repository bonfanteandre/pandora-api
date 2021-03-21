using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class PlanValidator : IPlanValidator
    {
        public ValidationResult Validate(Plan plan)
        {
            var validator = new InlineValidator<Plan>();
            
            validator.RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nome é um campo obrigatório");

            validator.RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Preço é um campo obrigatório");

            return validator.Validate(plan);
        }
    }
}
