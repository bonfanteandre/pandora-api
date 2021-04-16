using FluentValidation;
using FluentValidation.Results;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Validators
{
    public class OrderItemValidator : IOrderItemValidator
    {
        public ValidationResult Validate(OrderItem item)
        {
            var validator = new InlineValidator<OrderItem>();

            validator.RuleFor(i => i.Amount)
                .GreaterThan(0)
                .WithMessage("Quantidade deve ser maior que zero");

            validator.RuleFor(i => i.UnitValue)
                .GreaterThan(0)
                .WithMessage("Valor unitário deve ser maior que zero");

            validator.RuleFor(i => i.Price)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero");

            validator.RuleFor(i => i.OrderId)
                .NotNull()
                .WithMessage("O item deve pertencer a um pedido");

            validator.RuleFor(i => i.ProductId)
                .NotNull()
                .WithMessage("O item deve possuir um produto");

            return validator.Validate(item);
        }
    }
}
