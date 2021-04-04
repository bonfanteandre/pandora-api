using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class AddressValidator : IAddressValidator
    {
        public ValidationResult Validate(Address address)
        {
            var validator = new InlineValidator<Address>();

            validator.RuleFor(a => a.CustomerId)
                .NotNull()
                .WithMessage("Cliente é um campo obrigatório");

            validator.RuleFor(a => a.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente é um campo obrigatório");

            validator.RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage("Logradouro é um campo obrigatório");

            validator.RuleFor(a => a.Number)
                .NotEmpty()
                .WithMessage("Número é um campo obrigatório");

            validator.RuleFor(a => a.Neighborhood)
                .NotEmpty()
                .WithMessage("Bairro é um campo obrigatório");

            validator.RuleFor(a => a.Reference)
                .NotEmpty()
                .WithMessage("Ponto de referência é um campo obrigatório");

            return validator.Validate(address);
        }
    }
}
