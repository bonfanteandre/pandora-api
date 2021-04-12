using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public ValidationResult Validate(Order order)
        {
            var validator = new InlineValidator<Order>();

            validator.RuleFor(o => o.CustomerId)
                .NotNull()
                .WithMessage("Cliente é um campo obrigatório");

            validator.RuleFor(o => o.PaymentMethodId)
                .NotNull()
                .WithMessage("Forma de pagamento é um campo obrigatório");

            validator.RuleFor(o => o.DeliverAt)
                .NotNull()
                .WithMessage("Data/hora de entrega é um campo obrigatório");

            return validator.Validate(order);
        }
    }
}
