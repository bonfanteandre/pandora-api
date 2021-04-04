using Microsoft.Extensions.DependencyInjection;
using Pandora.Core.Contracts.Services;
using Pandora.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPlansService, PlansService>();
            services.AddTransient<IPaymentMethodsService, PaymentMethodsService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IAddressesService, AddressesService>();
        }
    }
}
