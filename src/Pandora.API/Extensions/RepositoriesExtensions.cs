﻿using Microsoft.Extensions.DependencyInjection;
using Pandora.Core.Contracts.Repositories;
using Pandora.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPlansRepository, PlansRepository>();
            services.AddTransient<IPaymentMethodsRepository, PaymentMethodsRepository>();
        }
    }
}
