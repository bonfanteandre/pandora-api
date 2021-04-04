using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class CustomerValidator : ICustomerValidator
    {
        public ValidationResult Validate(Customer customer)
        {
            var validator = new InlineValidator<Customer>();

            validator.RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nome é um campo obrigatório");

            validator.RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .WithMessage("Preço é um campo obrigatório");

            validator.RuleFor(p => p.PhoneNumber)
                    .Length(11)
                    .WithMessage("Telefone deve possuir 11 caracteres");

            return validator.Validate(customer);
        }
    }
}
