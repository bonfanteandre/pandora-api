using Microsoft.Extensions.DependencyInjection;
using Pandora.Core.Contracts.Validators;
using Pandora.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Extensions
{
    public static class ValidatorsExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IPlanValidator, PlanValidator>();
        }
    }
}
