using FluentValidation.Results;
using Pandora.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pandora.Core
{
    public class OperationResult
    {
        public bool Success { get; private set; }
        public object Entity { get; private set; }
        public IList<string> Errors { get; private set; }

        public OperationResult(
            bool success,
            object entity, 
            IList<string> errors)
        {
            Success = success;
            Entity = entity;
            Errors = errors;
        }
    }
}
