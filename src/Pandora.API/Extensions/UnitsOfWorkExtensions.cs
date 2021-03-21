using Microsoft.Extensions.DependencyInjection;
using Pandora.Core.Contracts.UnitOfWork;
using Pandora.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Extensions
{
    public static class UnitsOfWorkExtensions
    {
        public static void AddUnitsOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
