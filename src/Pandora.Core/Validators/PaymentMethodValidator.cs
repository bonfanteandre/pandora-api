using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class PaymentMethodValidator : IPaymentMethodValidator
    {
        public ValidationResult Validate(PaymentMethod paymentMethod)
        {
            var validator = new InlineValidator<PaymentMethod>();

            validator.RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Nome é um campo obrigatório");

            return validator.Validate(paymentMethod);
        }
    }
}
