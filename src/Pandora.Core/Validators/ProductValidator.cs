using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class ProductValidator : IProductValidator
    {
        public ValidationResult Validate(Product product)
        {
            var validator = new InlineValidator<Product>();

            validator.RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nome é um campo obrigatório");

            validator.RuleFor(p => p.Cost)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Custo deve ser um número positivo");

            return validator.Validate(product);
        }
    }
}
