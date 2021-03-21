using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core.Contracts.Validators
{
    public interface IValidator<T> where T : Entity
    {
        ValidationResult Validate(T entity);
    }
}
